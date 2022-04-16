using SimpleRoutingAnalyzer.Metrics;
using SimpleRoutingAnalyzer.RoutingAlgorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleRoutingAnalyzer {
    public partial class MainForm : Form {
        private static readonly string Version = "0.0.0.0";

        private Graph GraphStorage = null;
        private Graph Graph {
            get => GraphStorage;
            set {
                GraphStorage = value;
                PreviewPanel.Graph = value;
            }
        }
        private IRoutingAlgorithm AlgorithmStorage = null;
        private IRoutingAlgorithm Algorithm {
            get => AlgorithmStorage;
            set {
                GlobalMetadataTextBox.Text = value?.Metadata() ?? "";

                AlgorithmStorage = value;
                PreviewPanel.Algorithm = value;
            }
        }
        private MessageQueue LogQueue = new MessageQueue();

        private void LoadConfig(string path) {
            Config config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path));
            Graph = config.Graph;
            Algorithm = Config.StringToAlgorithm(config.Algorithm, Graph);
            ConfigsTextBox.Lines = config.Configs;
            MetricsArgumentsTextBox.Lines = config.Seeds;
            NameTextBox.Text = config.Name;

            AlgorithmComboBox.SelectedItem = Config.AlgorithmToString(Algorithm);

            AdjencyTextBox.Text = Graph.ToString();
            CoordinatesTextBox.Text = Graph.PointsToString();
            GraphMetadataTextBox.Text = Graph.MetadataToString();
        }
        private void SaveConfig(string path) {
            var config = new Config();
            config.Graph = Graph;
            config.Algorithm = Config.AlgorithmToString(Algorithm);
            config.Configs = ConfigsTextBox.Lines;
            config.Seeds = MetricsArgumentsTextBox.Lines;
            config.Name = NameTextBox.Text;

            File.WriteAllText(path,
                JsonSerializer.Serialize(
                    config,
                    new JsonSerializerOptions() {
                        WriteIndented = true,
                    }
                )
            );
        }

        public MainForm() {
            InitializeComponent();

            MainSplitContainer.Panel2Collapsed = true;

            Text += " " + Version;

            GeneratorComboBox.Items.Add("Mesh");
            GeneratorComboBox.Items.Add("Circulant");
            GeneratorComboBox.SelectedIndex = 0;

            AlgorithmComboBox.Items.Add("None");
            AlgorithmComboBox.Items.Add(DijkstraRouting.Name);
            AlgorithmComboBox.Items.Add(NoCahceDijkstraRouting.Name);
            AlgorithmComboBox.Items.Add(GreedyPromotionRouting.Name);
            AlgorithmComboBox.Items.Add(AdvancedGreedyPromotionRouting.Name);
            AlgorithmComboBox.Items.Add(RestrictedGreedyPromotionRouting.Name);
            AlgorithmComboBox.Items.Add(RestrictedGreedyPromotion2Routing.Name);
            AlgorithmComboBox.Items.Add(BrutCoordsRouting.Name);
            AlgorithmComboBox.Items.Add(HotPotatoRouting.Name);
            AlgorithmComboBox.Items.Add(OFTRouting.Name);
            AlgorithmComboBox.Items.Add(CirculantGreedyPromotionRouting.Name);
            AlgorithmComboBox.SelectedIndex = 0;

            ModeComboBox.SelectedIndex = 0;

            try { LoadConfig("current_config.json"); } catch { }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            try { SaveConfig("current_config.json"); } catch (Exception ex) {
                MessageBox.Show(
                    this,
                    ex.ToString(),
                    "Error saving current config.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void GeneratorButton_Click(object sender, EventArgs e) {
            Graph graph = null;


            var splitter = new Regex("[^\\d]+");
            var args = splitter.Split(ArgumentsTextBox.Text).Where(it => it != "").ToArray();

            switch (GeneratorComboBox.SelectedIndex) {
                case 0:
                    int w, h;
                    if (args.Length == 2 &&
                        int.TryParse(args[0], out w) &&
                        int.TryParse(args[1], out h))
                        graph = GraphGenerator.Mesh(w, h);
                    break;
                case 1:
                    int n;
                    if (args.Length >= 1 && int.TryParse(args[0], out n)) {
                        int[] gens = new int[args.Length - 1];
                        bool no_error = true;
                        for (int i = 1; i < args.Length; i++)
                            if (!int.TryParse(args[i], out gens[i - 1]))
                                no_error = false;

                        if (no_error)
                            graph = GraphGenerator.Circulant(n, gens);
                    }
                    break;
            }

            if (graph == null) {
                AdjencyTextBox.Text = "";
                CoordinatesTextBox.Text = "";
                GraphMetadataTextBox.Text = "";
                MessageBox.Show(this,
                    "Can not parse arguments.",
                    "Generator arguments error.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            } else {
                AdjencyTextBox.Text = graph.ToString();
                CoordinatesTextBox.Text = graph.PointsToString();
                GraphMetadataTextBox.Text = graph.MetadataToString();
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e) {
            Graph = Graph.Parse(AdjencyTextBox.Text);
            Graph?.ParsePoints(CoordinatesTextBox.Text);
            Graph?.ParseMetadata(GraphMetadataTextBox.Text);
            Algorithm = Config.StringToAlgorithm(AlgorithmComboBox.SelectedItem as string, Graph);
        }
        private void ResetButton_Click(object sender, EventArgs e) {
            AdjencyTextBox.Text = Graph.ToString();
            CoordinatesTextBox.Text = Graph.PointsToString();
        }

        private void ApplyAlgorithmButton_Click(object sender, EventArgs e) {
            Algorithm = Config.StringToAlgorithm(AlgorithmComboBox.SelectedItem as string, Graph);
        }
        private void ResetAlgorithmButton_Click(object sender, EventArgs e) {
            AlgorithmComboBox.SelectedItem = Config.AlgorithmToString(Algorithm);
        }

        private void RunButton_Click(object sender, EventArgs e) {
            LogTextBox.Clear();
            ResultsDataGridView.Rows.Clear();

            IMetrics metrics = null;
            switch (ModeComboBox.SelectedIndex) {
                case 0: metrics = new StressMetrics(Graph, Algorithm, (int)SeedNumeric.Value); break;
                case 1: metrics = new HalfFaultMetrics(Graph, Algorithm, (int)SeedNumeric.Value, (int)IterationsNumeric.Value); break;
            };

            if (!MetricsBackgroundWorker.IsBusy) {
                MetricsBackgroundWorker.RunWorkerAsync(metrics);
            }
        }
        private void StopButton_Click(object sender, EventArgs e) {
            if (MetricsBackgroundWorker.IsBusy && !MetricsBackgroundWorker.CancellationPending) {
                MetricsBackgroundWorker.CancelAsync();
            }
        }

        private void MetricsBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            var metrics = e.Argument as IMetrics;

            for (int i = 0; i < metrics.IterationsTotal; i++) {
                metrics.Iterate();

                var state = metrics.State();
                state.Add("Iteration", $"{metrics.Iteration:000}");
                state.Add("Progress", $"{(double)metrics.Iteration / (metrics.IterationsTotal - 1):f4}");

                MetricsBackgroundWorker.ReportProgress(100 * i / metrics.IterationsTotal, state);
                if (MetricsBackgroundWorker.CancellationPending) {
                    break;
                }
            }
        }
        private void MetricsBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            PreviewPanel.Invalidate();
            MetricsProgressBar.Value = e.ProgressPercentage;

            var state = e.UserState as Dictionary<string, string>;
            if (state != null) {
                var row = new List<string>();
                foreach (var item in state) {
                    var columns = ResultsDataGridView.Columns.Cast<DataGridViewColumn>().ToList();
                    int index = columns.FindIndex(column => column.HeaderText == item.Key);
                    if (index < 0) {
                        index = columns.Count;
                        ResultsDataGridView.Columns.Add(item.Key, item.Key);
                    }
                    while (row.Count <= index) {
                        row.Add(null);
                    }
                    row[index] = item.Value;
                }
                
                ResultsDataGridView.Rows.Add(row.ToArray());
            }
        }
        private void MetricsBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (NameTextBox.Text != "") {
                var dir = "results";
                var path = $"{dir}/{NameTextBox.Text} [{SeedNumeric.Value}].csv";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                ResultsDataGridView.ExportCSV(path);
            }

            MetricsProgressBar.Value = 0;
        }

        private void ToggleToolStripMenuItem_Click(object sender, EventArgs e) {
            PreviewPanel.ToggleSelected();
        }
        private void SourceToolStripMenuItem_Click(object sender, EventArgs e) {
            PreviewPanel.SourceSelected();
        }
        private void DestinationToolStripMenuItem_Click(object sender, EventArgs e) {
            PreviewPanel.DestinationSelected();
        }
        private void ExportToolStripMenuItem_Click(object sender, EventArgs e) {
            var bitmap = PreviewPanel.Export();
            if (SaveExportDialog.ShowDialog(this) == DialogResult.OK) {
                bitmap.Save(SaveExportDialog.FileName, ImageFormat.Png);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) {
            try { 
                SaveConfig("current_config.json"); 
            } catch { }
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                if (SaveConfigDialog.ShowDialog(this) == DialogResult.OK)
                    SaveConfig(SaveConfigDialog.FileName);
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), "Error: Can not save config.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                if (OpenConfigDialog.ShowDialog(this) == DialogResult.OK)
                    LoadConfig(OpenConfigDialog.FileName);
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), "Error: Can not save config.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MessageTimer_Tick(object sender, EventArgs e) {
            string message;
            while ((message = LogQueue.Get()) != null)
                LogTextBox.AppendText(message + Environment.NewLine);
        }

        private void MetadataToolStripMenuItem_Click(object sender, EventArgs e) {
            MetadataToolStripMenuItem.Checked = !MetadataToolStripMenuItem.Checked;
            MainSplitContainer.Panel2Collapsed = !MetadataToolStripMenuItem.Checked;
        }
        private void DistancesToolStripMenuItem_Click(object sender, EventArgs e) {
            DistancesToolStripMenuItem.Checked = !DistancesToolStripMenuItem.Checked;
            PreviewPanel.ShowDistances = DistancesToolStripMenuItem.Checked;
        }

        private void PreviewPanel_SelectionChanged(object sender, EventArgs e) {
            MetadataTextBox.Clear();
            var builder = new StringBuilder();
            foreach (int node in PreviewPanel.Selected) {
                var metadata = Algorithm.Metadata(node);

                builder.Append($"Node: {node} {{");
                if (metadata != null) {
                    builder.Append(Environment.NewLine);
                    builder.Append(metadata);
                    builder.Append(Environment.NewLine);
                }
                builder.Append("};");
                builder.Append(Environment.NewLine);
            }
            MetadataTextBox.Text = builder.ToString();
        }

        private static Random SeedGenerator = new Random();
        private void RandomSeedButton_Click(object sender, EventArgs e) {
            SeedNumeric.Value = SeedGenerator.Next();
        }

        private void DrawMetadataToolStripMenuItem_Click(object sender, EventArgs e) {
            DrawMetadataToolStripMenuItem.Checked = !DrawMetadataToolStripMenuItem.Checked;
            if (DrawMetadataToolStripMenuItem.Checked) {
                var metadata = new string[Graph.Count];

                for (int i = 0; i < Graph.Count; i++) {
                    metadata[i] = Algorithm?.Metadata(i) ?? "{none}";
                }

                PreviewPanel.Metadata = metadata;
            } else PreviewPanel.Metadata = null;
        }
    }
}

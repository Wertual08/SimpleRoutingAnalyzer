using SimpleRoutingAnalyzer.RoutingAlgorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleRoutingAnalyzer
{
    public partial class MainForm : Form
    {
        private static readonly string Version = "0.0.0.0";
    
        private Graph GraphStorage = null;
        private Graph Graph
        {
            get => GraphStorage;
            set
            {
                GraphStorage = value;
                PreviewPanel.Graph = value;
            }
        }
        private IRoutingAlgorithm AlgorithmStorage = null;
        private IRoutingAlgorithm Algorithm
        {
            get => AlgorithmStorage;
            set
            {
                GlobalMetadataTextBox.Text = value?.Metadata() ?? "";

                AlgorithmStorage = value;
                PreviewPanel.Algorithm = value;
            }
        }
        private MessageQueue LogQueue = new MessageQueue();

        private void LoadConfig(string path)
        {
            Config config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path));
            Graph = config.Graph;
            Algorithm = Config.StringToAlgorithm(config.Algorithm, Graph);
            ConfigsTextBox.Lines = config.Configs;
            SeedsTextBox.Lines = config.Seeds;
            NameTextBox.Text = config.Name;

            AlgorithmComboBox.SelectedItem = Config.AlgorithmToString(Algorithm);

            AdjencyTextBox.Text = Graph.ToString();
            CoordinatesTextBox.Text = Graph.PointsToString();
        }
        private void SaveConfig(string path)
        {
            var config = new Config();
            config.Graph = Graph;
            config.Algorithm = Config.AlgorithmToString(Algorithm);
            config.Configs = ConfigsTextBox.Lines;
            config.Seeds = SeedsTextBox.Lines;
            config.Name = NameTextBox.Text;

            File.WriteAllText(path, 
                JsonSerializer.Serialize(config,
                new JsonSerializerOptions()
                {
                    WriteIndented = true,
                }));
        }

        public MainForm()
        {
            InitializeComponent();

            MainSplitContainer.Panel2Collapsed = true;

            Text += " " + Version;

            GeneratorComboBox.Items.Add("Mesh");
            GeneratorComboBox.Items.Add("Circulant");
            GeneratorComboBox.SelectedIndex = 0;

            AlgorithmComboBox.Items.Add("None");
            AlgorithmComboBox.Items.Add(DijkstraRouting.Name);
            AlgorithmComboBox.Items.Add(GreedyPromotionRouting.Name);
            AlgorithmComboBox.Items.Add(AdvancedGreedyPromotionRouting.Name);
            AlgorithmComboBox.Items.Add(RestrictedGreedyPromotionRouting.Name);
            AlgorithmComboBox.Items.Add(RestrictedGreedyPromotion2Routing.Name);
            AlgorithmComboBox.SelectedIndex = 0;

            ModeComboBox.SelectedIndex = 0;

            try { LoadConfig("current_config.json"); }
            catch { }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { SaveConfig("current_config.json"); }
            catch { }
        }

        private void GeneratorButton_Click(object sender, EventArgs e)
        {
            Graph graph = null;

            var args = ArgumentsTextBox.Text.Split(new char[] { ' ', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
            switch (GeneratorComboBox.SelectedIndex)
            {
                case 0:
                    int w, h;
                    if (args.Length == 2 &&
                        int.TryParse(args[0], out w) &&
                        int.TryParse(args[1], out h))
                        graph = GraphGenerator.Mesh(w, h);
                    break;
                case 1:
                    int n;
                    if (args.Length >= 1 && int.TryParse(args[0], out n))
                    {
                        int[] gens = new int[n - 1];
                        bool no_error = true;
                        for (int i = 1; i < args.Length; i++)
                            if (!int.TryParse(args[i], out gens[i - 1]))
                                no_error = false;

                        if (no_error)
                            graph = GraphGenerator.Circulant(n, gens);
                    }
                    break;
            }

            if (graph == null)
            {
                AdjencyTextBox.Text = "";
                CoordinatesTextBox.Text = "";
                MessageBox.Show(this,
                    "Can not parse arguments.",
                    "Generator arguments error.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            else
            {
                AdjencyTextBox.Text = graph.ToString();
                CoordinatesTextBox.Text = graph.PointsToString();
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Graph = Graph.Parse(AdjencyTextBox.Text);
            Graph?.ParsePoints(CoordinatesTextBox.Text);
            Algorithm = Config.StringToAlgorithm(AlgorithmComboBox.SelectedItem as string, Graph);
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            AdjencyTextBox.Text = Graph.ToString();
            CoordinatesTextBox.Text = Graph.PointsToString();
        }

        private void ApplyAlgorithmButton_Click(object sender, EventArgs e)
        {
            Algorithm = Config.StringToAlgorithm(AlgorithmComboBox.SelectedItem as string, Graph);
        }
        private void ResetAlgorithmButton_Click(object sender, EventArgs e)
        {
            AlgorithmComboBox.SelectedItem = Config.AlgorithmToString(Algorithm);
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            LogTextBox.Clear();
            ResultsDataGridView.Rows.Clear();


            int index = (int)IterationsNumeric.Value - 1;
            var seeds = SeedsTextBox.Lines;
            int seed;
            if (index < seeds.Length &&
                int.TryParse(seeds[index], out seed) && 
                seed >= 0)
                SeedNumeric.Value = seed;

            var args = new MetricsArguments();
            args.Configs = ConfigsTextBox.Lines;
            args.Seed = (int)SeedNumeric.Value;
            args.LogQueue = LogQueue;

            if (!MetricsBackgroundWorker.IsBusy)
                MetricsBackgroundWorker.RunWorkerAsync(args);
        }
        private void MetricsBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MetricsProgressBar.Value = e.ProgressPercentage;

            var state = e.UserState as MetricsState;
            if (state != null)
            {
                ResultsDataGridView.Rows.Add(
                    $"{ResultsDataGridView.RowCount:000}",
                    $"{state.DisabledNode}",
                    $"{state.AlgorithmAVGPath:F4}",
                    $"{state.OptimalAVGPath:F4}",
                    $"{state.AlgorithmAVGDelta:F4}",
                    $"{state.OptimalAVGDelta:F4}",
                    $"{state.AlgorithmUnreachable:F4}",
                    $"{state.OptimalUnreachable:F4}",
                    state.Error
                    );
            }
        }
        private void MetricsBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var res = e.Result as MetricsResult;
            if (NameTextBox.Text != "")
            {
                var dir = "results";
                var path = $"{dir}/{NameTextBox.Text} [{res.Seed}].csv";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                ResultsDataGridView.ExportCSV(path);
            }

            if (IterationsNumeric.Value > 1)
            {
                IterationsNumeric.Value--;
                RunButton.PerformClick();
            }

            MetricsProgressBar.Value = 0;
        }

        private void ToggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewPanel.ToggleSelected();
        }
        private void SourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewPanel.SourceSelected();
        }
        private void DestinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewPanel.DestinationSelected();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { SaveConfig("current_config.json"); }
            catch { }
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveConfigDialog.ShowDialog(this) == DialogResult.OK)
                    SaveConfig(SaveConfigDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error: Can not save config.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpenConfigDialog.ShowDialog(this) == DialogResult.OK)
                    LoadConfig(OpenConfigDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error: Can not save config.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MetricsBackgroundWorker.DoWork -= Metrics.Simple;
            MetricsBackgroundWorker.DoWork -= Metrics.StressTest;
            MetricsBackgroundWorker.DoWork -= Metrics.StressTestMonteKarlo;

            switch (ModeComboBox.SelectedIndex)
            {
                case 0: MetricsBackgroundWorker.DoWork += Metrics.Simple; break;
                case 1: MetricsBackgroundWorker.DoWork += Metrics.StressTest; break;
                case 2: MetricsBackgroundWorker.DoWork += Metrics.StressTestMonteKarlo; break;
            }
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            string message;
            while ((message = LogQueue.Get()) != null)
                LogTextBox.AppendText(message + Environment.NewLine);
        }

        private void MetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MetadataToolStripMenuItem.Checked = !MetadataToolStripMenuItem.Checked;
            MainSplitContainer.Panel2Collapsed = !MetadataToolStripMenuItem.Checked;
        }
        private void DistancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DistancesToolStripMenuItem.Checked = !DistancesToolStripMenuItem.Checked;
            PreviewPanel.ShowDistances = DistancesToolStripMenuItem.Checked;
        }

        private void PreviewPanel_SelectionChanged(object sender, EventArgs e)
        {
            MetadataTextBox.Clear();
            var builder = new StringBuilder();
            foreach (int node in PreviewPanel.Selected)
            {
                var metadata = Algorithm.Metadata(node);

                builder.Append($"Node: {node} {{");
                if (metadata != null)
                {
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
        private void RandomSeedButton_Click(object sender, EventArgs e)
        {
            SeedNumeric.Value = SeedGenerator.Next();
        }
    }
}

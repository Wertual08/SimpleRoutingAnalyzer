
namespace SimpleRoutingAnalyzer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DestinationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MetadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DistancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.GraphTabPage = new System.Windows.Forms.TabPage();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PreviewPanel = new SimpleRoutingAnalyzer.GraphPanel();
            this.MetadataTextBox = new System.Windows.Forms.TextBox();
            this.GlobalMetadataTextBox = new System.Windows.Forms.TextBox();
            this.PropertiesTabPage = new System.Windows.Forms.TabPage();
            this.ResetAlgorithmButton = new System.Windows.Forms.Button();
            this.ApplyAlgorithmButton = new System.Windows.Forms.Button();
            this.CoordinatesTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.GeneratorButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.AlgorithmComboBox = new System.Windows.Forms.ComboBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ArgumentsTextBox = new System.Windows.Forms.TextBox();
            this.GeneratorComboBox = new System.Windows.Forms.ComboBox();
            this.AdjencyTextBox = new System.Windows.Forms.TextBox();
            this.MetricxTabPage = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.SeedsTextBox = new System.Windows.Forms.TextBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.IterationsNumeric = new System.Windows.Forms.NumericUpDown();
            this.RandomSeedButton = new System.Windows.Forms.Button();
            this.SeedNumeric = new System.Windows.Forms.NumericUpDown();
            this.ResultsDataGridView = new System.Windows.Forms.DataGridView();
            this.NumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisabledColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlgorithmAVGPathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OptimalAVGPathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlgorithmAVGDeltaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OptimalAVGDeltaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlgorithmUnreachableColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OptimalUnreachableColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.ModeComboBox = new System.Windows.Forms.ComboBox();
            this.MetricsProgressBar = new System.Windows.Forms.ProgressBar();
            this.RunButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.ConfigsTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.GraphContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BeginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MetricsBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.OpenConfigDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveConfigDialog = new System.Windows.Forms.SaveFileDialog();
            this.MessageTimer = new System.Windows.Forms.Timer(this.components);
            this.DrawMetadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenuStrip.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.GraphTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.PropertiesTabPage.SuspendLayout();
            this.MetricxTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IterationsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SeedNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsDataGridView)).BeginInit();
            this.GraphContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopMenuStrip
            // 
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.TopMenuStrip.TabIndex = 1;
            this.TopMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem,
            this.OpenToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.SaveToolStripMenuItem.Text = "Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.SaveAsToolStripMenuItem.Text = "Save As";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateToolStripMenuItem,
            this.DeleteToolStripMenuItem,
            this.ToggleToolStripMenuItem,
            this.SourceToolStripMenuItem,
            this.DestinationToolStripMenuItem});
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.graphToolStripMenuItem.Text = "Graph";
            // 
            // CreateToolStripMenuItem
            // 
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.CreateToolStripMenuItem.Text = "Create";
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.DeleteToolStripMenuItem.Text = "Delete";
            // 
            // ToggleToolStripMenuItem
            // 
            this.ToggleToolStripMenuItem.Name = "ToggleToolStripMenuItem";
            this.ToggleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.ToggleToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.ToggleToolStripMenuItem.Text = "Toggle";
            this.ToggleToolStripMenuItem.Click += new System.EventHandler(this.ToggleToolStripMenuItem_Click);
            // 
            // SourceToolStripMenuItem
            // 
            this.SourceToolStripMenuItem.Name = "SourceToolStripMenuItem";
            this.SourceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.SourceToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.SourceToolStripMenuItem.Text = "Begin";
            this.SourceToolStripMenuItem.Click += new System.EventHandler(this.SourceToolStripMenuItem_Click);
            // 
            // DestinationToolStripMenuItem
            // 
            this.DestinationToolStripMenuItem.Name = "DestinationToolStripMenuItem";
            this.DestinationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.DestinationToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.DestinationToolStripMenuItem.Text = "End";
            this.DestinationToolStripMenuItem.Click += new System.EventHandler(this.DestinationToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MetadataToolStripMenuItem,
            this.DistancesToolStripMenuItem,
            this.DrawMetadataToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // MetadataToolStripMenuItem
            // 
            this.MetadataToolStripMenuItem.Name = "MetadataToolStripMenuItem";
            this.MetadataToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.MetadataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.MetadataToolStripMenuItem.Text = "Metadata";
            this.MetadataToolStripMenuItem.Click += new System.EventHandler(this.MetadataToolStripMenuItem_Click);
            // 
            // DistancesToolStripMenuItem
            // 
            this.DistancesToolStripMenuItem.Name = "DistancesToolStripMenuItem";
            this.DistancesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.DistancesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DistancesToolStripMenuItem.Text = "Distances";
            this.DistancesToolStripMenuItem.Click += new System.EventHandler(this.DistancesToolStripMenuItem_Click);
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.GraphTabPage);
            this.MainTabControl.Controls.Add(this.PropertiesTabPage);
            this.MainTabControl.Controls.Add(this.MetricxTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 24);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(800, 426);
            this.MainTabControl.TabIndex = 2;
            // 
            // GraphTabPage
            // 
            this.GraphTabPage.Controls.Add(this.MainSplitContainer);
            this.GraphTabPage.Location = new System.Drawing.Point(4, 22);
            this.GraphTabPage.Name = "GraphTabPage";
            this.GraphTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GraphTabPage.Size = new System.Drawing.Size(792, 400);
            this.GraphTabPage.TabIndex = 0;
            this.GraphTabPage.Text = "Graph";
            this.GraphTabPage.UseVisualStyleBackColor = true;
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.MainSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.PreviewPanel);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.MetadataTextBox);
            this.MainSplitContainer.Panel2.Controls.Add(this.GlobalMetadataTextBox);
            this.MainSplitContainer.Size = new System.Drawing.Size(786, 394);
            this.MainSplitContainer.SplitterDistance = 541;
            this.MainSplitContainer.TabIndex = 3;
            // 
            // PreviewPanel
            // 
            this.PreviewPanel.Algorithm = null;
            this.PreviewPanel.BackColor = System.Drawing.Color.DimGray;
            this.PreviewPanel.Destination = -1;
            this.PreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewPanel.Graph = null;
            this.PreviewPanel.Location = new System.Drawing.Point(0, 0);
            this.PreviewPanel.Name = "PreviewPanel";
            this.PreviewPanel.ShowDistances = false;
            this.PreviewPanel.Size = new System.Drawing.Size(537, 390);
            this.PreviewPanel.Source = -1;
            this.PreviewPanel.TabIndex = 0;
            this.PreviewPanel.SelectionChanged += new System.EventHandler(this.PreviewPanel_SelectionChanged);
            // 
            // MetadataTextBox
            // 
            this.MetadataTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MetadataTextBox.Location = new System.Drawing.Point(0, 20);
            this.MetadataTextBox.Multiline = true;
            this.MetadataTextBox.Name = "MetadataTextBox";
            this.MetadataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MetadataTextBox.Size = new System.Drawing.Size(237, 370);
            this.MetadataTextBox.TabIndex = 0;
            // 
            // GlobalMetadataTextBox
            // 
            this.GlobalMetadataTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.GlobalMetadataTextBox.Location = new System.Drawing.Point(0, 0);
            this.GlobalMetadataTextBox.Name = "GlobalMetadataTextBox";
            this.GlobalMetadataTextBox.Size = new System.Drawing.Size(237, 20);
            this.GlobalMetadataTextBox.TabIndex = 1;
            // 
            // PropertiesTabPage
            // 
            this.PropertiesTabPage.Controls.Add(this.ResetAlgorithmButton);
            this.PropertiesTabPage.Controls.Add(this.ApplyAlgorithmButton);
            this.PropertiesTabPage.Controls.Add(this.CoordinatesTextBox);
            this.PropertiesTabPage.Controls.Add(this.label5);
            this.PropertiesTabPage.Controls.Add(this.GeneratorButton);
            this.PropertiesTabPage.Controls.Add(this.label4);
            this.PropertiesTabPage.Controls.Add(this.AlgorithmComboBox);
            this.PropertiesTabPage.Controls.Add(this.ResetButton);
            this.PropertiesTabPage.Controls.Add(this.ApplyButton);
            this.PropertiesTabPage.Controls.Add(this.label3);
            this.PropertiesTabPage.Controls.Add(this.label2);
            this.PropertiesTabPage.Controls.Add(this.label1);
            this.PropertiesTabPage.Controls.Add(this.ArgumentsTextBox);
            this.PropertiesTabPage.Controls.Add(this.GeneratorComboBox);
            this.PropertiesTabPage.Controls.Add(this.AdjencyTextBox);
            this.PropertiesTabPage.Location = new System.Drawing.Point(4, 22);
            this.PropertiesTabPage.Name = "PropertiesTabPage";
            this.PropertiesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.PropertiesTabPage.Size = new System.Drawing.Size(792, 400);
            this.PropertiesTabPage.TabIndex = 1;
            this.PropertiesTabPage.Text = "Properties";
            this.PropertiesTabPage.UseVisualStyleBackColor = true;
            // 
            // ResetAlgorithmButton
            // 
            this.ResetAlgorithmButton.Location = new System.Drawing.Point(315, 17);
            this.ResetAlgorithmButton.Name = "ResetAlgorithmButton";
            this.ResetAlgorithmButton.Size = new System.Drawing.Size(75, 23);
            this.ResetAlgorithmButton.TabIndex = 14;
            this.ResetAlgorithmButton.Text = "Reset";
            this.ResetAlgorithmButton.UseVisualStyleBackColor = true;
            this.ResetAlgorithmButton.Click += new System.EventHandler(this.ResetAlgorithmButton_Click);
            // 
            // ApplyAlgorithmButton
            // 
            this.ApplyAlgorithmButton.Location = new System.Drawing.Point(234, 17);
            this.ApplyAlgorithmButton.Name = "ApplyAlgorithmButton";
            this.ApplyAlgorithmButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyAlgorithmButton.TabIndex = 13;
            this.ApplyAlgorithmButton.Text = "Apply";
            this.ApplyAlgorithmButton.UseVisualStyleBackColor = true;
            this.ApplyAlgorithmButton.Click += new System.EventHandler(this.ApplyAlgorithmButton_Click);
            // 
            // CoordinatesTextBox
            // 
            this.CoordinatesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CoordinatesTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CoordinatesTextBox.Location = new System.Drawing.Point(648, 99);
            this.CoordinatesTextBox.Multiline = true;
            this.CoordinatesTextBox.Name = "CoordinatesTextBox";
            this.CoordinatesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CoordinatesTextBox.Size = new System.Drawing.Size(138, 266);
            this.CoordinatesTextBox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(647, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Point positions";
            // 
            // GeneratorButton
            // 
            this.GeneratorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GeneratorButton.Location = new System.Drawing.Point(709, 57);
            this.GeneratorButton.Name = "GeneratorButton";
            this.GeneratorButton.Size = new System.Drawing.Size(75, 23);
            this.GeneratorButton.TabIndex = 10;
            this.GeneratorButton.Text = "Generate";
            this.GeneratorButton.UseVisualStyleBackColor = true;
            this.GeneratorButton.Click += new System.EventHandler(this.GeneratorButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Routing algorithm";
            // 
            // AlgorithmComboBox
            // 
            this.AlgorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AlgorithmComboBox.FormattingEnabled = true;
            this.AlgorithmComboBox.Location = new System.Drawing.Point(6, 19);
            this.AlgorithmComboBox.Name = "AlgorithmComboBox";
            this.AlgorithmComboBox.Size = new System.Drawing.Size(222, 21);
            this.AlgorithmComboBox.TabIndex = 8;
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetButton.Location = new System.Drawing.Point(87, 371);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 7;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ApplyButton.Location = new System.Drawing.Point(6, 371);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyButton.TabIndex = 6;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Adjency matrix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Generator";
            // 
            // ArgumentsTextBox
            // 
            this.ArgumentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ArgumentsTextBox.Location = new System.Drawing.Point(234, 59);
            this.ArgumentsTextBox.Name = "ArgumentsTextBox";
            this.ArgumentsTextBox.Size = new System.Drawing.Size(471, 20);
            this.ArgumentsTextBox.TabIndex = 2;
            // 
            // GeneratorComboBox
            // 
            this.GeneratorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GeneratorComboBox.FormattingEnabled = true;
            this.GeneratorComboBox.Location = new System.Drawing.Point(6, 59);
            this.GeneratorComboBox.Name = "GeneratorComboBox";
            this.GeneratorComboBox.Size = new System.Drawing.Size(222, 21);
            this.GeneratorComboBox.TabIndex = 1;
            // 
            // AdjencyTextBox
            // 
            this.AdjencyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AdjencyTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AdjencyTextBox.Location = new System.Drawing.Point(6, 99);
            this.AdjencyTextBox.Multiline = true;
            this.AdjencyTextBox.Name = "AdjencyTextBox";
            this.AdjencyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AdjencyTextBox.Size = new System.Drawing.Size(636, 266);
            this.AdjencyTextBox.TabIndex = 0;
            // 
            // MetricxTabPage
            // 
            this.MetricxTabPage.Controls.Add(this.label12);
            this.MetricxTabPage.Controls.Add(this.SeedsTextBox);
            this.MetricxTabPage.Controls.Add(this.NameTextBox);
            this.MetricxTabPage.Controls.Add(this.label11);
            this.MetricxTabPage.Controls.Add(this.label10);
            this.MetricxTabPage.Controls.Add(this.label7);
            this.MetricxTabPage.Controls.Add(this.IterationsNumeric);
            this.MetricxTabPage.Controls.Add(this.RandomSeedButton);
            this.MetricxTabPage.Controls.Add(this.SeedNumeric);
            this.MetricxTabPage.Controls.Add(this.ResultsDataGridView);
            this.MetricxTabPage.Controls.Add(this.label9);
            this.MetricxTabPage.Controls.Add(this.LogTextBox);
            this.MetricxTabPage.Controls.Add(this.ModeComboBox);
            this.MetricxTabPage.Controls.Add(this.MetricsProgressBar);
            this.MetricxTabPage.Controls.Add(this.RunButton);
            this.MetricxTabPage.Controls.Add(this.label8);
            this.MetricxTabPage.Controls.Add(this.ConfigsTextBox);
            this.MetricxTabPage.Controls.Add(this.label6);
            this.MetricxTabPage.Location = new System.Drawing.Point(4, 22);
            this.MetricxTabPage.Name = "MetricxTabPage";
            this.MetricxTabPage.Size = new System.Drawing.Size(792, 400);
            this.MetricxTabPage.TabIndex = 2;
            this.MetricxTabPage.Text = "Metrics";
            this.MetricxTabPage.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(0, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Seeds";
            // 
            // SeedsTextBox
            // 
            this.SeedsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SeedsTextBox.Location = new System.Drawing.Point(3, 58);
            this.SeedsTextBox.Multiline = true;
            this.SeedsTextBox.Name = "SeedsTextBox";
            this.SeedsTextBox.Size = new System.Drawing.Size(282, 206);
            this.SeedsTextBox.TabIndex = 19;
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NameTextBox.Location = new System.Drawing.Point(3, 283);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(282, 20);
            this.NameTextBox.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(0, 267);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Name";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(81, 306);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Seed";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Iterations";
            // 
            // IterationsNumeric
            // 
            this.IterationsNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IterationsNumeric.Location = new System.Drawing.Point(3, 321);
            this.IterationsNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.IterationsNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.IterationsNumeric.Name = "IterationsNumeric";
            this.IterationsNumeric.Size = new System.Drawing.Size(75, 20);
            this.IterationsNumeric.TabIndex = 14;
            this.IterationsNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // RandomSeedButton
            // 
            this.RandomSeedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RandomSeedButton.Location = new System.Drawing.Point(204, 321);
            this.RandomSeedButton.Name = "RandomSeedButton";
            this.RandomSeedButton.Size = new System.Drawing.Size(81, 20);
            this.RandomSeedButton.TabIndex = 13;
            this.RandomSeedButton.Text = "Random seed";
            this.RandomSeedButton.UseVisualStyleBackColor = true;
            this.RandomSeedButton.Click += new System.EventHandler(this.RandomSeedButton_Click);
            // 
            // SeedNumeric
            // 
            this.SeedNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SeedNumeric.Location = new System.Drawing.Point(84, 321);
            this.SeedNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.SeedNumeric.Name = "SeedNumeric";
            this.SeedNumeric.Size = new System.Drawing.Size(114, 20);
            this.SeedNumeric.TabIndex = 12;
            // 
            // ResultsDataGridView
            // 
            this.ResultsDataGridView.AllowUserToAddRows = false;
            this.ResultsDataGridView.AllowUserToDeleteRows = false;
            this.ResultsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumberColumn,
            this.DisabledColumn,
            this.AlgorithmAVGPathColumn,
            this.OptimalAVGPathColumn,
            this.AlgorithmAVGDeltaColumn,
            this.OptimalAVGDeltaColumn,
            this.AlgorithmUnreachableColumn,
            this.OptimalUnreachableColumn,
            this.ErrorColumn});
            this.ResultsDataGridView.Location = new System.Drawing.Point(503, 19);
            this.ResultsDataGridView.Name = "ResultsDataGridView";
            this.ResultsDataGridView.ReadOnly = true;
            this.ResultsDataGridView.RowHeadersVisible = false;
            this.ResultsDataGridView.Size = new System.Drawing.Size(286, 349);
            this.ResultsDataGridView.TabIndex = 11;
            // 
            // NumberColumn
            // 
            this.NumberColumn.HeaderText = "Number";
            this.NumberColumn.Name = "NumberColumn";
            this.NumberColumn.ReadOnly = true;
            this.NumberColumn.Width = 50;
            // 
            // DisabledColumn
            // 
            this.DisabledColumn.HeaderText = "Disabled";
            this.DisabledColumn.Name = "DisabledColumn";
            this.DisabledColumn.ReadOnly = true;
            this.DisabledColumn.Width = 50;
            // 
            // AlgorithmAVGPathColumn
            // 
            this.AlgorithmAVGPathColumn.HeaderText = "Algorithm AVG Path";
            this.AlgorithmAVGPathColumn.Name = "AlgorithmAVGPathColumn";
            this.AlgorithmAVGPathColumn.ReadOnly = true;
            // 
            // OptimalAVGPathColumn
            // 
            this.OptimalAVGPathColumn.HeaderText = "Optimal AVG Path";
            this.OptimalAVGPathColumn.Name = "OptimalAVGPathColumn";
            this.OptimalAVGPathColumn.ReadOnly = true;
            // 
            // AlgorithmAVGDeltaColumn
            // 
            this.AlgorithmAVGDeltaColumn.HeaderText = "Algorithm AVG Delta";
            this.AlgorithmAVGDeltaColumn.Name = "AlgorithmAVGDeltaColumn";
            this.AlgorithmAVGDeltaColumn.ReadOnly = true;
            // 
            // OptimalAVGDeltaColumn
            // 
            this.OptimalAVGDeltaColumn.HeaderText = "Optimal AVG Delta";
            this.OptimalAVGDeltaColumn.Name = "OptimalAVGDeltaColumn";
            this.OptimalAVGDeltaColumn.ReadOnly = true;
            // 
            // AlgorithmUnreachableColumn
            // 
            this.AlgorithmUnreachableColumn.HeaderText = "Algorithm Unreachable";
            this.AlgorithmUnreachableColumn.Name = "AlgorithmUnreachableColumn";
            this.AlgorithmUnreachableColumn.ReadOnly = true;
            // 
            // OptimalUnreachableColumn
            // 
            this.OptimalUnreachableColumn.HeaderText = "Optimal Unreachable";
            this.OptimalUnreachableColumn.Name = "OptimalUnreachableColumn";
            this.OptimalUnreachableColumn.ReadOnly = true;
            // 
            // ErrorColumn
            // 
            this.ErrorColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ErrorColumn.HeaderText = "Error";
            this.ErrorColumn.Name = "ErrorColumn";
            this.ErrorColumn.ReadOnly = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(288, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Log";
            // 
            // LogTextBox
            // 
            this.LogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LogTextBox.Location = new System.Drawing.Point(291, 19);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(206, 349);
            this.LogTextBox.TabIndex = 9;
            // 
            // ModeComboBox
            // 
            this.ModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModeComboBox.FormattingEnabled = true;
            this.ModeComboBox.Items.AddRange(new object[] {
            "Simple",
            "Stress test",
            "Stress test MonteKarlo"});
            this.ModeComboBox.Location = new System.Drawing.Point(3, 347);
            this.ModeComboBox.Name = "ModeComboBox";
            this.ModeComboBox.Size = new System.Drawing.Size(282, 21);
            this.ModeComboBox.TabIndex = 8;
            this.ModeComboBox.SelectedIndexChanged += new System.EventHandler(this.ModeComboBox_SelectedIndexChanged);
            // 
            // MetricsProgressBar
            // 
            this.MetricsProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MetricsProgressBar.ForeColor = System.Drawing.Color.Chartreuse;
            this.MetricsProgressBar.Location = new System.Drawing.Point(84, 374);
            this.MetricsProgressBar.MarqueeAnimationSpeed = 1000;
            this.MetricsProgressBar.Name = "MetricsProgressBar";
            this.MetricsProgressBar.Size = new System.Drawing.Size(705, 23);
            this.MetricsProgressBar.TabIndex = 7;
            // 
            // RunButton
            // 
            this.RunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RunButton.Location = new System.Drawing.Point(3, 374);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 6;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Configs";
            // 
            // ConfigsTextBox
            // 
            this.ConfigsTextBox.Location = new System.Drawing.Point(3, 19);
            this.ConfigsTextBox.Name = "ConfigsTextBox";
            this.ConfigsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConfigsTextBox.Size = new System.Drawing.Size(282, 20);
            this.ConfigsTextBox.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(500, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Results";
            // 
            // GraphContextMenuStrip
            // 
            this.GraphContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BeginToolStripMenuItem,
            this.EndToolStripMenuItem});
            this.GraphContextMenuStrip.Name = "GraphContextMenuStrip";
            this.GraphContextMenuStrip.Size = new System.Drawing.Size(105, 48);
            // 
            // BeginToolStripMenuItem
            // 
            this.BeginToolStripMenuItem.Name = "BeginToolStripMenuItem";
            this.BeginToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.BeginToolStripMenuItem.Text = "Begin";
            this.BeginToolStripMenuItem.Click += new System.EventHandler(this.SourceToolStripMenuItem_Click);
            // 
            // EndToolStripMenuItem
            // 
            this.EndToolStripMenuItem.Name = "EndToolStripMenuItem";
            this.EndToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.EndToolStripMenuItem.Text = "End";
            this.EndToolStripMenuItem.Click += new System.EventHandler(this.DestinationToolStripMenuItem_Click);
            // 
            // MetricsBackgroundWorker
            // 
            this.MetricsBackgroundWorker.WorkerReportsProgress = true;
            this.MetricsBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MetricsBackgroundWorker_ProgressChanged);
            this.MetricsBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MetricsBackgroundWorker_RunWorkerCompleted);
            // 
            // OpenConfigDialog
            // 
            this.OpenConfigDialog.DefaultExt = "json";
            this.OpenConfigDialog.Filter = "JSON (*.json)|*.json";
            // 
            // SaveConfigDialog
            // 
            this.SaveConfigDialog.DefaultExt = "json";
            this.SaveConfigDialog.Filter = "JSON (*.json)|*.json";
            // 
            // MessageTimer
            // 
            this.MessageTimer.Enabled = true;
            this.MessageTimer.Tick += new System.EventHandler(this.MessageTimer_Tick);
            // 
            // DrawMetadataToolStripMenuItem
            // 
            this.DrawMetadataToolStripMenuItem.Name = "DrawMetadataToolStripMenuItem";
            this.DrawMetadataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DrawMetadataToolStripMenuItem.Text = "Draw metadata";
            this.DrawMetadataToolStripMenuItem.Click += new System.EventHandler(this.DrawMetadataToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.TopMenuStrip);
            this.MainMenuStrip = this.TopMenuStrip;
            this.Name = "MainForm";
            this.Text = "SRA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            this.MainTabControl.ResumeLayout(false);
            this.GraphTabPage.ResumeLayout(false);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            this.MainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.PropertiesTabPage.ResumeLayout(false);
            this.PropertiesTabPage.PerformLayout();
            this.MetricxTabPage.ResumeLayout(false);
            this.MetricxTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IterationsNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SeedNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsDataGridView)).EndInit();
            this.GraphContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphPanel PreviewPanel;
        private System.Windows.Forms.MenuStrip TopMenuStrip;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage GraphTabPage;
        private System.Windows.Forms.TabPage PropertiesTabPage;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ArgumentsTextBox;
        private System.Windows.Forms.ComboBox GeneratorComboBox;
        private System.Windows.Forms.TextBox AdjencyTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox AlgorithmComboBox;
        private System.Windows.Forms.Button GeneratorButton;
        private System.Windows.Forms.TextBox CoordinatesTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ResetAlgorithmButton;
        private System.Windows.Forms.Button ApplyAlgorithmButton;
        private System.Windows.Forms.TabPage MetricxTabPage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToggleToolStripMenuItem;
        private System.Windows.Forms.ProgressBar MetricsProgressBar;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ConfigsTextBox;
        private System.ComponentModel.BackgroundWorker MetricsBackgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem SourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DestinationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenConfigDialog;
        private System.Windows.Forms.SaveFileDialog SaveConfigDialog;
        private System.Windows.Forms.ContextMenuStrip GraphContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem BeginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EndToolStripMenuItem;
        private System.Windows.Forms.ComboBox ModeComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.Timer MessageTimer;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MetadataToolStripMenuItem;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.TextBox MetadataTextBox;
        private System.Windows.Forms.DataGridView ResultsDataGridView;
        private System.Windows.Forms.NumericUpDown SeedNumeric;
        private System.Windows.Forms.Button RandomSeedButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlgorithmAVGPathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OptimalAVGPathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlgorithmAVGDeltaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OptimalAVGDeltaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlgorithmUnreachableColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OptimalUnreachableColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorColumn;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown IterationsNumeric;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox SeedsTextBox;
        private System.Windows.Forms.TextBox GlobalMetadataTextBox;
        private System.Windows.Forms.ToolStripMenuItem DistancesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DrawMetadataToolStripMenuItem;
    }
}


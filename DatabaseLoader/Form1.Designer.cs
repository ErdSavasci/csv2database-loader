namespace DatabaseLoader
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.exitButton = new MetroFramework.Controls.MetroButton();
            this.openButton = new MetroFramework.Controls.MetroButton();
            this.databaseNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.databaseNameLabel = new MetroFramework.Controls.MetroLabel();
            this.tableNameLabel = new MetroFramework.Controls.MetroLabel();
            this.connectButton = new MetroFramework.Controls.MetroButton();
            this.tableNameComboBox = new MetroFramework.Controls.MetroComboBox();
            this.connectionStatusLabel = new MaterialSkin.Controls.MaterialLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.commandExecutionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.insertButton = new MetroFramework.Controls.MetroButton();
            this.loadingCircle = new MRG.Controls.UI.LoadingCircle();
            this.loadingStatusLabel = new MaterialSkin.Controls.MaterialLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.columnsOfCsvTextBox = new MetroFramework.Controls.MetroTextBox();
            this.columnsOfCsvLabel = new MetroFramework.Controls.MetroLabel();
            this.selectedFileLink = new MetroFramework.Controls.MetroLink();
            this.clearSelectedFilePictureBox = new System.Windows.Forms.PictureBox();
            this.skipRowsTextBox = new MetroFramework.Controls.MetroTextBox();
            this.skipRowLabel = new MetroFramework.Controls.MetroLabel();
            this.inputDataLabel = new MetroFramework.Controls.MetroLabel();
            this.outputDataLabel = new MetroFramework.Controls.MetroLabel();
            this.columnsOfTableLabel = new MetroFramework.Controls.MetroLabel();
            this.columnsOfTableTextBox = new MetroFramework.Controls.MetroTextBox();
            this.performStepCheckBox = new MaterialSkin.Controls.MaterialCheckBox();
            this.parseDateTimeCheckBox = new MaterialSkin.Controls.MaterialCheckBox();
            this.addUniqueKeyCheckBox = new MaterialSkin.Controls.MaterialCheckBox();
            this.uniqueIndexPositionTextBox = new MetroFramework.Controls.MetroTextBox();
            this.dateTimeTypeCheckBox = new MaterialSkin.Controls.MaterialCheckBox();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clearSelectedFilePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Location = new System.Drawing.Point(703, 348);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(108, 33);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "Exit";
            this.exitButton.UseSelectable = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openButton.Location = new System.Drawing.Point(703, 63);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(108, 33);
            this.openButton.TabIndex = 2;
            this.openButton.Text = "Open";
            this.openButton.UseSelectable = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // databaseNameTextBox
            // 
            // 
            // 
            // 
            this.databaseNameTextBox.CustomButton.Image = null;
            this.databaseNameTextBox.CustomButton.Location = new System.Drawing.Point(151, 1);
            this.databaseNameTextBox.CustomButton.Name = "";
            this.databaseNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.databaseNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.databaseNameTextBox.CustomButton.TabIndex = 1;
            this.databaseNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.databaseNameTextBox.CustomButton.UseSelectable = true;
            this.databaseNameTextBox.CustomButton.Visible = false;
            this.databaseNameTextBox.Lines = new string[0];
            this.databaseNameTextBox.Location = new System.Drawing.Point(139, 73);
            this.databaseNameTextBox.MaxLength = 32767;
            this.databaseNameTextBox.Name = "databaseNameTextBox";
            this.databaseNameTextBox.PasswordChar = '\0';
            this.databaseNameTextBox.PromptText = "ex. MobileActivity";
            this.databaseNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.databaseNameTextBox.SelectedText = "";
            this.databaseNameTextBox.SelectionLength = 0;
            this.databaseNameTextBox.SelectionStart = 0;
            this.databaseNameTextBox.ShortcutsEnabled = true;
            this.databaseNameTextBox.Size = new System.Drawing.Size(173, 23);
            this.databaseNameTextBox.TabIndex = 3;
            this.databaseNameTextBox.UseSelectable = true;
            this.databaseNameTextBox.WaterMark = "ex. MobileActivity";
            this.databaseNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.databaseNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.databaseNameTextBox.TextChanged += new System.EventHandler(this.databaseNameTextBox_TextChanged);
            // 
            // databaseNameLabel
            // 
            this.databaseNameLabel.AutoSize = true;
            this.databaseNameLabel.Location = new System.Drawing.Point(23, 73);
            this.databaseNameLabel.Name = "databaseNameLabel";
            this.databaseNameLabel.Size = new System.Drawing.Size(110, 19);
            this.databaseNameLabel.TabIndex = 5;
            this.databaseNameLabel.Text = "Database Name: ";
            // 
            // tableNameLabel
            // 
            this.tableNameLabel.AutoSize = true;
            this.tableNameLabel.Location = new System.Drawing.Point(23, 118);
            this.tableNameLabel.Name = "tableNameLabel";
            this.tableNameLabel.Size = new System.Drawing.Size(86, 19);
            this.tableNameLabel.TabIndex = 6;
            this.tableNameLabel.Text = "Table Name: ";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(337, 72);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(79, 24);
            this.connectButton.TabIndex = 7;
            this.connectButton.Text = "Connect";
            this.connectButton.UseSelectable = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // tableNameComboBox
            // 
            this.tableNameComboBox.FormattingEnabled = true;
            this.tableNameComboBox.ItemHeight = 23;
            this.tableNameComboBox.Location = new System.Drawing.Point(139, 115);
            this.tableNameComboBox.Name = "tableNameComboBox";
            this.tableNameComboBox.PromptText = "Select table name";
            this.tableNameComboBox.Size = new System.Drawing.Size(173, 29);
            this.tableNameComboBox.TabIndex = 8;
            this.tableNameComboBox.UseSelectable = true;
            this.tableNameComboBox.SelectedIndexChanged += new System.EventHandler(this.tableNameComboBox_SelectedIndexChanged);
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Depth = 0;
            this.connectionStatusLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.connectionStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.connectionStatusLabel.Location = new System.Drawing.Point(426, 75);
            this.connectionStatusLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(133, 19);
            this.connectionStatusLabel.TabIndex = 10;
            this.connectionStatusLabel.Text = "Connection Status";
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.BackColor = System.Drawing.SystemColors.Highlight;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandExecutionStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(20, 394);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(794, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 12;
            this.statusStrip.Text = "statusStrip";
            // 
            // commandExecutionStatusLabel
            // 
            this.commandExecutionStatusLabel.Name = "commandExecutionStatusLabel";
            this.commandExecutionStatusLabel.Size = new System.Drawing.Size(99, 17);
            this.commandExecutionStatusLabel.Text = "Command Status";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.AutoSize = false;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripProgressBar.Size = new System.Drawing.Size(150, 16);
            this.toolStripProgressBar.Step = 1;
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // insertButton
            // 
            this.insertButton.Location = new System.Drawing.Point(337, 117);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(79, 24);
            this.insertButton.TabIndex = 13;
            this.insertButton.Text = "Insert";
            this.insertButton.UseSelectable = true;
            this.insertButton.Click += new System.EventHandler(this.insertButton_Click);
            // 
            // loadingCircle
            // 
            this.loadingCircle.Active = false;
            this.loadingCircle.Color = System.Drawing.Color.ForestGreen;
            this.loadingCircle.InnerCircleRadius = 5;
            this.loadingCircle.Location = new System.Drawing.Point(20, 348);
            this.loadingCircle.Name = "loadingCircle";
            this.loadingCircle.NumberSpoke = 12;
            this.loadingCircle.OuterCircleRadius = 11;
            this.loadingCircle.RotationSpeed = 100;
            this.loadingCircle.Size = new System.Drawing.Size(33, 27);
            this.loadingCircle.SpokeThickness = 2;
            this.loadingCircle.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle.TabIndex = 14;
            this.loadingCircle.Text = "loadingCircle";
            // 
            // loadingStatusLabel
            // 
            this.loadingStatusLabel.AutoSize = true;
            this.loadingStatusLabel.Depth = 0;
            this.loadingStatusLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.loadingStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.loadingStatusLabel.Location = new System.Drawing.Point(59, 351);
            this.loadingStatusLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.loadingStatusLabel.Name = "loadingStatusLabel";
            this.loadingStatusLabel.Size = new System.Drawing.Size(74, 19);
            this.loadingStatusLabel.TabIndex = 15;
            this.loadingStatusLabel.Text = "Loading...";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // columnsOfCsvTextBox
            // 
            // 
            // 
            // 
            this.columnsOfCsvTextBox.CustomButton.Image = null;
            this.columnsOfCsvTextBox.CustomButton.Location = new System.Drawing.Point(182, 1);
            this.columnsOfCsvTextBox.CustomButton.Name = "";
            this.columnsOfCsvTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.columnsOfCsvTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.columnsOfCsvTextBox.CustomButton.TabIndex = 1;
            this.columnsOfCsvTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.columnsOfCsvTextBox.CustomButton.UseSelectable = true;
            this.columnsOfCsvTextBox.CustomButton.Visible = false;
            this.columnsOfCsvTextBox.Lines = new string[0];
            this.columnsOfCsvTextBox.Location = new System.Drawing.Point(23, 237);
            this.columnsOfCsvTextBox.MaxLength = 32767;
            this.columnsOfCsvTextBox.Name = "columnsOfCsvTextBox";
            this.columnsOfCsvTextBox.PasswordChar = '\0';
            this.columnsOfCsvTextBox.PromptText = "ex. 1,2,3 or 1,2-9";
            this.columnsOfCsvTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.columnsOfCsvTextBox.SelectedText = "";
            this.columnsOfCsvTextBox.SelectionLength = 0;
            this.columnsOfCsvTextBox.SelectionStart = 0;
            this.columnsOfCsvTextBox.ShortcutsEnabled = true;
            this.columnsOfCsvTextBox.Size = new System.Drawing.Size(204, 23);
            this.columnsOfCsvTextBox.TabIndex = 16;
            this.columnsOfCsvTextBox.UseSelectable = true;
            this.columnsOfCsvTextBox.WaterMark = "ex. 1,2,3 or 1,2-9";
            this.columnsOfCsvTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.columnsOfCsvTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // columnsOfCsvLabel
            // 
            this.columnsOfCsvLabel.AutoSize = true;
            this.columnsOfCsvLabel.Location = new System.Drawing.Point(23, 206);
            this.columnsOfCsvLabel.Name = "columnsOfCsvLabel";
            this.columnsOfCsvLabel.Size = new System.Drawing.Size(60, 19);
            this.columnsOfCsvLabel.TabIndex = 17;
            this.columnsOfCsvLabel.Text = "Columns";
            // 
            // selectedFileLink
            // 
            this.selectedFileLink.Location = new System.Drawing.Point(703, 114);
            this.selectedFileLink.Name = "selectedFileLink";
            this.selectedFileLink.Size = new System.Drawing.Size(97, 23);
            this.selectedFileLink.TabIndex = 18;
            this.selectedFileLink.Text = "No File Selected";
            this.selectedFileLink.UseSelectable = true;
            this.selectedFileLink.Click += new System.EventHandler(this.selectedFileLink_Click);
            // 
            // clearSelectedFilePictureBox
            // 
            this.clearSelectedFilePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("clearSelectedFilePictureBox.Image")));
            this.clearSelectedFilePictureBox.Location = new System.Drawing.Point(799, 114);
            this.clearSelectedFilePictureBox.Name = "clearSelectedFilePictureBox";
            this.clearSelectedFilePictureBox.Size = new System.Drawing.Size(12, 23);
            this.clearSelectedFilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.clearSelectedFilePictureBox.TabIndex = 20;
            this.clearSelectedFilePictureBox.TabStop = false;
            this.clearSelectedFilePictureBox.Click += new System.EventHandler(this.clearSelectedFilePictureBox_Click);
            // 
            // skipRowsTextBox
            // 
            // 
            // 
            // 
            this.skipRowsTextBox.CustomButton.Image = null;
            this.skipRowsTextBox.CustomButton.Location = new System.Drawing.Point(182, 1);
            this.skipRowsTextBox.CustomButton.Name = "";
            this.skipRowsTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.skipRowsTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.skipRowsTextBox.CustomButton.TabIndex = 1;
            this.skipRowsTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.skipRowsTextBox.CustomButton.UseSelectable = true;
            this.skipRowsTextBox.CustomButton.Visible = false;
            this.skipRowsTextBox.Lines = new string[0];
            this.skipRowsTextBox.Location = new System.Drawing.Point(23, 306);
            this.skipRowsTextBox.MaxLength = 32767;
            this.skipRowsTextBox.Name = "skipRowsTextBox";
            this.skipRowsTextBox.PasswordChar = '\0';
            this.skipRowsTextBox.PromptText = "ex. 1,2,3 or 1,2-9";
            this.skipRowsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.skipRowsTextBox.SelectedText = "";
            this.skipRowsTextBox.SelectionLength = 0;
            this.skipRowsTextBox.SelectionStart = 0;
            this.skipRowsTextBox.ShortcutsEnabled = true;
            this.skipRowsTextBox.Size = new System.Drawing.Size(204, 23);
            this.skipRowsTextBox.TabIndex = 21;
            this.skipRowsTextBox.UseSelectable = true;
            this.skipRowsTextBox.WaterMark = "ex. 1,2,3 or 1,2-9";
            this.skipRowsTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.skipRowsTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // skipRowLabel
            // 
            this.skipRowLabel.AutoSize = true;
            this.skipRowLabel.Location = new System.Drawing.Point(23, 274);
            this.skipRowLabel.Name = "skipRowLabel";
            this.skipRowLabel.Size = new System.Drawing.Size(62, 19);
            this.skipRowLabel.TabIndex = 22;
            this.skipRowLabel.Text = "Skip Row";
            // 
            // inputDataLabel
            // 
            this.inputDataLabel.AutoSize = true;
            this.inputDataLabel.Location = new System.Drawing.Point(23, 171);
            this.inputDataLabel.Name = "inputDataLabel";
            this.inputDataLabel.Size = new System.Drawing.Size(69, 19);
            this.inputDataLabel.TabIndex = 23;
            this.inputDataLabel.Text = "Input Data";
            // 
            // outputDataLabel
            // 
            this.outputDataLabel.AutoSize = true;
            this.outputDataLabel.Location = new System.Drawing.Point(280, 173);
            this.outputDataLabel.Name = "outputDataLabel";
            this.outputDataLabel.Size = new System.Drawing.Size(81, 19);
            this.outputDataLabel.TabIndex = 24;
            this.outputDataLabel.Text = "Output Data";
            // 
            // columnsOfTableLabel
            // 
            this.columnsOfTableLabel.AutoSize = true;
            this.columnsOfTableLabel.Location = new System.Drawing.Point(280, 208);
            this.columnsOfTableLabel.Name = "columnsOfTableLabel";
            this.columnsOfTableLabel.Size = new System.Drawing.Size(60, 19);
            this.columnsOfTableLabel.TabIndex = 25;
            this.columnsOfTableLabel.Text = "Columns";
            // 
            // columnsOfTableTextBox
            // 
            // 
            // 
            // 
            this.columnsOfTableTextBox.CustomButton.Image = null;
            this.columnsOfTableTextBox.CustomButton.Location = new System.Drawing.Point(177, 1);
            this.columnsOfTableTextBox.CustomButton.Name = "";
            this.columnsOfTableTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.columnsOfTableTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.columnsOfTableTextBox.CustomButton.TabIndex = 1;
            this.columnsOfTableTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.columnsOfTableTextBox.CustomButton.UseSelectable = true;
            this.columnsOfTableTextBox.CustomButton.Visible = false;
            this.columnsOfTableTextBox.Lines = new string[0];
            this.columnsOfTableTextBox.Location = new System.Drawing.Point(280, 238);
            this.columnsOfTableTextBox.MaxLength = 32767;
            this.columnsOfTableTextBox.Name = "columnsOfTableTextBox";
            this.columnsOfTableTextBox.PasswordChar = '\0';
            this.columnsOfTableTextBox.PromptText = "ex. 1,2,3 or 1,2-9";
            this.columnsOfTableTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.columnsOfTableTextBox.SelectedText = "";
            this.columnsOfTableTextBox.SelectionLength = 0;
            this.columnsOfTableTextBox.SelectionStart = 0;
            this.columnsOfTableTextBox.ShortcutsEnabled = true;
            this.columnsOfTableTextBox.Size = new System.Drawing.Size(199, 23);
            this.columnsOfTableTextBox.TabIndex = 26;
            this.columnsOfTableTextBox.UseSelectable = true;
            this.columnsOfTableTextBox.WaterMark = "ex. 1,2,3 or 1,2-9";
            this.columnsOfTableTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.columnsOfTableTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // performStepCheckBox
            // 
            this.performStepCheckBox.Depth = 0;
            this.performStepCheckBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.performStepCheckBox.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.performStepCheckBox.Location = new System.Drawing.Point(117, 204);
            this.performStepCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.performStepCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.performStepCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.performStepCheckBox.Name = "performStepCheckBox";
            this.performStepCheckBox.Ripple = true;
            this.performStepCheckBox.Size = new System.Drawing.Size(110, 25);
            this.performStepCheckBox.TabIndex = 27;
            this.performStepCheckBox.Text = "Perform Step";
            this.performStepCheckBox.UseVisualStyleBackColor = true;
            this.performStepCheckBox.CheckStateChanged += new System.EventHandler(this.performStepCheckBox_CheckStateChanged);
            // 
            // parseDateTimeCheckBox
            // 
            this.parseDateTimeCheckBox.Depth = 0;
            this.parseDateTimeCheckBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.parseDateTimeCheckBox.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.parseDateTimeCheckBox.Location = new System.Drawing.Point(277, 307);
            this.parseDateTimeCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.parseDateTimeCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.parseDateTimeCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.parseDateTimeCheckBox.Name = "parseDateTimeCheckBox";
            this.parseDateTimeCheckBox.Ripple = true;
            this.parseDateTimeCheckBox.Size = new System.Drawing.Size(139, 25);
            this.parseDateTimeCheckBox.TabIndex = 28;
            this.parseDateTimeCheckBox.Text = "Parse DateTime?";
            this.parseDateTimeCheckBox.UseVisualStyleBackColor = true;
            this.parseDateTimeCheckBox.CheckStateChanged += new System.EventHandler(this.parseDateTimeCheckBox_CheckStateChanged);
            // 
            // addUniqueKeyCheckBox
            // 
            this.addUniqueKeyCheckBox.Depth = 0;
            this.addUniqueKeyCheckBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.addUniqueKeyCheckBox.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.addUniqueKeyCheckBox.Location = new System.Drawing.Point(277, 274);
            this.addUniqueKeyCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.addUniqueKeyCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.addUniqueKeyCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.addUniqueKeyCheckBox.Name = "addUniqueKeyCheckBox";
            this.addUniqueKeyCheckBox.Ripple = true;
            this.addUniqueKeyCheckBox.Size = new System.Drawing.Size(139, 25);
            this.addUniqueKeyCheckBox.TabIndex = 29;
            this.addUniqueKeyCheckBox.Text = "Add Unique Key";
            this.addUniqueKeyCheckBox.UseVisualStyleBackColor = true;
            this.addUniqueKeyCheckBox.CheckStateChanged += new System.EventHandler(this.addUniqueKeyCheckBox_CheckStateChanged);
            // 
            // uniqueIndexPositionTextBox
            // 
            // 
            // 
            // 
            this.uniqueIndexPositionTextBox.CustomButton.Image = null;
            this.uniqueIndexPositionTextBox.CustomButton.Location = new System.Drawing.Point(37, 1);
            this.uniqueIndexPositionTextBox.CustomButton.Name = "";
            this.uniqueIndexPositionTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.uniqueIndexPositionTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.uniqueIndexPositionTextBox.CustomButton.TabIndex = 1;
            this.uniqueIndexPositionTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.uniqueIndexPositionTextBox.CustomButton.UseSelectable = true;
            this.uniqueIndexPositionTextBox.CustomButton.Visible = false;
            this.uniqueIndexPositionTextBox.Lines = new string[0];
            this.uniqueIndexPositionTextBox.Location = new System.Drawing.Point(420, 274);
            this.uniqueIndexPositionTextBox.MaxLength = 32767;
            this.uniqueIndexPositionTextBox.Name = "uniqueIndexPositionTextBox";
            this.uniqueIndexPositionTextBox.PasswordChar = '\0';
            this.uniqueIndexPositionTextBox.PromptText = "ex. 1";
            this.uniqueIndexPositionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.uniqueIndexPositionTextBox.SelectedText = "";
            this.uniqueIndexPositionTextBox.SelectionLength = 0;
            this.uniqueIndexPositionTextBox.SelectionStart = 0;
            this.uniqueIndexPositionTextBox.ShortcutsEnabled = true;
            this.uniqueIndexPositionTextBox.Size = new System.Drawing.Size(59, 23);
            this.uniqueIndexPositionTextBox.TabIndex = 30;
            this.uniqueIndexPositionTextBox.UseSelectable = true;
            this.uniqueIndexPositionTextBox.WaterMark = "ex. 1";
            this.uniqueIndexPositionTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.uniqueIndexPositionTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // dateTimeTypeCheckBox
            // 
            this.dateTimeTypeCheckBox.Depth = 0;
            this.dateTimeTypeCheckBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.dateTimeTypeCheckBox.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.dateTimeTypeCheckBox.Location = new System.Drawing.Point(417, 306);
            this.dateTimeTypeCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.dateTimeTypeCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.dateTimeTypeCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.dateTimeTypeCheckBox.Name = "dateTimeTypeCheckBox";
            this.dateTimeTypeCheckBox.Ripple = true;
            this.dateTimeTypeCheckBox.Size = new System.Drawing.Size(139, 25);
            this.dateTimeTypeCheckBox.TabIndex = 31;
            this.dateTimeTypeCheckBox.Text = "Is INT";
            this.dateTimeTypeCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 436);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimeTypeCheckBox);
            this.Controls.Add(this.uniqueIndexPositionTextBox);
            this.Controls.Add(this.addUniqueKeyCheckBox);
            this.Controls.Add(this.parseDateTimeCheckBox);
            this.Controls.Add(this.performStepCheckBox);
            this.Controls.Add(this.columnsOfTableTextBox);
            this.Controls.Add(this.columnsOfTableLabel);
            this.Controls.Add(this.outputDataLabel);
            this.Controls.Add(this.inputDataLabel);
            this.Controls.Add(this.skipRowLabel);
            this.Controls.Add(this.skipRowsTextBox);
            this.Controls.Add(this.clearSelectedFilePictureBox);
            this.Controls.Add(this.selectedFileLink);
            this.Controls.Add(this.columnsOfCsvLabel);
            this.Controls.Add(this.columnsOfCsvTextBox);
            this.Controls.Add(this.loadingStatusLabel);
            this.Controls.Add(this.loadingCircle);
            this.Controls.Add(this.insertButton);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.connectionStatusLabel);
            this.Controls.Add(this.tableNameComboBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.tableNameLabel);
            this.Controls.Add(this.databaseNameLabel);
            this.Controls.Add(this.databaseNameTextBox);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.exitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "DatabaseLoader v1.0";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clearSelectedFilePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton exitButton;
        private MetroFramework.Controls.MetroButton openButton;
        private MetroFramework.Controls.MetroTextBox databaseNameTextBox;
        private MetroFramework.Controls.MetroLabel databaseNameLabel;
        private MetroFramework.Controls.MetroLabel tableNameLabel;
        private MetroFramework.Controls.MetroButton connectButton;
        private MetroFramework.Controls.MetroComboBox tableNameComboBox;
        private MaterialSkin.Controls.MaterialLabel connectionStatusLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel commandExecutionStatusLabel;
        private MetroFramework.Controls.MetroButton insertButton;
        private MRG.Controls.UI.LoadingCircle loadingCircle;
        private MaterialSkin.Controls.MaterialLabel loadingStatusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private MetroFramework.Controls.MetroTextBox columnsOfCsvTextBox;
        private MetroFramework.Controls.MetroLabel columnsOfCsvLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private MetroFramework.Controls.MetroLink selectedFileLink;
        private System.Windows.Forms.PictureBox clearSelectedFilePictureBox;
        private MetroFramework.Controls.MetroTextBox skipRowsTextBox;
        private MetroFramework.Controls.MetroLabel skipRowLabel;
        private MetroFramework.Controls.MetroLabel inputDataLabel;
        private MetroFramework.Controls.MetroLabel outputDataLabel;
        private MetroFramework.Controls.MetroLabel columnsOfTableLabel;
        private MetroFramework.Controls.MetroTextBox columnsOfTableTextBox;
        private MaterialSkin.Controls.MaterialCheckBox performStepCheckBox;
        private MaterialSkin.Controls.MaterialCheckBox parseDateTimeCheckBox;
        private MaterialSkin.Controls.MaterialCheckBox addUniqueKeyCheckBox;
        private MetroFramework.Controls.MetroTextBox uniqueIndexPositionTextBox;
        private MaterialSkin.Controls.MaterialCheckBox dateTimeTypeCheckBox;
    }
}


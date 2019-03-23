namespace FingerprintRecognition
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonBinaryMode = new System.Windows.Forms.RadioButton();
            this.radioButtonASCIIMode = new System.Windows.Forms.RadioButton();
            this.comboBoxBaudrate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.panelConnectionStatus = new System.Windows.Forms.Panel();
            this.comboBoxPortCom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFingerprintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFingerprintToMatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxOriginal = new System.Windows.Forms.PictureBox();
            this.pictureBoxNew = new System.Windows.Forms.PictureBox();
            this.buttonScann = new System.Windows.Forms.Button();
            this.labelScannInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matchWithDbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNew)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.comboBoxBaudrate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonConnect);
            this.groupBox1.Controls.Add(this.panelConnectionStatus);
            this.groupBox1.Controls.Add(this.comboBoxPortCom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 90);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Connection status";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonBinaryMode);
            this.groupBox2.Controls.Add(this.radioButtonASCIIMode);
            this.groupBox2.Location = new System.Drawing.Point(148, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(72, 65);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // radioButtonBinaryMode
            // 
            this.radioButtonBinaryMode.AutoSize = true;
            this.radioButtonBinaryMode.Checked = true;
            this.radioButtonBinaryMode.Location = new System.Drawing.Point(6, 19);
            this.radioButtonBinaryMode.Name = "radioButtonBinaryMode";
            this.radioButtonBinaryMode.Size = new System.Drawing.Size(54, 17);
            this.radioButtonBinaryMode.TabIndex = 3;
            this.radioButtonBinaryMode.TabStop = true;
            this.radioButtonBinaryMode.Text = "Binary";
            this.radioButtonBinaryMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonASCIIMode
            // 
            this.radioButtonASCIIMode.AutoSize = true;
            this.radioButtonASCIIMode.Location = new System.Drawing.Point(6, 42);
            this.radioButtonASCIIMode.Name = "radioButtonASCIIMode";
            this.radioButtonASCIIMode.Size = new System.Drawing.Size(52, 17);
            this.radioButtonASCIIMode.TabIndex = 2;
            this.radioButtonASCIIMode.Text = "ASCII";
            this.radioButtonASCIIMode.UseVisualStyleBackColor = true;
            // 
            // comboBoxBaudrate
            // 
            this.comboBoxBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaudrate.FormattingEnabled = true;
            this.comboBoxBaudrate.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.comboBoxBaudrate.Location = new System.Drawing.Point(65, 58);
            this.comboBoxBaudrate.Name = "comboBoxBaudrate";
            this.comboBoxBaudrate.Size = new System.Drawing.Size(77, 21);
            this.comboBoxBaudrate.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baudrate";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(229, 52);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(153, 29);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // panelConnectionStatus
            // 
            this.panelConnectionStatus.BackColor = System.Drawing.Color.Red;
            this.panelConnectionStatus.Location = new System.Drawing.Point(324, 27);
            this.panelConnectionStatus.Name = "panelConnectionStatus";
            this.panelConnectionStatus.Size = new System.Drawing.Size(58, 16);
            this.panelConnectionStatus.TabIndex = 3;
            // 
            // comboBoxPortCom
            // 
            this.comboBoxPortCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPortCom.FormattingEnabled = true;
            this.comboBoxPortCom.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.comboBoxPortCom.Location = new System.Drawing.Point(65, 27);
            this.comboBoxPortCom.Name = "comboBoxPortCom";
            this.comboBoxPortCom.Size = new System.Drawing.Size(77, 21);
            this.comboBoxPortCom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port COM";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFingerprintToolStripMenuItem,
            this.openFingerprintToMatchToolStripMenuItem,
            this.matchToolStripMenuItem,
            this.matchWithDbToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openFingerprintToolStripMenuItem
            // 
            this.openFingerprintToolStripMenuItem.Name = "openFingerprintToolStripMenuItem";
            this.openFingerprintToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.openFingerprintToolStripMenuItem.Text = "Open fingerprint";
            this.openFingerprintToolStripMenuItem.Click += new System.EventHandler(this.openFingerprintToolStripMenuItem_Click);
            // 
            // openFingerprintToMatchToolStripMenuItem
            // 
            this.openFingerprintToMatchToolStripMenuItem.Name = "openFingerprintToMatchToolStripMenuItem";
            this.openFingerprintToMatchToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.openFingerprintToMatchToolStripMenuItem.Text = "Open fingerprint to match";
            this.openFingerprintToMatchToolStripMenuItem.Click += new System.EventHandler(this.openFingerprintToMatchToolStripMenuItem_Click);
            // 
            // matchToolStripMenuItem
            // 
            this.matchToolStripMenuItem.Name = "matchToolStripMenuItem";
            this.matchToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.matchToolStripMenuItem.Text = "Match";
            this.matchToolStripMenuItem.Click += new System.EventHandler(this.matchToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(210, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxOriginal.BackColor = System.Drawing.Color.White;
            this.pictureBoxOriginal.Location = new System.Drawing.Point(0, 120);
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.Size = new System.Drawing.Size(310, 321);
            this.pictureBoxOriginal.TabIndex = 2;
            this.pictureBoxOriginal.TabStop = false;
            // 
            // pictureBoxNew
            // 
            this.pictureBoxNew.BackColor = System.Drawing.Color.White;
            this.pictureBoxNew.Location = new System.Drawing.Point(314, 120);
            this.pictureBoxNew.Name = "pictureBoxNew";
            this.pictureBoxNew.Size = new System.Drawing.Size(310, 321);
            this.pictureBoxNew.TabIndex = 3;
            this.pictureBoxNew.TabStop = false;
            // 
            // buttonScann
            // 
            this.buttonScann.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonScann.Enabled = false;
            this.buttonScann.Location = new System.Drawing.Point(10, 54);
            this.buttonScann.Name = "buttonScann";
            this.buttonScann.Size = new System.Drawing.Size(194, 27);
            this.buttonScann.TabIndex = 9;
            this.buttonScann.Text = "Scann";
            this.buttonScann.UseVisualStyleBackColor = true;
            this.buttonScann.Click += new System.EventHandler(this.buttonScann_Click);
            // 
            // labelScannInfo
            // 
            this.labelScannInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScannInfo.AutoSize = true;
            this.labelScannInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScannInfo.Location = new System.Drawing.Point(3, 8);
            this.labelScannInfo.Name = "labelScannInfo";
            this.labelScannInfo.Size = new System.Drawing.Size(181, 17);
            this.labelScannInfo.TabIndex = 10;
            this.labelScannInfo.Text = "Place finger on scanner";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 90);
            this.panel1.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonScann);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(402, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(222, 90);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scann settings";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.labelScannInfo);
            this.panel2.Location = new System.Drawing.Point(10, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 33);
            this.panel2.TabIndex = 11;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDbToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // createDbToolStripMenuItem
            // 
            this.createDbToolStripMenuItem.Name = "createDbToolStripMenuItem";
            this.createDbToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.createDbToolStripMenuItem.Text = "Create db";
            this.createDbToolStripMenuItem.Click += new System.EventHandler(this.createDbToolStripMenuItem_Click);
            // 
            // matchWithDbToolStripMenuItem
            // 
            this.matchWithDbToolStripMenuItem.Name = "matchWithDbToolStripMenuItem";
            this.matchWithDbToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.matchWithDbToolStripMenuItem.Text = "Match with db";
            this.matchWithDbToolStripMenuItem.Click += new System.EventHandler(this.matchWithDbToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBoxOriginal);
            this.Controls.Add(this.pictureBoxNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fingerprint Recognition";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNew)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label labelScannInfo;
        private System.Windows.Forms.Button buttonScann;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonBinaryMode;
        private System.Windows.Forms.RadioButton radioButtonASCIIMode;
        private System.Windows.Forms.ComboBox comboBoxBaudrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Panel panelConnectionStatus;
        private System.Windows.Forms.ComboBox comboBoxPortCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.PictureBox pictureBoxNew;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem openFingerprintToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFingerprintToMatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matchWithDbToolStripMenuItem;
    }
}


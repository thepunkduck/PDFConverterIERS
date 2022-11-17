namespace Docotron
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
            this.buttonCONVERT = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneFilePerPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overwriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxWithLabelDPI = new IERSInterface.ToolStripTextBoxWithLabel();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pNGToolStripMenuItem = new IERSInterface.ToolStripRadioButtonMenuItem();
            this.jPGToolStripMenuItem = new IERSInterface.ToolStripRadioButtonMenuItem();
            this.bMPToolStripMenuItem = new IERSInterface.ToolStripRadioButtonMenuItem();
            this.tIFFToolStripMenuItem = new IERSInterface.ToolStripRadioButtonMenuItem();
            this.gIFToolStripMenuItem = new IERSInterface.ToolStripRadioButtonMenuItem();
            this.previewImageSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallToolStripMenuItem = new IERSInterface.ToolStripRadioButtonMenuItem();
            this.mediumToolStripMenuItem = new IERSInterface.ToolStripRadioButtonMenuItem();
            this.largeToolStripMenuItem = new IERSInterface.ToolStripRadioButtonMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileFieldAndBrowser1 = new IERSInterface.FileFieldAndBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelPAGES = new System.Windows.Forms.Label();
            this.textBoxPAGES = new System.Windows.Forms.TextBox();
            this.checkBoxPAGES = new System.Windows.Forms.CheckBox();
            this.doubleBuffer1 = new ChartLib.DoubleBuffer();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.buttonCANCEL = new System.Windows.Forms.Button();
            this.browseToOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCONVERT
            // 
            this.buttonCONVERT.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonCONVERT.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonCONVERT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCONVERT.Location = new System.Drawing.Point(0, 74);
            this.buttonCONVERT.Name = "buttonCONVERT";
            this.buttonCONVERT.Size = new System.Drawing.Size(528, 47);
            this.buttonCONVERT.TabIndex = 0;
            this.buttonCONVERT.Text = "Convert";
            this.buttonCONVERT.UseVisualStyleBackColor = false;
            this.buttonCONVERT.Click += new System.EventHandler(this.buttonCONVERT_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(528, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToolStripMenuItem,
            this.openToolStripMenuItem,
            this.browseToOutputToolStripMenuItem,
            this.toolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.convertToolStripMenuItem.Text = "Convert";
            this.convertToolStripMenuItem.Click += new System.EventHandler(this.buttonCONVERT_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Checked = true;
            this.toolsToolStripMenuItem.CheckOnClick = true;
            this.toolsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneFilePerPageToolStripMenuItem,
            this.overwriteToolStripMenuItem,
            this.toolStripTextBoxWithLabelDPI,
            this.formatToolStripMenuItem,
            this.previewImageSizeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.toolsToolStripMenuItem.Text = "Options";
            // 
            // oneFilePerPageToolStripMenuItem
            // 
            this.oneFilePerPageToolStripMenuItem.Checked = true;
            this.oneFilePerPageToolStripMenuItem.CheckOnClick = true;
            this.oneFilePerPageToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.oneFilePerPageToolStripMenuItem.Name = "oneFilePerPageToolStripMenuItem";
            this.oneFilePerPageToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.oneFilePerPageToolStripMenuItem.Text = "Multiple files (one per page)";
            // 
            // overwriteToolStripMenuItem
            // 
            this.overwriteToolStripMenuItem.CheckOnClick = true;
            this.overwriteToolStripMenuItem.Name = "overwriteToolStripMenuItem";
            this.overwriteToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.overwriteToolStripMenuItem.Text = "Overwrite";
            // 
            // toolStripTextBoxWithLabelDPI
            // 
            this.toolStripTextBoxWithLabelDPI.BackColor = System.Drawing.Color.White;
            this.toolStripTextBoxWithLabelDPI.LabelText = "ABC:";
            this.toolStripTextBoxWithLabelDPI.Name = "toolStripTextBoxWithLabelDPI";
            this.toolStripTextBoxWithLabelDPI.Size = new System.Drawing.Size(171, 21);
            this.toolStripTextBoxWithLabelDPI.Text = "toolStripTextBoxWithLabel1";
            this.toolStripTextBoxWithLabelDPI.TextBoxText = "";
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pNGToolStripMenuItem,
            this.jPGToolStripMenuItem,
            this.bMPToolStripMenuItem,
            this.tIFFToolStripMenuItem,
            this.gIFToolStripMenuItem});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.formatToolStripMenuItem.Text = "Format";
            // 
            // pNGToolStripMenuItem
            // 
            this.pNGToolStripMenuItem.CheckOnClick = true;
            this.pNGToolStripMenuItem.Name = "pNGToolStripMenuItem";
            this.pNGToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.pNGToolStripMenuItem.Text = "PNG";
            // 
            // jPGToolStripMenuItem
            // 
            this.jPGToolStripMenuItem.CheckOnClick = true;
            this.jPGToolStripMenuItem.Name = "jPGToolStripMenuItem";
            this.jPGToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.jPGToolStripMenuItem.Text = "JPG";
            // 
            // bMPToolStripMenuItem
            // 
            this.bMPToolStripMenuItem.CheckOnClick = true;
            this.bMPToolStripMenuItem.Name = "bMPToolStripMenuItem";
            this.bMPToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.bMPToolStripMenuItem.Text = "BMP";
            // 
            // tIFFToolStripMenuItem
            // 
            this.tIFFToolStripMenuItem.CheckOnClick = true;
            this.tIFFToolStripMenuItem.Name = "tIFFToolStripMenuItem";
            this.tIFFToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.tIFFToolStripMenuItem.Text = "TIFF";
            // 
            // gIFToolStripMenuItem
            // 
            this.gIFToolStripMenuItem.CheckOnClick = true;
            this.gIFToolStripMenuItem.Name = "gIFToolStripMenuItem";
            this.gIFToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.gIFToolStripMenuItem.Text = "GIF";
            // 
            // previewImageSizeToolStripMenuItem
            // 
            this.previewImageSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.largeToolStripMenuItem});
            this.previewImageSizeToolStripMenuItem.Name = "previewImageSizeToolStripMenuItem";
            this.previewImageSizeToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.previewImageSizeToolStripMenuItem.Text = "Preview Image Size";
            // 
            // smallToolStripMenuItem
            // 
            this.smallToolStripMenuItem.Checked = true;
            this.smallToolStripMenuItem.CheckOnClick = true;
            this.smallToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smallToolStripMenuItem.Name = "smallToolStripMenuItem";
            this.smallToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.smallToolStripMenuItem.Text = "Small";
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.CheckOnClick = true;
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.mediumToolStripMenuItem.Text = "Medium";
            // 
            // largeToolStripMenuItem
            // 
            this.largeToolStripMenuItem.CheckOnClick = true;
            this.largeToolStripMenuItem.Name = "largeToolStripMenuItem";
            this.largeToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.largeToolStripMenuItem.Text = "Large";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // fileFieldAndBrowser1
            // 
            this.fileFieldAndBrowser1.AllowDrop = true;
            this.fileFieldAndBrowser1.BrowseFoldersOnly = false;
            this.fileFieldAndBrowser1.CheckFileExists = true;
            this.fileFieldAndBrowser1.Dock = System.Windows.Forms.DockStyle.Top;
            this.fileFieldAndBrowser1.FileName = "";
            this.fileFieldAndBrowser1.Filter = "All Files|*.*";
            this.fileFieldAndBrowser1.FilterIndex = 1;
            this.fileFieldAndBrowser1.IsSave = false;
            this.fileFieldAndBrowser1.LocalFolder = null;
            this.fileFieldAndBrowser1.Location = new System.Drawing.Point(0, 24);
            this.fileFieldAndBrowser1.Margin = new System.Windows.Forms.Padding(0);
            this.fileFieldAndBrowser1.Multiselect = false;
            this.fileFieldAndBrowser1.Name = "fileFieldAndBrowser1";
            this.fileFieldAndBrowser1.Padding = new System.Windows.Forms.Padding(0, 3, 2, 0);
            this.fileFieldAndBrowser1.RecentFileRegistry = null;
            this.fileFieldAndBrowser1.Size = new System.Drawing.Size(528, 25);
            this.fileFieldAndBrowser1.TabIndex = 1;
            this.fileFieldAndBrowser1.TextOffset = 60;
            this.fileFieldAndBrowser1.TextWidth = 60;
            this.fileFieldAndBrowser1.Title = "File/Folder";
            this.fileFieldAndBrowser1.Uid = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelPAGES);
            this.panel1.Controls.Add(this.textBoxPAGES);
            this.panel1.Controls.Add(this.checkBoxPAGES);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 25);
            this.panel1.TabIndex = 8;
            // 
            // labelPAGES
            // 
            this.labelPAGES.AutoSize = true;
            this.labelPAGES.Location = new System.Drawing.Point(180, 5);
            this.labelPAGES.Name = "labelPAGES";
            this.labelPAGES.Size = new System.Drawing.Size(108, 13);
            this.labelPAGES.TabIndex = 2;
            this.labelPAGES.Text = "Convert these pages:";
            // 
            // textBoxPAGES
            // 
            this.textBoxPAGES.Location = new System.Drawing.Point(289, 1);
            this.textBoxPAGES.Name = "textBoxPAGES";
            this.textBoxPAGES.Size = new System.Drawing.Size(138, 20);
            this.textBoxPAGES.TabIndex = 1;
            // 
            // checkBoxPAGES
            // 
            this.checkBoxPAGES.AutoSize = true;
            this.checkBoxPAGES.Checked = true;
            this.checkBoxPAGES.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPAGES.Location = new System.Drawing.Point(66, 4);
            this.checkBoxPAGES.Name = "checkBoxPAGES";
            this.checkBoxPAGES.Size = new System.Drawing.Size(108, 17);
            this.checkBoxPAGES.TabIndex = 0;
            this.checkBoxPAGES.Text = "Convert all pages";
            this.checkBoxPAGES.UseVisualStyleBackColor = true;
            this.checkBoxPAGES.CheckStateChanged += new System.EventHandler(this.checkBoxPAGES_CheckStateChanged);
            // 
            // doubleBuffer1
            // 
            this.doubleBuffer1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.doubleBuffer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBuffer1.Freeze = false;
            this.doubleBuffer1.Location = new System.Drawing.Point(0, 168);
            this.doubleBuffer1.Name = "doubleBuffer1";
            this.doubleBuffer1.Size = new System.Drawing.Size(511, 424);
            this.doubleBuffer1.TabIndex = 1;
            this.doubleBuffer1.Text = "doubleBuffer1";
            this.doubleBuffer1.PaintEvent += new System.Windows.Forms.PaintEventHandler(this.doubleBuffer1_PaintEvent);
            this.doubleBuffer1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.doubleBuffer1_MouseDown);
            this.doubleBuffer1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.doubleBuffer1_MouseMove);
            this.doubleBuffer1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.doubleBuffer1_MouseUp);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(511, 168);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 424);
            this.vScrollBar1.TabIndex = 9;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged);
            // 
            // buttonCANCEL
            // 
            this.buttonCANCEL.BackColor = System.Drawing.Color.Salmon;
            this.buttonCANCEL.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonCANCEL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCANCEL.Location = new System.Drawing.Point(0, 121);
            this.buttonCANCEL.Name = "buttonCANCEL";
            this.buttonCANCEL.Size = new System.Drawing.Size(528, 47);
            this.buttonCANCEL.TabIndex = 10;
            this.buttonCANCEL.Text = "Conversion in progress... [Press to Cancel]";
            this.buttonCANCEL.UseVisualStyleBackColor = false;
            this.buttonCANCEL.Visible = false;
            this.buttonCANCEL.Click += new System.EventHandler(this.buttonCANCEL_Click);
            // 
            // browseToOutputToolStripMenuItem
            // 
            this.browseToOutputToolStripMenuItem.Name = "browseToOutputToolStripMenuItem";
            this.browseToOutputToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.browseToOutputToolStripMenuItem.Text = "Browse to Output...";
            this.browseToOutputToolStripMenuItem.Click += new System.EventHandler(this.browseToOutputToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 592);
            this.Controls.Add(this.doubleBuffer1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.buttonCANCEL);
            this.Controls.Add(this.buttonCONVERT);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fileFieldAndBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Docotron";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCONVERT;
        private IERSInterface.FileFieldAndBrowser fileFieldAndBrowser1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneFilePerPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overwriteToolStripMenuItem;
        private IERSInterface.ToolStripTextBoxWithLabel toolStripTextBoxWithLabelDPI;
        private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
        private IERSInterface.ToolStripRadioButtonMenuItem pNGToolStripMenuItem;
        private IERSInterface.ToolStripRadioButtonMenuItem jPGToolStripMenuItem;
        private IERSInterface.ToolStripRadioButtonMenuItem bMPToolStripMenuItem;
        private IERSInterface.ToolStripRadioButtonMenuItem tIFFToolStripMenuItem;
        private IERSInterface.ToolStripRadioButtonMenuItem gIFToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelPAGES;
        private System.Windows.Forms.TextBox textBoxPAGES;
        private System.Windows.Forms.CheckBox checkBoxPAGES;
        private ChartLib.DoubleBuffer doubleBuffer1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.ToolStripMenuItem previewImageSizeToolStripMenuItem;
        private IERSInterface.ToolStripRadioButtonMenuItem smallToolStripMenuItem;
        private IERSInterface.ToolStripRadioButtonMenuItem mediumToolStripMenuItem;
        private IERSInterface.ToolStripRadioButtonMenuItem largeToolStripMenuItem;
        private System.Windows.Forms.Button buttonCANCEL;
        private System.Windows.Forms.ToolStripMenuItem browseToOutputToolStripMenuItem;
    }
}


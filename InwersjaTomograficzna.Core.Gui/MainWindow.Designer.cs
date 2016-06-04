namespace InwersjaTomograficzna.Core.Gui
{
    partial class MainWindow
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.plikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wczytajDaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszDaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mockDataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.opcjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algorytmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.przetwarzanieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SignalsDataContainer = new System.Windows.Forms.SplitContainer();
            this.ReturnDataPanel = new System.Windows.Forms.SplitContainer();
            this.RayDencityAndInwersionPanel = new System.Windows.Forms.SplitContainer();
            this.SignalChartPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SignalsDataContainer)).BeginInit();
            this.SignalsDataContainer.Panel1.SuspendLayout();
            this.SignalsDataContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnDataPanel)).BeginInit();
            this.ReturnDataPanel.Panel1.SuspendLayout();
            this.ReturnDataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RayDencityAndInwersionPanel)).BeginInit();
            this.RayDencityAndInwersionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 739);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikiToolStripMenuItem,
            this.opcjeToolStripMenuItem,
            this.przetwarzanieToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1264, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "Menu";
            // 
            // plikiToolStripMenuItem
            // 
            this.plikiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wczytajDaneToolStripMenuItem,
            this.zapiszDaneToolStripMenuItem,
            this.mockDataMenu});
            this.plikiToolStripMenuItem.Name = "plikiToolStripMenuItem";
            this.plikiToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.plikiToolStripMenuItem.Text = "Pliki";
            // 
            // wczytajDaneToolStripMenuItem
            // 
            this.wczytajDaneToolStripMenuItem.Name = "wczytajDaneToolStripMenuItem";
            this.wczytajDaneToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.wczytajDaneToolStripMenuItem.Text = "Wczytaj Dane";
            // 
            // zapiszDaneToolStripMenuItem
            // 
            this.zapiszDaneToolStripMenuItem.Name = "zapiszDaneToolStripMenuItem";
            this.zapiszDaneToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.zapiszDaneToolStripMenuItem.Text = "Zapisz Dane";
            // 
            // mockDataMenu
            // 
            this.mockDataMenu.Name = "mockDataMenu";
            this.mockDataMenu.Size = new System.Drawing.Size(188, 22);
            this.mockDataMenu.Text = "Otwórz Mock Danych";
            // 
            // opcjeToolStripMenuItem
            // 
            this.opcjeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algorytmToolStripMenuItem});
            this.opcjeToolStripMenuItem.Name = "opcjeToolStripMenuItem";
            this.opcjeToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.opcjeToolStripMenuItem.Text = "Opcje";
            // 
            // algorytmToolStripMenuItem
            // 
            this.algorytmToolStripMenuItem.Name = "algorytmToolStripMenuItem";
            this.algorytmToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.algorytmToolStripMenuItem.Text = "Algorytm";
            // 
            // przetwarzanieToolStripMenuItem
            // 
            this.przetwarzanieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem});
            this.przetwarzanieToolStripMenuItem.Name = "przetwarzanieToolStripMenuItem";
            this.przetwarzanieToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.przetwarzanieToolStripMenuItem.Text = "Przetwarzanie";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SignalsDataContainer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ReturnDataPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 715);
            this.splitContainer1.SplitterDistance = 429;
            this.splitContainer1.TabIndex = 2;
            // 
            // SignalsDataContainer
            // 
            this.SignalsDataContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SignalsDataContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SignalsDataContainer.Location = new System.Drawing.Point(0, 0);
            this.SignalsDataContainer.Name = "SignalsDataContainer";
            this.SignalsDataContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SignalsDataContainer.Panel1
            // 
            this.SignalsDataContainer.Panel1.Controls.Add(this.SignalChartPanel);
            this.SignalsDataContainer.Size = new System.Drawing.Size(429, 715);
            this.SignalsDataContainer.SplitterDistance = 253;
            this.SignalsDataContainer.TabIndex = 0;
            // 
            // ReturnDataPanel
            // 
            this.ReturnDataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReturnDataPanel.Location = new System.Drawing.Point(0, 0);
            this.ReturnDataPanel.Name = "ReturnDataPanel";
            this.ReturnDataPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ReturnDataPanel.Panel1
            // 
            this.ReturnDataPanel.Panel1.Controls.Add(this.RayDencityAndInwersionPanel);
            this.ReturnDataPanel.Size = new System.Drawing.Size(829, 713);
            this.ReturnDataPanel.SplitterDistance = 330;
            this.ReturnDataPanel.TabIndex = 0;
            // 
            // RayDencityAndInwersionPanel
            // 
            this.RayDencityAndInwersionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RayDencityAndInwersionPanel.Location = new System.Drawing.Point(0, 0);
            this.RayDencityAndInwersionPanel.Name = "RayDencityAndInwersionPanel";
            // 
            // RayDencityAndInwersionPanel.Panel1
            // 
            this.RayDencityAndInwersionPanel.Panel1.Resize += new System.EventHandler(this.RayDencityAndInwersionPanel_Panel1_Resize);
            this.RayDencityAndInwersionPanel.Size = new System.Drawing.Size(829, 330);
            this.RayDencityAndInwersionPanel.SplitterDistance = 405;
            this.RayDencityAndInwersionPanel.TabIndex = 0;
            // 
            // SignalChartPanel
            // 
            this.SignalChartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SignalChartPanel.Location = new System.Drawing.Point(0, 0);
            this.SignalChartPanel.Name = "SignalChartPanel";
            this.SignalChartPanel.Size = new System.Drawing.Size(427, 251);
            this.SignalChartPanel.TabIndex = 0;
            this.SignalChartPanel.Resize += new System.EventHandler(this.SignalChartPanel_Resize);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.SignalsDataContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SignalsDataContainer)).EndInit();
            this.SignalsDataContainer.ResumeLayout(false);
            this.ReturnDataPanel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReturnDataPanel)).EndInit();
            this.ReturnDataPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RayDencityAndInwersionPanel)).EndInit();
            this.RayDencityAndInwersionPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem plikiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wczytajDaneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszDaneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algorytmToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer SignalsDataContainer;
        private System.Windows.Forms.SplitContainer ReturnDataPanel;
        private System.Windows.Forms.SplitContainer RayDencityAndInwersionPanel;
        private System.Windows.Forms.ToolStripMenuItem mockDataMenu;
        private System.Windows.Forms.ToolStripMenuItem przetwarzanieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel SignalChartPanel;
    }
}
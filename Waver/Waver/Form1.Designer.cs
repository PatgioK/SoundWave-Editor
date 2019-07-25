using System;

namespace Waver
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphRecorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Window = new System.Windows.Forms.Button();
            this.FilterBut = new System.Windows.Forms.Button();
            this.HammingBut = new System.Windows.Forms.RadioButton();
            this.TriangleBut = new System.Windows.Forms.RadioButton();
            this.highPass = new System.Windows.Forms.RadioButton();
            this.lowPass = new System.Windows.Forms.RadioButton();
            this.noWindowButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.compressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToCompressedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCompressedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.recorderToolStripMenuItem,
            this.compressionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(845, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // recorderToolStripMenuItem
            // 
            this.recorderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openRecorderToolStripMenuItem,
            this.graphRecorderToolStripMenuItem});
            this.recorderToolStripMenuItem.Name = "recorderToolStripMenuItem";
            this.recorderToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.recorderToolStripMenuItem.Text = "Recorder";
            // 
            // openRecorderToolStripMenuItem
            // 
            this.openRecorderToolStripMenuItem.Name = "openRecorderToolStripMenuItem";
            this.openRecorderToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.openRecorderToolStripMenuItem.Text = "Open Recorder";
            this.openRecorderToolStripMenuItem.Click += new System.EventHandler(this.openRecorderToolStripMenuItem_Click);
            // 
            // graphRecorderToolStripMenuItem
            // 
            this.graphRecorderToolStripMenuItem.Name = "graphRecorderToolStripMenuItem";
            this.graphRecorderToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.graphRecorderToolStripMenuItem.Text = "Graph Recorder";
            this.graphRecorderToolStripMenuItem.Click += new System.EventHandler(this.graphRecorderToolStripMenuItem_Click);
            // 
            // chart1
            // 
            chartArea1.AxisX.ScaleView.Zoomable = false;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(186, 31);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(647, 183);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            chartArea2.AxisX.ScaleView.Zoomable = false;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartArea2";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend2";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(186, 220);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea2";
            series2.Legend = "Legend2";
            series2.Name = "series2";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(647, 183);
            this.chart2.TabIndex = 2;
            this.chart2.Text = "chart2";
            // 
            // Window
            // 
            this.Window.Location = new System.Drawing.Point(34, 31);
            this.Window.Name = "Window";
            this.Window.Size = new System.Drawing.Size(75, 23);
            this.Window.TabIndex = 5;
            this.Window.Text = "Window";
            this.Window.UseVisualStyleBackColor = true;
            this.Window.Click += new System.EventHandler(this.Window_Click);
            // 
            // FilterBut
            // 
            this.FilterBut.Location = new System.Drawing.Point(34, 380);
            this.FilterBut.Name = "FilterBut";
            this.FilterBut.Size = new System.Drawing.Size(75, 23);
            this.FilterBut.TabIndex = 6;
            this.FilterBut.Text = "FilterBut";
            this.FilterBut.UseVisualStyleBackColor = true;
            this.FilterBut.Click += new System.EventHandler(this.Filter_Click);
            // 
            // HammingBut
            // 
            this.HammingBut.AutoSize = true;
            this.HammingBut.Location = new System.Drawing.Point(34, 60);
            this.HammingBut.Name = "HammingBut";
            this.HammingBut.Size = new System.Drawing.Size(141, 21);
            this.HammingBut.TabIndex = 7;
            this.HammingBut.TabStop = true;
            this.HammingBut.Text = "Hamming Window";
            this.HammingBut.UseVisualStyleBackColor = true;
            // 
            // TriangleBut
            // 
            this.TriangleBut.AutoSize = true;
            this.TriangleBut.Location = new System.Drawing.Point(34, 87);
            this.TriangleBut.Name = "TriangleBut";
            this.TriangleBut.Size = new System.Drawing.Size(134, 21);
            this.TriangleBut.TabIndex = 8;
            this.TriangleBut.TabStop = true;
            this.TriangleBut.Text = "Triangle Window";
            this.TriangleBut.UseVisualStyleBackColor = true;
            // 
            // noFilter
            // 
            this.highPass.AutoSize = true;
            this.highPass.Location = new System.Drawing.Point(34, 326);
            this.highPass.Name = "Highpass";
            this.highPass.Size = new System.Drawing.Size(103, 21);
            this.highPass.TabIndex = 9;
            this.highPass.TabStop = true;
            this.highPass.Text = "High Pass";
            this.highPass.UseVisualStyleBackColor = true;
            // 
            // lowPass
            // 
            this.lowPass.AutoSize = true;
            this.lowPass.Location = new System.Drawing.Point(34, 353);
            this.lowPass.Name = "lowPass";
            this.lowPass.Size = new System.Drawing.Size(89, 21);
            this.lowPass.TabIndex = 10;
            this.lowPass.TabStop = true;
            this.lowPass.Text = "Low Pass";
            this.lowPass.UseVisualStyleBackColor = true;
            // 
            // noWindowButton
            // 
            this.noWindowButton.AutoSize = true;
            this.noWindowButton.Location = new System.Drawing.Point(34, 115);
            this.noWindowButton.Name = "noWindowButton";
            this.noWindowButton.Size = new System.Drawing.Size(100, 21);
            this.noWindowButton.TabIndex = 11;
            this.noWindowButton.TabStop = true;
            this.noWindowButton.Text = "No Window";
            this.noWindowButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Threads for DFT";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(7, 73);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(94, 21);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "4 Threads";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 50);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(94, 21);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "2 Threads";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(87, 21);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1 Thread";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Benchmark";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // compressionToolStripMenuItem
            // 
            this.compressionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToCompressedFileToolStripMenuItem,
            this.openCompressedFileToolStripMenuItem});
            this.compressionToolStripMenuItem.Name = "compressionToolStripMenuItem";
            this.compressionToolStripMenuItem.Size = new System.Drawing.Size(107, 24);
            this.compressionToolStripMenuItem.Text = "Compression";
            // 
            // saveToCompressedFileToolStripMenuItem
            // 
            this.saveToCompressedFileToolStripMenuItem.Name = "saveToCompressedFileToolStripMenuItem";
            this.saveToCompressedFileToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.saveToCompressedFileToolStripMenuItem.Text = "save to compressed file";
            this.saveToCompressedFileToolStripMenuItem.Click += new System.EventHandler(this.saveToCompressedFileToolStripMenuItem_Click);
            // 
            // openCompressedFileToolStripMenuItem
            // 
            this.openCompressedFileToolStripMenuItem.Name = "openCompressedFileToolStripMenuItem";
            this.openCompressedFileToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.openCompressedFileToolStripMenuItem.Text = "open compressed file";
            this.openCompressedFileToolStripMenuItem.Click += new System.EventHandler(this.openCompressedFileToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.noWindowButton);
            this.Controls.Add(this.lowPass);
            this.Controls.Add(this.highPass);
            this.Controls.Add(this.TriangleBut);
            this.Controls.Add(this.HammingBut);
            this.Controls.Add(this.FilterBut);
            this.Controls.Add(this.Window);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRecorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphRecorderToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Button Window;
        private System.Windows.Forms.Button FilterBut;
        private System.Windows.Forms.RadioButton HammingBut;
        private System.Windows.Forms.RadioButton TriangleBut;
        private System.Windows.Forms.RadioButton highPass;
        private System.Windows.Forms.RadioButton lowPass;
        private System.Windows.Forms.RadioButton noWindowButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem compressionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToCompressedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCompressedFileToolStripMenuItem;
    }
}


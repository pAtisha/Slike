namespace Slike
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip = new ContextMenuStrip(components);
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem1 = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            convertToolStripMenuItem = new ToolStripMenuItem();
            rGBToYUVToolStripMenuItem = new ToolStripMenuItem();
            downsampleToolStripMenuItem = new ToolStripMenuItem();
            downsampleUAndVToolStripMenuItem = new ToolStripMenuItem();
            compressToolStripMenuItem = new ToolStripMenuItem();
            compressImageToolStripMenuItem = new ToolStripMenuItem();
            filtersToolStripMenuItem = new ToolStripMenuItem();
            contrastToolStripMenuItem = new ToolStripMenuItem();
            sharpenToolStripMenuItem = new ToolStripMenuItem();
            edgeDetectToolStripMenuItem = new ToolStripMenuItem();
            randomJitterToolStripMenuItem = new ToolStripMenuItem();
            txtBoxFilterValue = new TextBox();
            labelFilter = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(20, 20);
            contextMenuStrip.Name = "contextMenuStrip1";
            contextMenuStrip.Size = new Size(61, 4);
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, convertToolStripMenuItem, downsampleToolStripMenuItem, compressToolStripMenuItem, filtersToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem1, saveToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem1
            // 
            openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            openToolStripMenuItem1.Size = new Size(103, 22);
            openToolStripMenuItem1.Text = "Open";
            openToolStripMenuItem1.Click += openToolStripMenuItem1_Click_1;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(103, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(103, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // convertToolStripMenuItem
            // 
            convertToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { rGBToYUVToolStripMenuItem });
            convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            convertToolStripMenuItem.Size = new Size(61, 20);
            convertToolStripMenuItem.Text = "Convert";
            // 
            // rGBToYUVToolStripMenuItem
            // 
            rGBToYUVToolStripMenuItem.Name = "rGBToYUVToolStripMenuItem";
            rGBToYUVToolStripMenuItem.Size = new Size(136, 22);
            rGBToYUVToolStripMenuItem.Text = "RGB To YUV";
            rGBToYUVToolStripMenuItem.Click += rGBToYUVToolStripMenuItem_Click_1;
            // 
            // downsampleToolStripMenuItem
            // 
            downsampleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { downsampleUAndVToolStripMenuItem });
            downsampleToolStripMenuItem.Name = "downsampleToolStripMenuItem";
            downsampleToolStripMenuItem.Size = new Size(88, 20);
            downsampleToolStripMenuItem.Text = "Downsample";
            // 
            // downsampleUAndVToolStripMenuItem
            // 
            downsampleUAndVToolStripMenuItem.Name = "downsampleUAndVToolStripMenuItem";
            downsampleUAndVToolStripMenuItem.Size = new Size(187, 22);
            downsampleUAndVToolStripMenuItem.Text = "Downsample U and V";
            downsampleUAndVToolStripMenuItem.Click += downsampleUAndVToolStripMenuItem_Click;
            // 
            // compressToolStripMenuItem
            // 
            compressToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { compressImageToolStripMenuItem });
            compressToolStripMenuItem.Name = "compressToolStripMenuItem";
            compressToolStripMenuItem.Size = new Size(72, 20);
            compressToolStripMenuItem.Text = "Compress";
            // 
            // compressImageToolStripMenuItem
            // 
            compressImageToolStripMenuItem.Name = "compressImageToolStripMenuItem";
            compressImageToolStripMenuItem.Size = new Size(163, 22);
            compressImageToolStripMenuItem.Text = "Compress image";
            compressImageToolStripMenuItem.Click += compressImageToolStripMenuItem_Click;
            // 
            // filtersToolStripMenuItem
            // 
            filtersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { contrastToolStripMenuItem, sharpenToolStripMenuItem, edgeDetectToolStripMenuItem, randomJitterToolStripMenuItem });
            filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            filtersToolStripMenuItem.Size = new Size(50, 20);
            filtersToolStripMenuItem.Text = "Filters";
            // 
            // contrastToolStripMenuItem
            // 
            contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            contrastToolStripMenuItem.Size = new Size(180, 22);
            contrastToolStripMenuItem.Text = "Contrast";
            contrastToolStripMenuItem.Click += contrastToolStripMenuItem_Click;
            // 
            // sharpenToolStripMenuItem
            // 
            sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            sharpenToolStripMenuItem.Size = new Size(180, 22);
            sharpenToolStripMenuItem.Text = "Sharpen";
            sharpenToolStripMenuItem.Click += sharpenToolStripMenuItem_Click;
            // 
            // edgeDetectToolStripMenuItem
            // 
            edgeDetectToolStripMenuItem.Name = "edgeDetectToolStripMenuItem";
            edgeDetectToolStripMenuItem.Size = new Size(180, 22);
            edgeDetectToolStripMenuItem.Text = "Edge detect ";
            edgeDetectToolStripMenuItem.Click += edgeDetectToolStripMenuItem_Click;
            // 
            // randomJitterToolStripMenuItem
            // 
            randomJitterToolStripMenuItem.Name = "randomJitterToolStripMenuItem";
            randomJitterToolStripMenuItem.Size = new Size(180, 22);
            randomJitterToolStripMenuItem.Text = "Random jitter";
            randomJitterToolStripMenuItem.Click += randomJitterToolStripMenuItem_Click;
            // 
            // txtBoxFilterValue
            // 
            txtBoxFilterValue.Location = new Point(299, 199);
            txtBoxFilterValue.Margin = new Padding(3, 2, 3, 2);
            txtBoxFilterValue.Name = "txtBoxFilterValue";
            txtBoxFilterValue.Size = new Size(216, 23);
            txtBoxFilterValue.TabIndex = 2;
            txtBoxFilterValue.Visible = false;
            // 
            // labelFilter
            // 
            labelFilter.AutoSize = true;
            labelFilter.Location = new Point(299, 182);
            labelFilter.Name = "labelFilter";
            labelFilter.Size = new Size(197, 15);
            labelFilter.TabIndex = 3;
            labelFilter.Text = "Contrast Value (min: -100, max: 100)";
            labelFilter.Visible = false;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(299, 224);
            btnOk.Margin = new Padding(3, 2, 3, 2);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(93, 22);
            btnOk.TabIndex = 4;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Visible = false;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(422, 224);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(93, 22);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Visible = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(labelFilter);
            Controls.Add(txtBoxFilterValue);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip contextMenuStrip;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem convertToolStripMenuItem;
        private ToolStripMenuItem rGBToYUVToolStripMenuItem;
        private ToolStripMenuItem downsampleToolStripMenuItem;
        private ToolStripMenuItem downsampleUAndVToolStripMenuItem;
        private ToolStripMenuItem compressToolStripMenuItem;
        private ToolStripMenuItem compressImageToolStripMenuItem;
        private ToolStripMenuItem filtersToolStripMenuItem;
        private ToolStripMenuItem contrastToolStripMenuItem;
        private ToolStripMenuItem sharpenToolStripMenuItem;
        private ToolStripMenuItem edgeDetectToolStripMenuItem;
        private ToolStripMenuItem randomJitterToolStripMenuItem;
        private TextBox txtBoxFilterValue;
        private Label labelFilter;
        private Button btnOk;
        private Button btnCancel;
    }
}
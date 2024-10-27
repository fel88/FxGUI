namespace FxGUI
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            panel1 = new System.Windows.Forms.Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            sample1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sample2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sample3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sample4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sample5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(elementHost1, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(990, 546);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // elementHost1
            // 
            elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            elementHost1.Location = new System.Drawing.Point(499, 3);
            elementHost1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            elementHost1.Name = "elementHost1";
            elementHost1.Size = new System.Drawing.Size(487, 540);
            elementHost1.TabIndex = 2;
            elementHost1.Text = "elementHost1";
            // 
            // panel1
            // 
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(4, 3);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(487, 540);
            panel1.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 20;
            timer1.Tick += timer1_Tick;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 571);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip1.Size = new System.Drawing.Size(990, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripDropDownButton1 });
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(990, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { sample1ToolStripMenuItem, sample2ToolStripMenuItem, sample3ToolStripMenuItem, sample4ToolStripMenuItem, sample5ToolStripMenuItem });
            toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new System.Drawing.Size(63, 22);
            toolStripDropDownButton1.Text = "samples";
            // 
            // sample1ToolStripMenuItem
            // 
            sample1ToolStripMenuItem.Name = "sample1ToolStripMenuItem";
            sample1ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            sample1ToolStripMenuItem.Text = "sample 1";
            sample1ToolStripMenuItem.Click += sample1ToolStripMenuItem_Click;
            // 
            // sample2ToolStripMenuItem
            // 
            sample2ToolStripMenuItem.Name = "sample2ToolStripMenuItem";
            sample2ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            sample2ToolStripMenuItem.Text = "sample 2";
            sample2ToolStripMenuItem.Click += sample2ToolStripMenuItem_Click;
            // 
            // sample3ToolStripMenuItem
            // 
            sample3ToolStripMenuItem.Name = "sample3ToolStripMenuItem";
            sample3ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            sample3ToolStripMenuItem.Text = "sample 3";
            sample3ToolStripMenuItem.Click += sample3ToolStripMenuItem_Click;
            // 
            // sample4ToolStripMenuItem
            // 
            sample4ToolStripMenuItem.Name = "sample4ToolStripMenuItem";
            sample4ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            sample4ToolStripMenuItem.Text = "sample 4";
            sample4ToolStripMenuItem.Click += sample4ToolStripMenuItem_Click;
            // 
            // sample5ToolStripMenuItem
            // 
            sample5ToolStripMenuItem.Name = "sample5ToolStripMenuItem";
            sample5ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            sample5ToolStripMenuItem.Text = "sample 5";
            sample5ToolStripMenuItem.Click += sample5ToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(990, 593);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FxGUI";
            Load += Form1_Load;
            Leave += Form1_Leave;
            tableLayoutPanel1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem sample1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sample2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sample3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sample4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sample5ToolStripMenuItem;
    }
}


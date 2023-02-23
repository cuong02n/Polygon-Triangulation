using System.ComponentModel;
using System.Windows.Forms;

namespace Polygon_Triangulation {
    partial class playForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.mouse_info = new System.Windows.Forms.Label();
            this.timer1 = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseClick);
            this.splitContainer1.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseMove);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.splitContainer1.Panel2.Controls.Add(this.MessageLabel);
            this.splitContainer1.Panel2.Controls.Add(this.SizeLabel);
            this.splitContainer1.Panel2.Controls.Add(this.clear);
            this.splitContainer1.Panel2.Controls.Add(this.start);
            this.splitContainer1.Panel2.Controls.Add(this.mouse_info);
            this.splitContainer1.Size = new System.Drawing.Size(1348, 721);
            this.splitContainer1.SplitterDistance = 1015;
            this.splitContainer1.TabIndex = 0;
            // 
            // MessageLabel
            // 
            this.MessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(3, 240);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(326, 472);
            this.MessageLabel.TabIndex = 4;
            this.MessageLabel.Text = "message";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SizeLabel
            // 
            this.SizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SizeLabel.Location = new System.Drawing.Point(124, 40);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(100, 37);
            this.SizeLabel.TabIndex = 3;
            this.SizeLabel.Text = "0";
            this.SizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clear
            // 
            this.clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clear.Location = new System.Drawing.Point(206, 161);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(68, 30);
            this.clear.TabIndex = 2;
            this.clear.Text = "Clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.clear_MouseClick);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(87, 161);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(68, 30);
            this.start.TabIndex = 1;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.MouseClick += new System.Windows.Forms.MouseEventHandler(this.start_MouseClick);
            // 
            // mouse_info
            // 
            this.mouse_info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mouse_info.Location = new System.Drawing.Point(74, 96);
            this.mouse_info.Name = "mouse_info";
            this.mouse_info.Size = new System.Drawing.Size(200, 23);
            this.mouse_info.TabIndex = 0;
            this.mouse_info.Text = "Vị trí chuột :";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300D;
            this.timer1.SynchronizingObject = this;

            // 
            // playForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.splitContainer1);
            this.MaximumSize = new System.Drawing.Size(1366, 768);
            this.MinimumSize = new System.Drawing.Size(1366, 768);
            this.Name = "playForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polygon Triangulation";
            this.Click += new System.EventHandler(this.playForm_Click);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.playForm_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.playForm_MouseMove);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Timers.Timer timer1;
        // private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label MessageLabel;

        private System.Windows.Forms.Label SizeLabel;

        private System.Windows.Forms.Button clear;

        private System.Windows.Forms.Button start;

        private System.Windows.Forms.Label mouse_info;

        private System.Windows.Forms.SplitContainer splitContainer1;

        #endregion
    }
}
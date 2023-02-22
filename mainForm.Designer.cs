namespace Polygon_Triangulation {
    partial class mainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.monotone_btn = new System.Windows.Forms.Button();
            this.ear_clip_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // monotone_btn
            // 
            this.monotone_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.monotone_btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.monotone_btn.Location = new System.Drawing.Point(300, 141);
            this.monotone_btn.Name = "monotone_btn";
            this.monotone_btn.Size = new System.Drawing.Size(200, 100);
            this.monotone_btn.TabIndex = 0;
            this.monotone_btn.Text = "Using monotone";
            this.monotone_btn.UseVisualStyleBackColor = true;
            this.monotone_btn.Click += new System.EventHandler(this.using_monotone_btn);
            // 
            // ear_clip_btn
            // 
            this.ear_clip_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ear_clip_btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ear_clip_btn.Location = new System.Drawing.Point(300, 301);
            this.ear_clip_btn.Name = "ear_clip_btn";
            this.ear_clip_btn.Size = new System.Drawing.Size(200, 100);
            this.ear_clip_btn.TabIndex = 1;
            this.ear_clip_btn.Text = "Using Ear Clipping\r\n";
            this.ear_clip_btn.UseVisualStyleBackColor = true;
            this.ear_clip_btn.Click += new System.EventHandler(this.ear_clip_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.ear_clip_btn);
            this.Controls.Add(this.monotone_btn);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polygon Triangulation";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button ear_clip_btn;

        private System.Windows.Forms.Button monotone_btn;

        #endregion
    }
}
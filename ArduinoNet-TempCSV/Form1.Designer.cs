namespace ArduinoNet_TempCSV
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
            this.tracker = new WinControls.Tracker();
            this.rtbConOut = new System.Windows.Forms.RichTextBox();
            this.cbBlockingSave = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tracker
            // 
            this.tracker.BackColor = System.Drawing.Color.Navy;
            this.tracker.Dock = System.Windows.Forms.DockStyle.Top;
            this.tracker.ForeColor = System.Drawing.Color.Transparent;
            this.tracker.Location = new System.Drawing.Point(0, 0);
            this.tracker.Name = "tracker";
            this.tracker.Size = new System.Drawing.Size(897, 191);
            this.tracker.TabIndex = 0;
            this.tracker.Text = "tracker1";
            // 
            // rtbConOut
            // 
            this.rtbConOut.BackColor = System.Drawing.Color.MidnightBlue;
            this.rtbConOut.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtbConOut.ForeColor = System.Drawing.Color.Yellow;
            this.rtbConOut.Location = new System.Drawing.Point(0, 191);
            this.rtbConOut.Name = "rtbConOut";
            this.rtbConOut.Size = new System.Drawing.Size(897, 108);
            this.rtbConOut.TabIndex = 1;
            this.rtbConOut.Text = "";
            // 
            // cbBlockingSave
            // 
            this.cbBlockingSave.AutoSize = true;
            this.cbBlockingSave.Checked = true;
            this.cbBlockingSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBlockingSave.Location = new System.Drawing.Point(12, 306);
            this.cbBlockingSave.Name = "cbBlockingSave";
            this.cbBlockingSave.Size = new System.Drawing.Size(117, 17);
            this.cbBlockingSave.TabIndex = 2;
            this.cbBlockingSave.Text = "Save with Blocking";
            this.cbBlockingSave.UseVisualStyleBackColor = true;
            this.cbBlockingSave.CheckedChanged += new System.EventHandler(this.cbBlockingSave_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(12, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "When unchecked you can open the saved file while it is still being used.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 350);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbBlockingSave);
            this.Controls.Add(this.rtbConOut);
            this.Controls.Add(this.tracker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Save Arduino Data as CSV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinControls.Tracker tracker;
        private System.Windows.Forms.RichTextBox rtbConOut;
        private System.Windows.Forms.CheckBox cbBlockingSave;
        private System.Windows.Forms.Label label1;
    }
}


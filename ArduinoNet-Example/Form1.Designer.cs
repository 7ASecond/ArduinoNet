namespace ArduinoNet_Example
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
            this.rtbConOut = new System.Windows.Forms.RichTextBox();
            this.tracker = new WinControls.Tracker();
            this.SuspendLayout();
            // 
            // rtbConOut
            // 
            this.rtbConOut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbConOut.Location = new System.Drawing.Point(0, 171);
            this.rtbConOut.Name = "rtbConOut";
            this.rtbConOut.Size = new System.Drawing.Size(680, 90);
            this.rtbConOut.TabIndex = 0;
            this.rtbConOut.Text = "";
            // 
            // tracker
            // 
            this.tracker.BackColor = System.Drawing.SystemColors.WindowText;
            this.tracker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tracker.Grid = 5;
            this.tracker.Location = new System.Drawing.Point(0, 0);
            this.tracker.LowerRange = 0;
            this.tracker.Name = "tracker";
            this.tracker.RefreshingTime = WinControls.Tracker.eRefresh.Medium;
            this.tracker.Size = new System.Drawing.Size(680, 171);
            this.tracker.TabIndex = 1;
            this.tracker.Text = "tracker1";
            this.tracker.UpperRange = 100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 261);
            this.Controls.Add(this.tracker);
            this.Controls.Add(this.rtbConOut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Arduino Temperature Sensor Demonstration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbConOut;
        private WinControls.Tracker tracker;
    }
}


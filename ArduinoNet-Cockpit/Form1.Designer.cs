using System.ComponentModel;

namespace ArduinoNet_Cockpit
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.verticalSpeed = new ArduinoNet_Cockpit.CockpitControls.VerticalSpeedIndicatorInstrumentControl();
            this.turnCoordinator = new ArduinoNet_Cockpit.CockpitControls.TurnCoordinatorInstrumentControl();
            this.heading = new ArduinoNet_Cockpit.CockpitControls.HeadingIndicatorInstrumentControl();
            this.attitude = new ArduinoNet_Cockpit.CockpitControls.AttitudeIndicatorInstrumentControl();
            this.altimeter = new ArduinoNet_Cockpit.CockpitControls.AltimeterInstrumentControl();
            this.airSpeed = new ArduinoNet_Cockpit.CockpitControls.AirSpeedIndicatorInstrumentControl();
            this.btnStartEngines = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbConOut
            // 
            this.rtbConOut.BackColor = System.Drawing.Color.Black;
            this.rtbConOut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbConOut.ForeColor = System.Drawing.Color.Lime;
            this.rtbConOut.Location = new System.Drawing.Point(0, 590);
            this.rtbConOut.Name = "rtbConOut";
            this.rtbConOut.Size = new System.Drawing.Size(791, 149);
            this.rtbConOut.TabIndex = 6;
            this.rtbConOut.Text = "";
            // 
            // verticalSpeed
            // 
            this.verticalSpeed.Location = new System.Drawing.Point(268, 13);
            this.verticalSpeed.Name = "verticalSpeed";
            this.verticalSpeed.Size = new System.Drawing.Size(250, 250);
            this.verticalSpeed.TabIndex = 5;
            this.verticalSpeed.Text = "verticalSpeedIndicatorInstrumentControl1";
            // 
            // turnCoordinator
            // 
            this.turnCoordinator.Location = new System.Drawing.Point(268, 269);
            this.turnCoordinator.Name = "turnCoordinator";
            this.turnCoordinator.Size = new System.Drawing.Size(250, 250);
            this.turnCoordinator.TabIndex = 4;
            this.turnCoordinator.Text = "turnCoordinatorInstrumentControl1";
            // 
            // heading
            // 
            this.heading.Location = new System.Drawing.Point(524, 270);
            this.heading.Name = "heading";
            this.heading.Size = new System.Drawing.Size(250, 250);
            this.heading.TabIndex = 3;
            this.heading.Text = "headingIndicatorInstrumentControl1";
            // 
            // attitude
            // 
            this.attitude.Location = new System.Drawing.Point(12, 269);
            this.attitude.Name = "attitude";
            this.attitude.Size = new System.Drawing.Size(250, 250);
            this.attitude.TabIndex = 2;
            this.attitude.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // altimeter
            // 
            this.altimeter.Location = new System.Drawing.Point(524, 14);
            this.altimeter.Name = "altimeter";
            this.altimeter.Size = new System.Drawing.Size(250, 250);
            this.altimeter.TabIndex = 1;
            this.altimeter.Text = "altimeterInstrumentControl1";
            // 
            // airSpeed
            // 
            this.airSpeed.Location = new System.Drawing.Point(12, 13);
            this.airSpeed.Name = "airSpeed";
            this.airSpeed.Size = new System.Drawing.Size(250, 250);
            this.airSpeed.TabIndex = 0;
            this.airSpeed.Text = "airSpeedIndicatorInstrumentControl1";
            // 
            // btnStartEngines
            // 
            this.btnStartEngines.BackColor = System.Drawing.Color.Black;
            this.btnStartEngines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartEngines.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStartEngines.Location = new System.Drawing.Point(681, 561);
            this.btnStartEngines.Name = "btnStartEngines";
            this.btnStartEngines.Size = new System.Drawing.Size(93, 23);
            this.btnStartEngines.TabIndex = 7;
            this.btnStartEngines.Text = "Start Engines";
            this.btnStartEngines.UseVisualStyleBackColor = false;
            this.btnStartEngines.Click += new System.EventHandler(this.btnStartEngines_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 739);
            this.Controls.Add(this.btnStartEngines);
            this.Controls.Add(this.rtbConOut);
            this.Controls.Add(this.verticalSpeed);
            this.Controls.Add(this.turnCoordinator);
            this.Controls.Add(this.heading);
            this.Controls.Add(this.attitude);
            this.Controls.Add(this.altimeter);
            this.Controls.Add(this.airSpeed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Arduino Cockpit Sensors";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CockpitControls.AirSpeedIndicatorInstrumentControl airSpeed;
        private CockpitControls.AltimeterInstrumentControl altimeter;
        private CockpitControls.AttitudeIndicatorInstrumentControl attitude;
        private CockpitControls.HeadingIndicatorInstrumentControl heading;
        private CockpitControls.TurnCoordinatorInstrumentControl turnCoordinator;
        private CockpitControls.VerticalSpeedIndicatorInstrumentControl verticalSpeed;
        private System.Windows.Forms.RichTextBox rtbConOut;
        private System.Windows.Forms.Button btnStartEngines;
    }
}


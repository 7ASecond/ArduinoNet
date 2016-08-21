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
            this.btnStartEngines = new System.Windows.Forms.Button();
            this.btnRunWithoutDemo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAltitude = new System.Windows.Forms.Label();
            this.lblVerticalSpeed = new System.Windows.Forms.Label();
            this.lblAirSpeed = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblTurnCoordinator = new System.Windows.Forms.Label();
            this.lblAttitude = new System.Windows.Forms.Label();
            this.btnAirSpeedReturnToZero = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.verticalSpeed = new ArduinoNet_Cockpit.CockpitControls.VerticalSpeedIndicatorInstrumentControl();
            this.turnCoordinator = new ArduinoNet_Cockpit.CockpitControls.TurnCoordinatorInstrumentControl();
            this.heading = new ArduinoNet_Cockpit.CockpitControls.HeadingIndicatorInstrumentControl();
            this.attitude = new ArduinoNet_Cockpit.CockpitControls.AttitudeIndicatorInstrumentControl();
            this.altimeter = new ArduinoNet_Cockpit.CockpitControls.AltimeterInstrumentControl();
            this.airSpeed = new ArduinoNet_Cockpit.CockpitControls.AirSpeedIndicatorInstrumentControl();
            this.btnPitchResetToZero = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbConOut
            // 
            this.rtbConOut.BackColor = System.Drawing.Color.Black;
            this.rtbConOut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbConOut.ForeColor = System.Drawing.Color.Lime;
            this.rtbConOut.Location = new System.Drawing.Point(0, 655);
            this.rtbConOut.Name = "rtbConOut";
            this.rtbConOut.Size = new System.Drawing.Size(791, 149);
            this.rtbConOut.TabIndex = 6;
            this.rtbConOut.Text = "";
            // 
            // btnStartEngines
            // 
            this.btnStartEngines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartEngines.BackColor = System.Drawing.Color.Black;
            this.btnStartEngines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartEngines.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStartEngines.Location = new System.Drawing.Point(681, 626);
            this.btnStartEngines.Name = "btnStartEngines";
            this.btnStartEngines.Size = new System.Drawing.Size(93, 23);
            this.btnStartEngines.TabIndex = 7;
            this.btnStartEngines.Text = "Demo";
            this.btnStartEngines.UseVisualStyleBackColor = false;
            this.btnStartEngines.Click += new System.EventHandler(this.btnStartEngines_Click);
            // 
            // btnRunWithoutDemo
            // 
            this.btnRunWithoutDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunWithoutDemo.BackColor = System.Drawing.Color.Black;
            this.btnRunWithoutDemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunWithoutDemo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRunWithoutDemo.Location = new System.Drawing.Point(516, 626);
            this.btnRunWithoutDemo.Name = "btnRunWithoutDemo";
            this.btnRunWithoutDemo.Size = new System.Drawing.Size(159, 23);
            this.btnRunWithoutDemo.TabIndex = 8;
            this.btnRunWithoutDemo.Text = "Display Arduino Sensor Data";
            this.btnRunWithoutDemo.UseVisualStyleBackColor = false;
            this.btnRunWithoutDemo.Click += new System.EventHandler(this.btnRunWithoutDemo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(83, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Airspeed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(330, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Vertical Speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(603, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Altitude";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(90, 591);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Attitude";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(315, 591);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Turn Coordinator";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(603, 591);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Heading";
            // 
            // lblAltitude
            // 
            this.lblAltitude.AutoSize = true;
            this.lblAltitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAltitude.Location = new System.Drawing.Point(672, 275);
            this.lblAltitude.Name = "lblAltitude";
            this.lblAltitude.Size = new System.Drawing.Size(18, 20);
            this.lblAltitude.TabIndex = 18;
            this.lblAltitude.Text = "0";
            // 
            // lblVerticalSpeed
            // 
            this.lblVerticalSpeed.AutoSize = true;
            this.lblVerticalSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerticalSpeed.Location = new System.Drawing.Point(449, 275);
            this.lblVerticalSpeed.Name = "lblVerticalSpeed";
            this.lblVerticalSpeed.Size = new System.Drawing.Size(18, 20);
            this.lblVerticalSpeed.TabIndex = 17;
            this.lblVerticalSpeed.Text = "0";
            // 
            // lblAirSpeed
            // 
            this.lblAirSpeed.AutoSize = true;
            this.lblAirSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAirSpeed.Location = new System.Drawing.Point(161, 275);
            this.lblAirSpeed.Name = "lblAirSpeed";
            this.lblAirSpeed.Size = new System.Drawing.Size(18, 20);
            this.lblAirSpeed.TabIndex = 16;
            this.lblAirSpeed.Text = "0";
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(672, 591);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(18, 20);
            this.lblHeading.TabIndex = 21;
            this.lblHeading.Text = "0";
            // 
            // lblTurnCoordinator
            // 
            this.lblTurnCoordinator.AutoSize = true;
            this.lblTurnCoordinator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurnCoordinator.Location = new System.Drawing.Point(449, 591);
            this.lblTurnCoordinator.Name = "lblTurnCoordinator";
            this.lblTurnCoordinator.Size = new System.Drawing.Size(18, 20);
            this.lblTurnCoordinator.TabIndex = 20;
            this.lblTurnCoordinator.Text = "0";
            // 
            // lblAttitude
            // 
            this.lblAttitude.AutoSize = true;
            this.lblAttitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttitude.Location = new System.Drawing.Point(161, 591);
            this.lblAttitude.Name = "lblAttitude";
            this.lblAttitude.Size = new System.Drawing.Size(18, 20);
            this.lblAttitude.TabIndex = 19;
            this.lblAttitude.Text = "0";
            // 
            // btnAirSpeedReturnToZero
            // 
            this.btnAirSpeedReturnToZero.Location = new System.Drawing.Point(222, 271);
            this.btnAirSpeedReturnToZero.Name = "btnAirSpeedReturnToZero";
            this.btnAirSpeedReturnToZero.Size = new System.Drawing.Size(40, 23);
            this.btnAirSpeedReturnToZero.TabIndex = 22;
            this.btnAirSpeedReturnToZero.Text = "RTZ";
            this.btnAirSpeedReturnToZero.UseVisualStyleBackColor = true;
            this.btnAirSpeedReturnToZero.Click += new System.EventHandler(this.btnAirSpeedReturnToZero_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(734, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "RTZ";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(734, 588);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "RTZ";
            this.button3.UseVisualStyleBackColor = true;
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
            this.turnCoordinator.Location = new System.Drawing.Point(268, 328);
            this.turnCoordinator.Name = "turnCoordinator";
            this.turnCoordinator.Size = new System.Drawing.Size(250, 250);
            this.turnCoordinator.TabIndex = 4;
            this.turnCoordinator.Text = "turnCoordinatorInstrumentControl1";
            // 
            // heading
            // 
            this.heading.Location = new System.Drawing.Point(524, 329);
            this.heading.Name = "heading";
            this.heading.Size = new System.Drawing.Size(250, 250);
            this.heading.TabIndex = 3;
            this.heading.Text = "headingIndicatorInstrumentControl1";
            // 
            // attitude
            // 
            this.attitude.Location = new System.Drawing.Point(12, 328);
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
            // btnPitchResetToZero
            // 
            this.btnPitchResetToZero.Location = new System.Drawing.Point(222, 591);
            this.btnPitchResetToZero.Name = "btnPitchResetToZero";
            this.btnPitchResetToZero.Size = new System.Drawing.Size(40, 23);
            this.btnPitchResetToZero.TabIndex = 25;
            this.btnPitchResetToZero.Text = "RTZ";
            this.btnPitchResetToZero.UseVisualStyleBackColor = true;
            this.btnPitchResetToZero.Click += new System.EventHandler(this.btnPitchResetToZero_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 804);
            this.Controls.Add(this.btnPitchResetToZero);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAirSpeedReturnToZero);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.lblTurnCoordinator);
            this.Controls.Add(this.lblAttitude);
            this.Controls.Add(this.lblAltitude);
            this.Controls.Add(this.lblVerticalSpeed);
            this.Controls.Add(this.lblAirSpeed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRunWithoutDemo);
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
            this.PerformLayout();

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
        private System.Windows.Forms.Button btnRunWithoutDemo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAltitude;
        private System.Windows.Forms.Label lblVerticalSpeed;
        private System.Windows.Forms.Label lblAirSpeed;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblTurnCoordinator;
        private System.Windows.Forms.Label lblAttitude;
        private System.Windows.Forms.Button btnAirSpeedReturnToZero;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnPitchResetToZero;
    }
}


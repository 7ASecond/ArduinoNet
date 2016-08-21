using System;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArduinoNet;

namespace ArduinoNet_Cockpit
{
    public partial class Form1 : Form
    {
        public delegate void ControlStringConsumer(string text);  // defines a delegate type
        public delegate void ControlLabelStringConsumer(Label lbl, string text);  // defines a delegate type
        public delegate void ControlConsumer();  // defines a delegate type 

        // The Serial Device we will be communicating with.
        private readonly Serial _serial = new Serial("COM9", 115200, false); // false = Don't open the connection yet

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            airSpeed.SetAirSpeedIndicatorParameters(0);                 // Set Initial Air speed
            altimeter.SetAlimeterParameters(0);                         // Set Initial Altitude
            attitude.SetAttitudeIndicatorParameters(0, 0);               // Set Initial Attitude (Pitch, Roll)
            heading.SetHeadingIndicatorParameters(0);                   // Set Initial Heading
            turnCoordinator.SetTurnCoordinatorParameters(0.00f, 0.00f);  // Set Initial Turn Coordinator Settings (Rate / Quality)
            verticalSpeed.SetVerticalSpeedIndicatorParameters(0);       // Set Initial Vertical Speed (Ground speed?)

            _serial.ArduinoErrorEvent += _serial_ArduinoErrorEvent;
            _serial.ArduinoDataRecievedEvent += _serial_ArduinoDataRecievedEvent;
            _serial.ArduinoLogEvent += _serial_ArduinoLogEvent;

        }

        private void _serial_ArduinoLogEvent(string message)
        {
            Log("Arduino Log: " + message);
        }

        // Using Protocol 2.
        // 2 char Command
        // Sensor Value as string
        // Z char terminator

        private double _currentPitch = 0;
        private int _currentAirSpeed = 0;
        private void _serial_ArduinoDataRecievedEvent(string dataRecievedMessage)
        {

            if (dataRecievedMessage.Length < 3) return;


            if (string.IsNullOrEmpty(dataRecievedMessage))
            {
                Log("Empty Message Received");
                return; // Ignore empty commands
            }


            if (dataRecievedMessage.Contains("Z"))
            {
                var zTerminatorPosition = dataRecievedMessage.LastIndexOf('Z');
                dataRecievedMessage = dataRecievedMessage.Replace("\r", "");
                dataRecievedMessage = dataRecievedMessage.Substring(0, zTerminatorPosition);

            }
            else
            {
                Log("Corrupt Message received");
                return; // No Terminator found - corrupt message
            }


            try
            {
                var command = dataRecievedMessage.ToCharArray()[0].ToString() +
                              dataRecievedMessage.ToCharArray()[1].ToString();
                    // Get the command - first two letters in our protocol
                var finalCommand = command.ToString().ToLowerInvariant(); // May not be needed now

                Log("Arduino Data: " + dataRecievedMessage);
                switch (finalCommand)
                {
                    // Collision Detection
                    case "cd":
                        // Pull UP! if low and flying
                        // Terrain - Pull Up! if high
                        break;
                    // Detect Internal Lighting Conditions
                    case "dl":
                        var lightValue = Convert.ToInt16(dataRecievedMessage.Replace(command.ToString(), ""));
                        // Low Light Darken form background
                        BackColor = lightValue > 500 ? Color.Black : DefaultBackColor;
                        SetRefresh();

                        break;
                    // Detect Direction
                    case "dd":
                        SetLabel(lblHeading, dataRecievedMessage.Replace(command.ToString(), ""));
                        // Set Left Right
                        // Set pitch
                        // Set Air Speed
                        // Set Direction
                        // Set Altimeter
                        break;
                    // Detect Pitch
                    case "dp":
                        // Set pitch
                        SetLabel(lblAttitude, (Convert.ToDouble(dataRecievedMessage.Replace(command.ToString(), "")) - _pitchModifier).ToString(CultureInfo.CurrentCulture) );
                        _currentPitch = Convert.ToDouble(dataRecievedMessage.Replace(command.ToString(), ""));
                        SetPitch(dataRecievedMessage.Replace(command.ToString(), ""));
                       
                        
                        // Set Air Speed
                        // Set Altimeter
                        // Set Vertical Speed
                        break;
                    // Detect Throttle
                    case "dt":
                        SetLabel(lblAirSpeed, (Convert.ToInt16(dataRecievedMessage.Replace(command.ToString(), "")) - _airSpeedModifier).ToString());
                        _currentAirSpeed = Convert.ToInt16(dataRecievedMessage.Replace(command.ToString(), ""));
                        SetAirSpeed(dataRecievedMessage.Replace(command.ToString(), ""));
                        break;
                    default:
                        Log("Unknown Message " + dataRecievedMessage);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log("Error processing command: " + ex.Message + "\t" + dataRecievedMessage);
            }            
        }

        private double _pitchModifier = 0;
        private void SetPitch(string value)
        {
            if (airSpeed.InvokeRequired)
            {
                airSpeed.Invoke(new ControlStringConsumer(SetPitch), new object[] { value });  // invoking itself
            }
            else
            {
                try
                {
                    attitude.SetAttitudeIndicatorParameters((Convert.ToDouble(value)-_pitchModifier), 0.00f);
                }
                catch (Exception ex)
                {
                    Log("Set Pitch: " + ex.Message);
                }
                
            }
        }

        private int _airSpeedModifier = 0;
        private void SetAirSpeed(string value)
        {
            if (airSpeed.InvokeRequired)
            {
                airSpeed.Invoke(new ControlStringConsumer(SetAirSpeed), new object[] { value });  // invoking itself
            }
            else
            {
                airSpeed.SetAirSpeedIndicatorParameters((Convert.ToInt16(value) - _airSpeedModifier));
            }
        }

        private void _serial_ArduinoErrorEvent(string errorMessage)
        {
            Log("Arduino Error: " + errorMessage);
        }

        private void RunStartupDemo()
        {
            btnStartEngines.Enabled = false;

            var tDas = new Task(DoAirSpeedDemo);
            var tDam = new Task(DoAltimeterDemo);
            var tDat = new Task(DoAttitudeDemo);
            var tDhd = new Task(DoHeadingDemo);
            var tDtc = new Task(DoTurnCoordinatorDemo);
            var tDv = new Task(DoVerticalSpeedDemo);

            tDas.Start();
            tDam.Start();
            tDat.Start();
            tDhd.Start();
            tDtc.Start();
            tDv.Start();

            while (_dtcRunning || _dvsRunning || _dhRunning || _daRunning || _dadRunning || _dasRunning)
            {
                Application.DoEvents();
            }

            
        }

        private bool _dvsRunning = true;
        private void DoVerticalSpeedDemo()
        {
            for (var idx = 0; idx < 6000; idx += 20)
            {
                verticalSpeed.SetVerticalSpeedIndicatorParameters(idx);
            }

            for (var idx = 6000; idx > -6000; idx -= 20)
            {
                verticalSpeed.SetVerticalSpeedIndicatorParameters(idx);
            }

            for (var idx = -6000; idx < 0; idx += 20)
            {
                verticalSpeed.SetVerticalSpeedIndicatorParameters(idx);
            }

            verticalSpeed.SetVerticalSpeedIndicatorParameters(0);
            _dvsRunning = false;
        }

        private bool _dtcRunning = true;
        private void DoTurnCoordinatorDemo()
        {
            for (var idx = 0.00f; idx < 4f; idx += 0.05f)
            {
                turnCoordinator.SetTurnCoordinatorParameters(idx, 0.00f);
            }

            for (var idx = 4f; idx > -4f; idx -= 0.05f)
            {
                turnCoordinator.SetTurnCoordinatorParameters(idx, 0.00f);
            }

            for (var idx = -4f; idx < 0f; idx += 0.05f)
            {
                turnCoordinator.SetTurnCoordinatorParameters(idx, 0.00f);
            }

            for (var idx = 0.00f; idx < 10f; idx += 0.05f)
            {
                turnCoordinator.SetTurnCoordinatorParameters(0.00f, idx);
            }

            for (var idx = 10f; idx > -10f; idx -= 0.05f)
            {
                turnCoordinator.SetTurnCoordinatorParameters(0.00f, idx);
            }

            for (var idx = -10f; idx < 0f; idx += 0.05f)
            {
                turnCoordinator.SetTurnCoordinatorParameters(0.00f, idx);
            }

            turnCoordinator.SetTurnCoordinatorParameters(0.00f, 0f);
            _dtcRunning = false;
        }

        private bool _dhRunning = true;
        private void DoHeadingDemo()
        {
            for (var idx = 0; idx < 359; idx++)
            {
                for (var its = 0; its < 5; its++)
                {
                    heading.SetHeadingIndicatorParameters(idx);
                }
            }

            for (var idx = 359; idx > 0; idx--)
            {
                for (var its = 0; its < 2; its++)
                {
                    heading.SetHeadingIndicatorParameters(idx);
                }
            }

            heading.SetHeadingIndicatorParameters(0);
            _dhRunning = false;
        }

        private bool _daRunning = true;
        private void DoAttitudeDemo()
        {
            // Attitude Demo

            for (var its = 0; its < 40; its++) // Pitch up to 40
            {
                for (var idx = 0; idx < 5; idx++)
                {
                    attitude.SetAttitudeIndicatorParameters(its, 0);
                }
            }

            for (var its = 40; its > -40; its--) // Pitch dow to -40
            {
                for (var idx = 0; idx < 5; idx++)
                {
                    attitude.SetAttitudeIndicatorParameters(its, 0);
                }
            }

            for (var its = -40; its < 0; its++) // Pitch up to 0
            {
                for (var idx = 5; idx > 0; idx--)
                {
                    attitude.SetAttitudeIndicatorParameters(its, 0);
                }
            }

            for (var its = 0; its < 40; its++) // Pitch up to 40
            {
                for (var idx = 0; idx < 5; idx++)
                {
                    attitude.SetAttitudeIndicatorParameters(0, its);
                }
            }

            for (var its = 40; its > -40; its--) // Pitch dow to -40
            {
                for (var idx = 0; idx < 5; idx++)
                {
                    attitude.SetAttitudeIndicatorParameters(0, its);
                }
            }

            for (var its = -40; its < 0; its++) // Pitch up to 0
            {
                for (var idx = 5; idx > 0; idx--)
                {
                    attitude.SetAttitudeIndicatorParameters(0, its);
                }
            }

            for (var its = 0; its < 40; its++) // Pitch up to 40
            {
                for (var idx = 0; idx < 5; idx++)
                {
                    attitude.SetAttitudeIndicatorParameters(its, its);
                }
            }

            for (var its = 40; its > -40; its--) // Pitch dow to -40
            {
                for (var idx = 0; idx < 5; idx++)
                {
                    attitude.SetAttitudeIndicatorParameters(its, its);
                }
            }

            for (var its = -40; its < 0; its++) // Pitch up to 0
            {
                for (var idx = 5; idx > 0; idx--)
                {
                    attitude.SetAttitudeIndicatorParameters(its, its);
                }
            }


            attitude.SetAttitudeIndicatorParameters(0f, 0f);
            _daRunning = false;
        }

        private bool _dadRunning = true;
        private void DoAltimeterDemo()
        {
            // Altimeter Demo
            for (var idx = 0; idx < 9500; idx += 100)
            {
                altimeter.SetAlimeterParameters(idx);
            }

            for (var idx = 9500; idx > 0; idx -= 500)
            {
                altimeter.SetAlimeterParameters(idx);
            }

            altimeter.SetAlimeterParameters(0);
            _dadRunning = false;
        }

        private bool _dasRunning = true;
        private void DoAirSpeedDemo()
        {
            // Airspeed Demo

            for (var airIdx = 0; airIdx < 800; airIdx += 25)
            {
                airSpeed.SetAirSpeedIndicatorParameters(airIdx);
            }

            for (var airIdx = 800; airIdx > 0; airIdx -= 50)
            {
                airSpeed.SetAirSpeedIndicatorParameters(airIdx);
            }

            airSpeed.SetAirSpeedIndicatorParameters(0);
            _dasRunning = false;
        }

        private void btnStartEngines_Click(object sender, EventArgs e)
        {
            RunStartupDemo();
        }

        private void Log(string message)
        {
            if (rtbConOut.InvokeRequired)
            {
                rtbConOut.Invoke(new ControlStringConsumer(Log), new object[] { message });  // invoking itself
            }
            else
            {
                rtbConOut.AppendText(message + Environment.NewLine);

            }
        }

        private void btnRunWithoutDemo_Click(object sender, EventArgs e)
        {
            btnStartEngines.Enabled = false;
            btnRunWithoutDemo.Enabled = false;
            _serial.OpenConnection();
        }
        
        private void SetRefresh()
        {
            if (InvokeRequired)
            {
                Invoke(new ControlConsumer(SetRefresh), new object[] { });  // invoking itself
            }
            else
            {
                Refresh();      // the "functional part", executing only on the main thread
            }
        }

        private void SetLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new ControlLabelStringConsumer(SetLabel), new object[] { label, text });  // invoking itself
            }
            else
            {
                label.Text = text;
                label.Refresh();      // the "functional part", executing only on the main thread
            }
        }

        private void btnPitchResetToZero_Click(object sender, EventArgs e)
        {
            btnPitchResetToZero.Enabled = false;
            btnPitchResetToZero.BackColor = Color.LightGreen;
            _pitchModifier = _currentPitch;
        }

        private void btnAirSpeedReturnToZero_Click(object sender, EventArgs e)
        {
            btnAirSpeedReturnToZero.Enabled = false;
            btnAirSpeedReturnToZero.BackColor = Color.LightGreen;
            _airSpeedModifier = _currentAirSpeed;
        }
    }
}

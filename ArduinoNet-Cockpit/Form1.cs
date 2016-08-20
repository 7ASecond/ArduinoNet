using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoNet_Cockpit
{
    public partial class Form1 : Form
    {
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
         
            btnStartEngines.Enabled = true;
        }

        private bool _dvsRunning = true;
        private void DoVerticalSpeedDemo()
        {
            for (var idx = 0; idx < 6000; idx += 10)
            {
                verticalSpeed.SetVerticalSpeedIndicatorParameters(idx);
            }

            for (var idx = 6000; idx > -6000; idx -= 10)
            {
                verticalSpeed.SetVerticalSpeedIndicatorParameters(idx);
            }

            for (var idx = -6000; idx < 0; idx += 10)
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
                turnCoordinator.SetTurnCoordinatorParameters( 0.00f, idx);
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
            for (var idx = 0; idx < 359; idx ++)
            {
                for (var its = 0; its < 5; its ++)
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


            attitude.SetAttitudeIndicatorParameters(0f,0f);
            _daRunning = false;
        }

        private bool _dadRunning = true;
        private void DoAltimeterDemo()
        {
            // Altimeter Demo
            for (var idx = 0; idx < 9500; idx+=100)
            {
                altimeter.SetAlimeterParameters(idx);
            }

            for (var idx = 9500; idx > 0; idx-=500)
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

            for (var airIdx = 0; airIdx < 800; airIdx+=25)
            {
                airSpeed.SetAirSpeedIndicatorParameters(airIdx);
            }

            for (var airIdx = 800; airIdx > 0; airIdx-=50)
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
    }
}

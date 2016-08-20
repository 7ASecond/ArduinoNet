using System.Windows.Forms;

namespace ArduinoNet_Cockpit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            airSpeed.SetAirSpeedIndicatorParameters(0);                 // Set Initial Air speed
            altimeter.SetAlimeterParameters(0);                         // Set Initial Altitude
            attitude.SetAttitudeIndicatorParameters(0,0);               // Set Initial Attitude (Pitch, Roll)
            heading.SetHeadingIndicatorParameters(0);                   // Set Initial Heading
            turnCoordinator.SetTurnCoordinatorParameters(0.00f,0.00f);  // Set Initial Turn Coordinator Settings (Rate / Quality)
            verticalSpeed.SetVerticalSpeedIndicatorParameters(0);       // Set Initial Vertical Speed (Ground speed?)
        }
    }
}

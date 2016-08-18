using System;
using System.Linq;
using System.Windows.Forms;
using ArduinoNet;

namespace ArduinoNet_Example
{
    public partial class Form1 : Form
    {
        // These delegates allow us to update the UI from an alternative thread to the UI Thread
        public delegate void ControlStringConsumer(RichTextBox control, string text);  // defines a delegate type
        public delegate void ControlDecimalConsumer(Decimal value);  // defines a delegate type

        // The Serial Device we will be communicating with.
        private readonly Serial _serial = new Serial("COM9", 115200, false);

        /// <summary>
        /// Instantiate the Form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When form has Loaded set up the Serial Communications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            _serial.ArduinoErrorEvent += Serial_ArduinoErrorEvent;
            _serial.ArduinoDataRecievedEvent += Serial_ArduinoDataRecievedEvent;
            _serial.ArduinoLogEvent += Serial_ArduinoLogEvent;
            _serial.OpenConnection();
        }

        /// <summary>
        /// Recieves information from ArduinoNet.Serial
        /// </summary>
        /// <param name="message">
        /// string: The information to log
        /// </param>
        private void Serial_ArduinoLogEvent(string message)
        {
            SetText(rtbConOut, "LOG: " + message);
        }

        /// <summary>
        /// Keeps a record of the last value recieved from the Temp Sensor
        /// </summary>
        private string _lastValue = "0";

        /// <summary>
        /// Recieves and processes the data that ArduinoNet.Serial has sent us
        /// </summary>
        /// <param name="dataRecievedMessage">
        /// string: The data we are to process
        /// </param>
        private void Serial_ArduinoDataRecievedEvent(string dataRecievedMessage)
        {
            SetText(rtbConOut, "DATA: " + dataRecievedMessage);
            try
            {
                if (dataRecievedMessage.Contains('\r')) dataRecievedMessage = dataRecievedMessage.Replace("\r","");
                if (dataRecievedMessage.Length == 5)
                    SetDecimal(Convert.ToDecimal(dataRecievedMessage));
                else
                    SetDecimal(Convert.ToDecimal(_lastValue));
            }
            catch (Exception)
            {
                SetDecimal(Convert.ToDecimal(_lastValue));
                throw;
            }

            _lastValue = dataRecievedMessage;
        }

        /// <summary>
        /// This updates the Tracker Graph 
        /// </summary>
        /// <param name="toDecimal">
        /// Decimal: The new value for the tracker
        /// </param>
        private void SetDecimal(decimal toDecimal)
        {
            if (tracker.InvokeRequired)
            {
                tracker.Invoke(new ControlDecimalConsumer(SetDecimal), new object[] { toDecimal });  // invoking itself
            }
            else
            {
                if ((int)(toDecimal * 100) + 100 > tracker.Maximum)
                    tracker.Maximum = (int)(toDecimal * 100) + 100;
                tracker.Value = (int)(toDecimal * 100);      // the "functional part", executing only on the main thread
            }
        }

        /// <summary>
        /// Recieves information from ArduinoNet.Serial concerning an Error
        /// </summary>
        /// <param name="errorMessage">
        /// string: The Error detected
        /// </param>
        private void Serial_ArduinoErrorEvent(string errorMessage)
        {
            SetText(rtbConOut, "ERROR: " + errorMessage);
        }

        /// <summary>
        /// Clean up resources on closing the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serial.ArduinoErrorEvent -= Serial_ArduinoErrorEvent;
            _serial.ArduinoDataRecievedEvent -= Serial_ArduinoDataRecievedEvent;
            _serial.ArduinoLogEvent -= Serial_ArduinoLogEvent;
           // _serial.CloseConnection();
            
        }

        /// <summary>
        /// This updates the Log RichTextBox
        /// </summary>
        /// <param name="control">
        /// RichTextBox: The control to update
        /// </param>
        /// <param name="text">
        /// string: The information to add to the log control
        /// </param>
        public void SetText(RichTextBox control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new ControlStringConsumer(SetText), new object[] { control, text });  // invoking itself
            }
            else
            {
                control.AppendText(DateTime.Now + "\t" + text);      // the "functional part", executing only on the main thread
            }
        }
    }
}

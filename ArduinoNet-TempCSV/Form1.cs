using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ArduinoNet;

namespace ArduinoNet_TempCSV
{
    public partial class Form1 : Form
    {
        public delegate void ControlStringConsumer(RichTextBox control, string text);  // defines a delegate type
        public delegate void ControlDecimalConsumer(decimal value);  // defines a delegate type
        private readonly Serial _serial = new Serial("COM9", 115200, false);

        public Form1()
        {
            InitializeComponent();
            _serial.ArduinoDataRecievedEvent += _serial_ArduinoDataRecievedEvent;
            _serial.OpenConnection();
        }

        private string _lastValue = "0";
        private void _serial_ArduinoDataRecievedEvent(string dataRecievedMessage)
        {
            SetText(rtbConOut,dataRecievedMessage);
            try
            {
                if (dataRecievedMessage.Contains('\r')) dataRecievedMessage = dataRecievedMessage.Replace("\r", "");
              
                    SetDecimal(Convert.ToDecimal(dataRecievedMessage));              
            }
            catch (Exception)
            {
                SetDecimal(Convert.ToDecimal(_lastValue));
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
                SaveData(DateTime.Now + "," + text); // use a comma instead of a tab \t to make a CSV file
            }
        }

        // File saved to desktop
        private readonly StreamWriter _sw = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ArduinoData.txt"));
        private void SaveData(string data)
        {
            _sw.WriteLineAsync(data + Environment.NewLine); // may write out of turn but the TimeStamp will allow us to sort it and put it back together correctly
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sw.Close();
        }
    }
}

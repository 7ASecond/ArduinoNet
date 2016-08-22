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
        private  Serial _serial; 

        public Form1()
        {
            InitializeComponent();           
        }

        private string _lastValue = "0";
        private void _serial_ArduinoDataRecievedEvent(string dataRecievedMessage)
        {
            SetText(rtbConOut, dataRecievedMessage);
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
        private  StreamWriter _sw = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ArduinoData.txt"));
        private readonly string _savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ArduinoData.txt");
        private void SaveData(string data)
        {
            
            if (_saveWithBlocking)
            {        
                if(_sw.BaseStream == null) _sw = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ArduinoData.txt"));
                _sw.WriteLineAsync(data + Environment.NewLine);
            }
            // may write out of turn but the TimeStamp will allow us to sort it and put it back together correctly
            else
            {            
                _sw?.Close();    
                FileStream fs = null;
                StreamWriter sw = null;

                try
                {
                    fs = new FileStream(_savePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(data + Environment.NewLine);
                    sw?.Close();
                    fs?.Close();
                }
                catch (Exception ex)
                {
                    SetText(rtbConOut, "ERROR: " + ex.Message);
                }
                finally
                {
                    sw?.Close();
                    fs?.Close();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sw?.Close(); // Close if not Null
        }


        private bool _saveWithBlocking = true;
        private void cbBlockingSave_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBlockingSave.Checked)
                _saveWithBlocking = true;
            else
            {
                _saveWithBlocking = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbComPort.Items.Add(Hardware.GetArduinoPort());
            cbComPort.SelectedIndex = 0;
           _serial = new Serial(Hardware.GetArduinoPort(), 115200, false);
            _serial.ArduinoDataRecievedEvent += _serial_ArduinoDataRecievedEvent;
            _serial.OpenConnection();
        }
    }
}

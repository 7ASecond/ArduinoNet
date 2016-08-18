using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArduinoNet;
using WinControls;

namespace ArduinoNet_Example
{
    public partial class Form1 : Form
    {

        public delegate void ControlStringConsumer(RichTextBox control, string text);  // defines a delegate type
        public delegate void ControlDecimalConsumer(Decimal value);  // defines a delegate type


        private readonly ArduinoNet.Serial _serial = new Serial("COM9", 115200, false);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _serial.ArduinoErrorEvent += Serial_ArduinoErrorEvent;
            _serial.ArduinoDataRecievedEvent += Serial_ArduinoDataRecievedEvent;
            _serial.ArduinoLogEvent += Serial_ArduinoLogEvent;
            _serial.OpenConnection();
        }

        private void Serial_ArduinoLogEvent(string message)
        {
            SetText(rtbConOut, "LOG: " + message);
        }

        private string lastValue = "0";
        private void Serial_ArduinoDataRecievedEvent(string dataRecievedMessage)
        {
            SetText(rtbConOut, "DATA: " + dataRecievedMessage);
            try
            {
                if (dataRecievedMessage.Contains('\r')) dataRecievedMessage = dataRecievedMessage.Replace("\r","");
                if (dataRecievedMessage.Length == 5)
                    SetDecimal(Convert.ToDecimal(dataRecievedMessage));
                else
                    SetDecimal(Convert.ToDecimal(lastValue));
            }
            catch (Exception)
            {
                SetDecimal(Convert.ToDecimal(lastValue));
                throw;
            }

            lastValue = dataRecievedMessage;
        }

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

        private void Serial_ArduinoErrorEvent(string errorMessage)
        {
            SetText(rtbConOut, "ERROR: " + errorMessage);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serial.ArduinoErrorEvent -= Serial_ArduinoErrorEvent;
            _serial.ArduinoDataRecievedEvent -= Serial_ArduinoDataRecievedEvent;
            _serial.ArduinoLogEvent -= Serial_ArduinoLogEvent;
           // _serial.CloseConnection();
            
        }

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

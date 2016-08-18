using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;

namespace ArduinoNet
{
    public class Serial
    {
        public delegate void LogHandler(string message);
        public event LogHandler ArduinoLogEvent;

        public delegate void ArduinoErrorHandler(string errorMessage);
        public event ArduinoErrorHandler ArduinoErrorEvent;

        public delegate void ArduinoDataRecievedHandler(string dataRecievedMessage);
        public event ArduinoDataRecievedHandler ArduinoDataRecievedEvent;

        private readonly SerialPort _sp = new SerialPort();
        private bool Stopping = false;

        private void Log(string message)
        {
            ArduinoLogEvent?.Invoke(message);
        }

        private void ErrorMessage(string errorMessage)
        {
            ArduinoErrorEvent?.Invoke(errorMessage);
        }

        private void DataRecieved(string dataReceived)
        {
            ArduinoDataRecievedEvent?.Invoke(dataReceived);
        }

        /// <summary>
        /// Instantiates the Arduino.Serial class with the correct settings to use
        /// </summary>
        /// <param name="portName">
        /// string: The portname to use 
        /// <example>
        /// "COM1"
        /// </example> 
        /// </param>
        /// <param name="baudRate">
        /// int: The baudRate of the connection, as set in the Arduino sketch
        /// <example>
        /// 115200
        /// </example>
        /// </param>
        /// <param name="openConnection">
        /// bool: Should the Connection be opened now? 
        /// <remarks>
        /// Default = True;
        /// </remarks>
        /// <example>
        /// False
        /// </example>
        /// </param>
        public Serial(string portName, int baudRate, bool openConnection = true)
        {
            _sp.PortName = portName;
            _sp.BaudRate = baudRate;


            _sp.DataReceived += _sp_DataReceived;
            _sp.ErrorReceived += _sp_ErrorReceived;
            _sp.PinChanged += _sp_PinChanged;


            if (openConnection) _sp.Open();
            Log("Serial Port has been opened: " + _sp.IsOpen);
        }

        private void _sp_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            if (!Stopping)
            {
                Log("Pin Changed " + e.EventType.ToString());
            }
        }

        private void _sp_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (!Stopping)
            {
                ErrorMessage(e.EventType.ToString());
            }
        }

        private void _sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!Stopping)
            {
                var sp = (SerialPort)sender;
                DataRecieved(sp.ReadLine());
            }
        }

        public void SendData(string data)
        {
            if (!Stopping)
            {
            }
        }

        public void SendData(int data)
        {
            if (!Stopping)
            {
            }
        }

        /// <summary>
        /// Opens a connection to the Arduino using the values provided in the new Arduino.Serial method
        /// </summary>
        /// <returns>
        /// bool: True if connection is opened successfully
        /// </returns>
        public bool OpenConnection()
        {
            if (_sp.IsOpen) return true;

            //TODO: Assumed to work for just now
            _sp.Open();
            return true;
        }

        /// <summary>
        /// Closes the Currently Open Connection
        /// </summary>
        /// <returns>
        /// bool: True if the connection is closed
        /// </returns>
        public bool CloseConnection()
        {
            if (_sp.IsOpen)
            {
                Stopping = true;

                //TODO: Closing the Serial Port causes a Known issue to cause apps to hang.
                //     System.Threading.Thread.Sleep(10);                
                //      _sp.Close(); 
                // _sp.Dispose();
            }
            return true;
        }


    }
}

using System.IO.Ports;

namespace ArduinoNet
{
    public class Serial
    {
        // Sends logging information to calling application
        public delegate void LogHandler(string message);
        public event LogHandler ArduinoLogEvent;

        // Sends error information to calling application
        public delegate void ArduinoErrorHandler(string errorMessage);
        public event ArduinoErrorHandler ArduinoErrorEvent;

        // Sends Data Recieved from Serially Connected Hardware to calling application
        public delegate void ArduinoDataRecievedHandler(string dataRecievedMessage);
        public event ArduinoDataRecievedHandler ArduinoDataRecievedEvent;

        // The global definition for this serial port.
        private readonly SerialPort _sp = new SerialPort();
        // Used to stop Events from running
        private bool _stopping;

        /// <summary>
        /// Used internally to send Logging information back to the calling application via an Event
        /// </summary>
        /// <param name="message">
        /// string: The message to send calling application
        /// </param>
        private void Log(string message)
        {
            ArduinoLogEvent?.Invoke(message);
        }

        /// <summary>
        /// Used internally to send Error information back to the calling application via an Event
        /// </summary>
        /// <param name="errorMessage">
        /// string: The Error Message to send calling application
        /// </param>
        private void ErrorMessage(string errorMessage)
        {
            ArduinoErrorEvent?.Invoke(errorMessage);
        }

        /// <summary>
        /// Used internally to send Received Data back to the calling application via an Event
        /// </summary>
        /// <param name="dataReceived">
        /// string: The data to send calling application
        /// </param>
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

        /// <summary>
        /// Reports when the connected serial device reports a pin change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _sp_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            if (!_stopping)
            {
                Log("Pin Changed " + e.EventType.ToString());
            }
        }

        /// <summary>
        /// Reports when the connected serial device reports an Error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _sp_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (!_stopping)
            {
                ErrorMessage(e.EventType.ToString());
            }
        }

        /// <summary>
        /// Reports when the connected serial device reports Data has been received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_stopping) return;

            var sp = (SerialPort)sender;
            DataRecieved(sp.ReadLine());
        }

        /// <summary>
        /// Sends Data as a string to the serial device
        /// </summary>
        /// <param name="data">
        /// string: The data to send
        /// </param>
        public void SendData(string data)
        {
            if (!_stopping)
            {
            }
        }

        /// <summary>
        /// Sends Data as an int to the serial device
        /// </summary>
        /// <param name="data">
        /// int: The data to send
        /// </param>
        public void SendData(int data)
        {
            if (!_stopping)
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
        /// <remarks>
        /// Currently this does nothing as there is a known "issue" that causes the serial device to hang when being closed.
        /// </remarks>
        public bool CloseConnection()
        {
            if (_sp.IsOpen)
            {
                _stopping = true;

                //TODO: Closing the Serial Port causes a Known issue to cause apps to hang.
                //     System.Threading.Thread.Sleep(10);                
                //      _sp.Close(); 
                // _sp.Dispose();
            }
            return true;
        }


    }
}

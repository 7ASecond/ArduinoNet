using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoNet
{
    public class Hardware
    {

        // If Only One Device exists then it must be the one we are using.
        // Can be changed later.
        public static string GetArduinoPort()
        {
            var devices = new List<UsbDeviceInfo>();

            var searcher = new ManagementObjectSearcher(@"Select * From Win32_SerialPort");
            foreach (var device in searcher.Get())
            {
                var res = device.GetPropertyValue("Name");
                if (res.ToString().Contains("Arduino") || res.ToString().Contains("Genuino"))
                    return (string)device.GetPropertyValue("DeviceId");
            }
            return string.Empty;
        }
    }

    internal class UsbDeviceInfo
    {
        public UsbDeviceInfo(string deviceId, string pnpDeviceId, string description, string name)
        {
            this.DeviceId = deviceId;
            this.PnpDeviceId = pnpDeviceId;
            this.Description = description;
            this.Name = name;
        }
        public string DeviceId { get; private set; }
        public string PnpDeviceId { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }

    }
}

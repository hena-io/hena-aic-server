using System;
using System.Text;
using System.Net.NetworkInformation;

namespace Hena
{
    public class SystemInfomation
    {
        private static SystemInfomation _instance = new SystemInfomation();
        private long _macAddressHash = 0;
        private int _macAddressHash24 = 0;
        private byte[] _macAddressBytes = null;
        private string _macAddress = string.Empty;
        private string _hostName = string.Empty;

		public static string UserName => Environment.UserName;
		public static string MachineName => Environment.MachineName;
		public static string OSVersion => Environment.OSVersion.ToString();

		public static string UserAgent => string.Format($"{UserName}({MachineName}) {OSVersion}");

		public static long MacAddressHash
        {
            get { return _instance._macAddressHash; }
        }

        public static int MacAddressHash24
        {
            get { return _instance._macAddressHash24; }
        }

        public static string MacAddress
        {
            get { return _instance._macAddress; }
        }

        public static byte[] MacAddressBytes
        {
            get { return _instance._macAddressBytes; }
        }

        public static string HostName
        {
            get { return _instance._hostName; }
        }

        SystemInfomation()
        {
            _macAddress = GetMacAddress();
            _macAddressBytes = GetMacAddressBytes();
            _hostName = GetHostName();

            _macAddressHash = 0;
            for( int i = 0; i < _macAddressBytes.Length; ++i )
            {
                long value = _macAddressBytes[i];
                _macAddressHash |= value << (i * 8);
            }

            _macAddressHash24 = 0x00FFFFFF & (int)_macAddressHash;
        }

        string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics.Length == 0)
                return string.Empty;

            return nics[0].GetPhysicalAddress().ToString();
        }

        byte[] GetMacAddressBytes()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics.Length == 0)
                return new byte[0];

            return nics[0].GetPhysicalAddress().GetAddressBytes();
        }

        string GetHostName()
        {
            return IPGlobalProperties.GetIPGlobalProperties().HostName;
        }
    }

}

using System;
using System.Net;
using System.Net.Sockets;

namespace HPCL.Common.Helper
{
    public static class CommonBase
    {
        public static string Api_Key = "3C25F265-F86D-419D-9A04-EA74A503C197";
        public static string Secret_Key = "PVmMSclp834KBIUa9O-XxpBsDJhsi1dsds74CiGaoo5";
        public static string useragent = "web";
        public static string userip = "";//GetLocalIPAddress();
        public static string userid = "1";
        public static string zonalid = "";
        public static string regionalid = "";

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

    }

}

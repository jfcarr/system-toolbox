using System.Net;
using System.Net.Sockets;

namespace SystemToolbox.Commands
{
    public class Network
    {
        public static string GetLocalIp()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }

            return localIP;
        }

        public static string GetExternalIpAddress()
        {
            var externalIpTask = GetExternalIpAddressHelper();
            GetExternalIpAddressHelper().Wait();
            var externalIpString = externalIpTask.Result ?? IPAddress.Loopback;

            return externalIpString.ToString();
        }

        private static async Task<IPAddress> GetExternalIpAddressHelper()
        {
            var externalIpString = (await new HttpClient().GetStringAsync("https://api.ipify.org"))
                .Replace("\\r\\n", "")
                .Replace("\\n", "")
                .Trim();

            if (!IPAddress.TryParse(externalIpString, out var ipAddress)) return null;

            return ipAddress;
        }

        public static string GetHostName()
        {
            string hostName = Dns.GetHostName();

            return hostName;
        }
    }
}

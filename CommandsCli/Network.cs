using System.ComponentModel;
using Spectre.Console.Cli;
using SystemToolbox.Commands;

namespace SystemToolbox.CommandsCli
{
    public class NetworkCategory : CommandSettings { }

    public class LocalIpAddress : NetworkCategory { }
    public class ExternalIpAddress : NetworkCategory { }
    public class HostName : NetworkCategory { }

    [Description("Get the local IP address.")]
    public class LocalIpAddressCommand : Command<LocalIpAddress>
    {
        public override int Execute(CommandContext context, LocalIpAddress settings)
        {
            Console.WriteLine(Network.GetLocalIp());

            return 0;
        }
    }

    [Description("Get the external IP address. (The IP address visible outside the local network)")]
    public class ExternalIpAddressCommand : Command<ExternalIpAddress>
    {
        public override int Execute(CommandContext context, ExternalIpAddress settings)
        {
            Console.WriteLine(Network.GetExternalIpAddress());

            return 0;
        }
    }

    [Description("Get the host/machine name.")]
    public class HostNameCommand : Command<HostName>
    {
        public override int Execute(CommandContext context, HostName settings)
        {
            Console.WriteLine(Network.GetHostName());

            return 0;
        }
    }

}
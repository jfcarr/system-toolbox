using Spectre.Console.Cli;
using SystemToolbox.CommandsCli;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = new CommandApp();

        app.Configure(config =>
        {
            config.AddBranch<DiskCategory>("disk", add =>
            {
                add.AddCommand<DiskFreeCommand>("free");
            });

            config.AddBranch<NetworkCategory>("network", add =>
            {
                add.AddCommand<LocalIpAddressCommand>("local_ip");
                add.AddCommand<ExternalIpAddressCommand>("external_ip");
                add.AddCommand<HostNameCommand>("hostname");
            });

            config.AddBranch<ValidateCategory>("validate", add =>
            {
                add.AddCommand<CheckJsonCommand>("json");
                add.AddCommand<CheckXmlCommand>("xml");
            });
        });

        app.Run(args);
    }
}


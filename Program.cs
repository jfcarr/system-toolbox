using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;
using SystemToolbox.Commands;

internal class Program
{
    private static void Main(string[] args)
    {
        // https://spectreconsole.net/cli/composing

        var app = new CommandApp();

        app.Configure(config =>
        {
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

public class NetworkCategory : CommandSettings { }

public class LocalIpAddress : NetworkCategory { }
public class ExternalIpAddress : NetworkCategory { }
public class HostName : NetworkCategory { }

public class LocalIpAddressCommand : Command<LocalIpAddress>
{
    public override int Execute(CommandContext context, LocalIpAddress settings)
    {
        Console.WriteLine(Network.GetLocalIp());

        return 0;
    }
}

public class ExternalIpAddressCommand : Command<ExternalIpAddress>
{
    public override int Execute(CommandContext context, ExternalIpAddress settings)
    {
        Console.WriteLine(Network.GetExternalIpAddress());

        return 0;
    }
}

public class HostNameCommand : Command<HostName>
{
    public override int Execute(CommandContext context, HostName settings)
    {
        Console.WriteLine(Network.GetHostName());

        return 0;
    }
}

public class ValidateCategory : CommandSettings { }

public class CheckXml : ValidateCategory
{
    [CommandArgument(0, "<FILE_NAME>")]
    public string FileName { get; set; }

    [CommandOption("-v|--verbose")]
    [DefaultValue(false)]
    public bool Verbose { get; set; }
}

public class CheckJson : ValidateCategory
{
    [CommandArgument(0, "<FILE_NAME>")]
    public string FileName { get; set; }

    [CommandOption("-v|--verbose")]
    [DefaultValue(false)]
    public bool Verbose { get; set; }
}

public class CheckXmlCommand : Command<CheckXml>
{
    public override int Execute(CommandContext context, CheckXml settings)
    {
        bool result = SystemToolbox.Commands.Validate.IsValidXml(settings.FileName, settings.Verbose);

        Console.WriteLine($"{((result) ? "Valid" : "Invalid")}");

        return 0;
    }
}

public class CheckJsonCommand : Command<CheckJson>
{
    public override int Execute(CommandContext context, CheckJson settings)
    {
        bool result = SystemToolbox.Commands.Validate.IsValidJson(settings.FileName, settings.Verbose);

        Console.WriteLine($"{((result) ? "Valid" : "Invalid")}");

        return 0;
    }
}
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

public class DiskCategory : CommandSettings { }

public class DiskFree : DiskCategory
{
    [Description("Show all drives.")]
    [CommandOption("-s|--showall")]
    [DefaultValue(false)]
    public bool ShowAll { get; set; }

    [Description("Display error messages.")]
    [CommandOption("-v|--verbose")]
    [DefaultValue(false)]
    public bool Verbose { get; set; }
}

public class DiskFreeCommand : Command<DiskFree>
{
    public override int Execute(CommandContext context, DiskFree settings)
    {
        Disk.ShowFreeSpace(settings.ShowAll, settings.Verbose);

        return 0;
    }
}

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

public class ValidateCategory : CommandSettings { }

public class CheckXml : ValidateCategory
{
    [CommandArgument(0, "<FILE_NAME>")]
    public string FileName { get; set; }

    [Description("Display error messages.")]
    [CommandOption("-v|--verbose")]
    [DefaultValue(false)]
    public bool Verbose { get; set; }
}

public class CheckJson : ValidateCategory
{
    [CommandArgument(0, "<FILE_NAME>")]
    public string FileName { get; set; }

    [Description("Display error messages.")]
    [CommandOption("-v|--verbose")]
    [DefaultValue(false)]
    public bool Verbose { get; set; }
}

[Description("Check to see if the given file contains well-formed XML.")]
public class CheckXmlCommand : Command<CheckXml>
{
    public override int Execute(CommandContext context, CheckXml settings)
    {
        bool result = SystemToolbox.Commands.Validate.IsValidXml(settings.FileName, settings.Verbose);

        Console.WriteLine($"{((result) ? "Valid" : "Invalid")}");

        return 0;
    }
}

[Description("Check to see if the given file contains well-formed JSON.")]
public class CheckJsonCommand : Command<CheckJson>
{
    public override int Execute(CommandContext context, CheckJson settings)
    {
        bool result = SystemToolbox.Commands.Validate.IsValidJson(settings.FileName, settings.Verbose);

        Console.WriteLine($"{((result) ? "Valid" : "Invalid")}");

        return 0;
    }
}
using System.ComponentModel;
using Spectre.Console.Cli;
using SystemToolbox.Commands;

namespace SystemToolbox.CommandsCli
{
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
}
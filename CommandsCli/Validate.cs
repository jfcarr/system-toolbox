using System.ComponentModel;
using Spectre.Console.Cli;

namespace SystemToolbox.CommandsCli
{
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
}
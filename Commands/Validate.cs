using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace SystemToolbox.Commands
{
    public static class Validate
    {
        public static bool IsValidXml(string fileName, bool verbose)
        {
            try
            {
                var xd1 = new XDocument();
                xd1 = XDocument.Load(fileName);

                return true;
            }
            catch (XmlException exception)
            {
                if (verbose)
                    Console.WriteLine(exception.Message);

                return false;
            }
        }

        public static bool IsValidJson(string fileName, bool verbose)
        {
            try
            {
                var jsonText = File.ReadAllText(fileName);
                var result = JsonDocument.Parse(jsonText);

                return true;
            }
            catch (JsonException ex)
            {
                if (verbose)
                    Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
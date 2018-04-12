using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Utility
{
    public class XmlHandler
    {
        public XmlHandler()
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add("http://www.w3.org/2001/XMLSchema", "PackageTemplate.xsd");
                settings.ValidationType = ValidationType.Schema;

                XmlReader reader = XmlReader.Create("packages.config", settings);
                XmlDocument document = new XmlDocument();
                document.Load(reader);

                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);

                // the following call to Validate succeeds.
                document.Validate(eventHandler);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //// Set the validation settings.
            XmlReaderSettings settings2 = new XmlReaderSettings();
            settings2.Schemas.Add("http://www.w3.org/2001/XMLSchema", "PackageTemplate.xsd");
            settings2.ValidationType = ValidationType.Schema;
            settings2.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings2.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings2.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings2.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            // Create the XmlReader object.
            XmlReader reader2 = XmlReader.Create("packages.config", settings2);
            XmlDocument document2 = new XmlDocument();
            document2.Load(reader2);

            var data  = document2.SelectNodes("packages/NuGet.Core");
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);

        }
    }
}

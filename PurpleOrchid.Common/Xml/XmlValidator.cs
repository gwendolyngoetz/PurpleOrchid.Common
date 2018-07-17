using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace PurpleOrchid.Common.Xml
{
    public enum XmlValidatorMode
    {
        FileSystem,
        InMemory
    }

    public class XmlValidator
    {
        private readonly ICollection<XmlSchemaException> _errors = new List<XmlSchemaException>();

        /// <summary>
        /// Validates an xmlSource document against an xmlSource schema
        /// </summary>
        /// <param name="mode">
        /// Mode alters the way xmlSource and xmlSchema are treated.
        /// - FileSystem uses them as paths on disk
        /// - InMemory uses them as xmlSource documents
        /// </param>
        /// <param name="xmlSource">Path to the xmlSource file</param>
        /// <param name="xmlSchema">Path to the xmlSchema file</param>
        /// <param name="defaultNamespace">Default namespace of the xmlSource/xmlSchema</param>
        public IEnumerable<XmlSchemaException> Validate(XmlValidatorMode mode, string xmlSource, string xmlSchema, string defaultNamespace = null)
        {
            switch (mode)
            {
                case XmlValidatorMode.InMemory:
                    return ValidateInMemoryXml(xmlSource, xmlSchema, defaultNamespace);

                case XmlValidatorMode.FileSystem:
                    return ValidateFromFileSystem(xmlSource, xmlSchema, defaultNamespace);

                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private IEnumerable<XmlSchemaException> ValidateFromFileSystem(string xmlSource, string xmlSchema, string defaultNamespace)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlSource);
            
            var settings = new XmlReaderSettings
            {
                CloseInput = true,
                ValidationType = ValidationType.Schema,
                ValidationFlags =
                    XmlSchemaValidationFlags.ReportValidationWarnings |
                    XmlSchemaValidationFlags.ProcessIdentityConstraints |
                    XmlSchemaValidationFlags.ProcessInlineSchema |
                    XmlSchemaValidationFlags.ProcessSchemaLocation
            };

            settings.ValidationEventHandler += OnValidation;

            defaultNamespace = defaultNamespace == string.Empty ? null : defaultNamespace;

            settings.Schemas.Add(defaultNamespace, xmlSchema);

            using (var stringReader = new StringReader(xmlSource))
            {
                using (var validatingReader = XmlReader.Create(stringReader, settings))
                {
                    while (validatingReader.Read())
                    {
                        /* just loop through document, so the validation callback is triggered */
                    }
                }
            }

            return _errors;
        }

        private IEnumerable<XmlSchemaException> ValidateInMemoryXml(string xmlSource, string xmlSchema, string defaultNamespace)
        {
            var xdoc = XDocument.Parse(xmlSource);
            var schemas = new XmlSchemaSet();

            defaultNamespace = defaultNamespace ?? string.Empty;
            schemas.Add(defaultNamespace, XmlReader.Create(new StringReader(xmlSchema)));

            xdoc.Validate(schemas, OnValidation);

            return _errors;
        }

        private void OnValidation(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning)
            {
                _errors.Add(e.Exception);
            }
        }
    }
}

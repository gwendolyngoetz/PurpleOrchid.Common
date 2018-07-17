using System;
using System.Xml.Linq;
using PurpleOrchid.Common.Contracts;

namespace PurpleOrchid.Common.Extensions
{
    public static class XElementExtensions
    {
        public static string GetAttributeValue(this XElement source, string name, bool required = true)
        {
            Require.NotNull(nameof(source), source);

            var attribute = source.Attribute(name);

            if (attribute != null)
            {
                return attribute.Value;
            }

            if (!required)
            {
                return string.Empty;
            }

            throw new InvalidOperationException("Could not find attribute: {0}".FormatString(name));
        }

        public static string GetElementValue(this XElement source, string name, bool required = true)
        {
            Require.NotNull(nameof(source), source);

            var element = source.Element(name);

            if (element != null)
            {
                return element.Value;
            }

            if (!required)
            {
                return string.Empty;
            }

            throw new InvalidOperationException("Could not find element: {0}".FormatString(name));
        }
    }
}

using System.Xml.Linq;

namespace PurpleOrchid.Common.Xml
{
    public class XlinqHelpers
    {
        public static XAttribute NewAttributeOrNull(XName name, object value)
        {
            return value == null ? null : new XAttribute(name, value);
        }

        public static XAttribute NewAttributeOrNull(XName name, string value)
        {
            return string.IsNullOrEmpty(value) ? null : new XAttribute(name, value);
        }

        public static XAttribute NewAttributeOrDefault(XName name, object value, string defaultValue = "")
        {
            return value == null ? new XAttribute(name, defaultValue) : new XAttribute(name, value);
        }

        public static XElement NewXElementOrNull(XName name, object value)
        {
            return value == null ? null : new XElement(name, value);
        }
    }
}

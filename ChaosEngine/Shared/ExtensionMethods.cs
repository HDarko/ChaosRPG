using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChaosEngine.Shared
{
    public static class ExtensionMethods
    {
        public static int GetXmlAttributeAsInt(this XmlNode node, string attributeName)
        {
            return Convert.ToInt32(node.GetXmlAttributeAsString(attributeName));
        }

        public static string GetXmlAttributeAsString(this XmlNode node, string attributeName)
        {
            XmlAttribute attribute = node.Attributes?[attributeName];
            if (attribute == null)
            {
                throw new ArgumentException($"The attribute '{attributeName}' does not exist");
            }
            return attribute.Value;
        }
    }
}



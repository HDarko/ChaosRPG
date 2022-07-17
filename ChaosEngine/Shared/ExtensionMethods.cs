using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace ChaosEngine.Shared
{
    public static class ExtensionMethods
    {
        public static int GetXmlAttributeAsInt(this XmlNode node, string attributeName, bool returnNull = false)
        {
            return Convert.ToInt32(node.GetXmlAttributeAsString(attributeName, returnNull));
        }

        public static string GetXmlAttributeAsString(this XmlNode node, string attributeName, bool returnNull=false)
        {
            XmlAttribute attribute = node.Attributes?[attributeName];
            if (attribute == null)
                {
                    if (returnNull) 
                    {
                        return null;
                    }
                    else 
                    {
                        throw new ArgumentException($"The attribute '{attributeName}' does not exist");
                    }
              
            }        
            return attribute.Value;
        }
        public static bool GetXmlAttributeAsBool(this XmlNode node, string attributeName, bool returnNull = false, bool defaultIfNull=false)
        {
            string value = node.GetXmlAttributeAsString(attributeName, returnNull) ?? "NoValue";
            if(value== "NoValue")
            {
                return defaultIfNull;
            }
            else
            {
                return Convert.ToBoolean(value);
            }
            
        }
        public static string StringValueOf(this JObject jsonObject, string key)
        {
            return jsonObject[key].ToString();
        }
        public static string StringValueOf(this JToken jsonObject, string key)
        {
            return jsonObject[key].ToString();
        }
        public static bool BoolValueOf(this JToken jsonToken, string key)
        {
            return Convert.ToBoolean(jsonToken[key]);
        }
        public static int IntValueOf(this JToken jsonToken, string key)
        {
            return Convert.ToInt32(jsonToken[key]);
        }
    }
}



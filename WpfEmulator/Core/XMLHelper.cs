using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Games.NBall.Common;

namespace Games.NBall.WpfEmulator.Core
{
    public class XMLHelper
    {
        // Methods
        public static bool CreateOrUpdateXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName, string value)
        {
            bool flag = false;
            bool flag2 = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        if (attribute.Name.ToLower() == xmlAttributeName.ToLower())
                        {
                            attribute.Value = value;
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        XmlAttribute attribute2 = document.CreateAttribute(xmlAttributeName);
                        attribute2.Value = value;
                        node.Attributes.Append(attribute2);
                    }
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool CreateOrUpdateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText)
        {
            bool flag = false;
            bool flag2 = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name.ToLower() == xmlNodeName.ToLower())
                        {
                            node2.InnerXml = innerText;
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        XmlElement newChild = document.CreateElement(xmlNodeName);
                        newChild.InnerXml = innerText;
                        node.AppendChild(newChild);
                    }
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string version, string encoding, string standalone)
        {
            bool flag = false;
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration(version, encoding, standalone);
                XmlNode node = document.CreateElement(rootNodeName);
                document.AppendChild(newChild);
                document.AppendChild(node);
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool CreateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText, string xmlAttributeName, string value)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    XmlElement newChild = document.CreateElement(xmlNodeName);
                    newChild.InnerXml = innerText;
                    if (!(string.IsNullOrEmpty(xmlAttributeName) || string.IsNullOrEmpty(value)))
                    {
                        XmlAttribute attribute = document.CreateAttribute(xmlAttributeName);
                        attribute.Value = value;
                        newChild.Attributes.Append(attribute);
                    }
                    node.AppendChild(newChild);
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DeleteAllXmlAttributeByXPath(string xmlFileName, string xpath)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    node.Attributes.RemoveAll();
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DeleteXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName)
        {
            bool flag = false;
            bool flag2 = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                XmlAttribute attribute = null;
                if (node != null)
                {
                    foreach (XmlAttribute attribute2 in node.Attributes)
                    {
                        if (attribute2.Name.ToLower() == xmlAttributeName.ToLower())
                        {
                            attribute = attribute2;
                            flag2 = true;
                            break;
                        }
                    }
                    if (flag2)
                    {
                        node.Attributes.Remove(attribute);
                    }
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DeleteXmlNodeByXPath(string xmlFileName, string xpath)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode oldChild = document.SelectSingleNode(xpath);
                if (oldChild != null)
                {
                    oldChild.ParentNode.RemoveChild(oldChild);
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static XmlAttribute GetXmlAttribute(string xmlFileName, string xpath, string xmlAttributeName)
        {
            XmlDocument document = new XmlDocument();
            XmlAttribute attribute = null;
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node == null)
                {
                    return attribute;
                }
                if (node.Attributes.Count > 0)
                {
                    attribute = node.Attributes[xmlAttributeName];
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return attribute;
        }

        public static XmlNode GetXmlNodeByXpath(string xmlFileName, string xpath)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                return document.SelectSingleNode(xpath);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return null;
            }
        }

        public static XmlNodeList GetXmlNodeListByXpath(string xmlFileName, string xpath)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                return document.SelectNodes(xpath);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return null;
            }
        }

    }
}

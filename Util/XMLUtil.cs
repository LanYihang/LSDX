using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Com.Ls.Lsdx.Util
{
    /// <summary>
    /// 操作XML的实体操作类
    /// </summary>
    public class XMLUtil
    {
        /// <summary>
        /// 从XML中读取数据
        /// </summary>
        public static void LoadXMLData()
        {
            XmlReader reader = null;
            try
            {
                if (File.Exists(Sysinfo.BASE_DICTIONARY_XML_PATH))
                {
                    XmlDocument xml = new XmlDocument();
                    XmlReaderSettings setting = new XmlReaderSettings();
                    setting.IgnoreComments = true;//不读取注释
                    reader = XmlReader.Create(Sysinfo.BASE_DICTIONARY_XML_PATH, setting);
                    xml.Load(reader);
                    XmlNode parentNode = xml.SelectSingleNode("DATA");
                    foreach (XmlNode type_node in parentNode.ChildNodes)
                    {
                        string type = type_node.Attributes["name"].Value;
                        BaseInfo info = new BaseInfo();
                        foreach (XmlNode standardsOrProjectItem in type_node.ChildNodes)
                        {
                            if (standardsOrProjectItem.Name == "STANDARDS")
                            {
                                foreach (XmlNode standard_node in standardsOrProjectItem.ChildNodes)
                                    info.Standards.Add(standard_node.InnerText);
                            }
                            if (standardsOrProjectItem.Name == "PROJECTS")
                            {
                                foreach (XmlNode project_node in standardsOrProjectItem.ChildNodes)
                                    info.Projects.Add(project_node.InnerText);
                            }
                        }
                        Sysinfo.XLS_TYPE_DICTIONARA.Add(type, info);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// 将数据写入XML中
        /// </summary>
        public static void SerializationDataForXML()
        {
            try
            {
                string indentation = "";//缩进标识
                using (FileStream stream = new FileStream(Sysinfo.BASE_DICTIONARY_XML_PATH, FileMode.OpenOrCreate))
                {
                    StringBuilder xmlStrBuilder = new StringBuilder();
                    xmlStrBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    xmlStrBuilder.AppendLine("<DATA>");
                    if (Sysinfo.XLS_TYPE_DICTIONARA.Count > 0)
                    {
                        foreach (string key in Sysinfo.XLS_TYPE_DICTIONARA.Keys)
                        {
                            indentation = " ";
                            xmlStrBuilder.AppendLine(indentation + "<TYPE name=\"" + key + "\">");
                            indentation = "  ";
                            xmlStrBuilder.AppendLine(indentation + "<STANDARDS>");
                            indentation = "   ";
                            if (Sysinfo.XLS_TYPE_DICTIONARA[key].Standards.Count > 0)
                                foreach (string standard in Sysinfo.XLS_TYPE_DICTIONARA[key].Standards)
                                    xmlStrBuilder.AppendLine(indentation + "<ITEM>" + standard + "</ITEM>");
                            indentation = "  ";
                            xmlStrBuilder.AppendLine(indentation + "</STANDARDS>");
                            xmlStrBuilder.AppendLine(indentation + "<PROJECTS>");
                            indentation = "   ";
                            if (Sysinfo.XLS_TYPE_DICTIONARA[key].Projects.Count > 0)
                                foreach (string project in Sysinfo.XLS_TYPE_DICTIONARA[key].Projects)
                                    xmlStrBuilder.AppendLine(indentation + "<ITEM>" + project + "</ITEM>");
                            indentation = "  ";
                            xmlStrBuilder.AppendLine(indentation + "</PROJECTS>");
                            indentation = " ";
                            xmlStrBuilder.AppendLine(indentation + "</TYPE>");
                        }
                    }
                    xmlStrBuilder.AppendLine("</DATA>");
                    byte[] buffer = Encoding.ASCII.GetBytes(xmlStrBuilder.ToString());
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

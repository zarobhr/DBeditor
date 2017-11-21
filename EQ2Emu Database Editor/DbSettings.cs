using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Net;

namespace EQ2Emu_Database_Editor
{
    class Settings
    {
        public static void Load()
        {
            if (!File.Exists("settings.xml"))
                return;

            XmlReader reader = XmlReader.Create("settings.xml");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "DbSettings")
                {

                    if (reader.GetAttribute(0) == "Server")
                    {
                        reader.Read();
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            Form_Connect.server_path = reader.Value;
                            Form1.server_path = reader.Value;
                        }
                    }

                    else if (reader.GetAttribute(0) == "Database")
                    {
                        reader.Read();
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            Form_Connect.database_path = reader.Value;
                            Form1.database_path = reader.Value;
                        }
                    }

                    else if (reader.GetAttribute(0) == "User")
                    {
                        reader.Read();
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            Form_Connect.username_path = reader.Value;
                            Form1.username_path = reader.Value;
                        }
                    }

                    else if (reader.GetAttribute(0) == "Password")
                    {
                        reader.Read();
                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            Form_Connect.password_path = reader.Value;
                            Form1.password_path = reader.Value;
                        }
                    }
                }
            }

            reader.Close();
        }

        public static void Save(string server_path, string database_path, string username_path, string password_path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("settings.xml", settings);
            writer.WriteStartDocument();
            writer.WriteComment("QuestEditor settings, used to store database information");

            writer.WriteStartElement("QuestEditor");

            if (!string.IsNullOrEmpty(server_path))
            {
                writer.WriteStartElement("DbSettings");
                writer.WriteAttributeString("Type", "Server");
                writer.WriteString(server_path);
                writer.WriteEndElement();
            }

            if (!string.IsNullOrEmpty(database_path))
            {
                writer.WriteStartElement("DbSettings");
                writer.WriteAttributeString("Type", "Database");
                writer.WriteString(database_path);
                writer.WriteEndElement();
            }

            if (!string.IsNullOrEmpty(username_path))
            {
                writer.WriteStartElement("DbSettings");
                writer.WriteAttributeString("Type", "User");
                writer.WriteString(username_path);
                writer.WriteEndElement();
            }

            if (!string.IsNullOrEmpty(password_path))
            {
                writer.WriteStartElement("DbSettings");
                writer.WriteAttributeString("Type", "Password");
                writer.WriteString(password_path);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
        /* Sample XML File
         * 
         * <Server Type="World">C:/EQ2Emu/EQ2World.exe</Server>
         * <Server Type="Login">C:/EQ2Emu/EQ2Login.exe</Server>
         */


    }
}


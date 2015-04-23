using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Xml;


namespace DB
{
    public class DBFunctions
    {
        public static void SaveResult(string resultXML)
        {
            var xmlFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("LoggingUtilities", "GatewayResponse.xml"));
            string path = xmlFileName.ToString(); 
            string timestamp = DateTime.Now.ToString();

            // This Text is added only once to the file
            /*if (!File.Exists(path))
            {
                //Create a file to Write to
                string createText = "THis is a placeholder" + Environment.NewLine;
                File.WriteAllText(path, createText);
            }*/
            //Create Stringbuilder for logging the Result File
            StringBuilder logResultFile = new StringBuilder();

            //Create new instance of XML Settings, set indent
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;

            //Create XML to be placed inside ResultFiles.XML
            using (XmlWriter xmlWriter = XmlWriter.Create(logResultFile, ws))
            {
                //Start Xml Writer
                xmlWriter.WriteStartDocument();
                //Start Root Element
                xmlWriter.WriteStartElement("ResultFile");

                xmlWriter.WriteAttributeString("timestamp", timestamp);

                xmlWriter.WriteStartElement("Contents");
                xmlWriter.WriteString(resultXML);
                xmlWriter.WriteEndElement();

                //End Root Element
                xmlWriter.WriteEndElement();
                //End Xml Writer
                xmlWriter.WriteEndDocument();

            }
            
            string logContent = logResultFile.ToString();
            string decodedLogContent = HttpUtility.HtmlDecode(logContent);

            // This Text is Always Added, Making the Fiel Longer over time if it is not deleted.
            File.AppendAllText(path, decodedLogContent);
           
        }

        public static void saveAlias (string resultXML)
        {
            string parsedAlias = null;
            string transactionID = null;

            using (XmlTextReader xmlReader = new XmlTextReader(new StringReader(resultXML)))
            {
                while (xmlReader.Read())
                {
                    while (xmlReader.ReadToFollowing("TransactionID"))
                    {
                        transactionID = xmlReader.ReadElementContentAsString();
                        
                        while (xmlReader.ReadToFollowing("Alias"))
                        {
                            parsedAlias = xmlReader.ReadElementContentAsString();
                        }
                    }
                }
                xmlReader.Close();
            }

            StringBuilder saveAliasSB = new StringBuilder();

            XmlWriterSettings sA = new XmlWriterSettings();
            sA.Indent = true;

            using (XmlWriter xmlWriter = XmlWriter.Create(saveAliasSB, sA))
            {
                //Start Xml Writer
                xmlWriter.WriteStartDocument();
                //Start Root Element
                xmlWriter.WriteStartElement("TokenInformation");

                xmlWriter.WriteStartElement("TransactionID");
                xmlWriter.WriteString(transactionID);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Alias");
                xmlWriter.WriteString(parsedAlias);
                xmlWriter.WriteEndElement();

                //End Root Element
                xmlWriter.WriteEndElement();
                //End Xml Writer
                xmlWriter.WriteEndDocument();
            }

            string tokenInformation = saveAliasSB.ToString();

            var xmlFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("LoggingUtilities", "TokenDB.xml"));
            string path = xmlFileName.ToString(); 

            File.AppendAllText(path, tokenInformation);
            
        }
    }
    
}
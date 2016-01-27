using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;


namespace DB
{
    public class DBFunctions
    {
        internal void xmlParser (string resultXML)
        {
            //Load XML
            XDocument xcr = XDocument.Parse(resultXML);

            //NYI
        }
        //XML Content Strings

        public static string responseCode = null;
        public static string responseDescription = null;
        public static string transactionID = null;
        public static string transactionType = null;
        public static string alias = null;
        public static string cardBrand = null;
        public static string maskedAcctNum = null;
        public static string expDate = null;
        public static string otk = null;
        public static string acctNumSource = null;
        public static string approvalCode = null;
        public static string avsResponseCode = null;
        public static string cardCodeResponse = null;
        public static string amount = null;

        public static void SaveResult(string resultXML)
        {
            var xmlFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("LoggingUtilities", "GatewayResponse.xml"));
            string path = xmlFileName.ToString(); 
            string timestamp = DateTime.Now.ToString();


            var rawTextPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("LoggingUtilities", "Raw Results.txt"));
            string rawPath = rawTextPath.ToString();
            File.AppendAllText(rawPath, resultXML);
            File.AppendAllText(rawPath, Environment.NewLine);

            
            bool AliasTransaction = false;
            bool OtKTransaction = false;
            bool Approval = false;
            bool Pending = false;
            

            // This Text is added only once to the file
            /*if (!File.Exists(path))
            {
                //Create a file to Write to
                string createText = "THis is a placeholder" + Environment.NewLine;
                File.WriteAllText(path, createText);
            }*/
            //Create Stringbuilder for logging the Result File

            using (XmlTextReader xmlReader = new XmlTextReader(new StringReader(resultXML)))
            {
                while (xmlReader.Read())
                {
                    while (xmlReader.ReadToFollowing("ResponseCode"))
                    {
                        responseCode = xmlReader.ReadElementContentAsString();

                        if (responseCode == "005")
                        {
                            AliasTransaction = true;

                            while (xmlReader.ReadToFollowing("ResponseDescription"))

                            {
                                responseDescription = xmlReader.ReadElementContentAsString();

                                while (xmlReader.ReadToFollowing("TransactionID"))
                                {
                                    transactionID = xmlReader.ReadElementContentAsString();

                                    while (xmlReader.ReadToFollowing("TransactionType"))
                                    {
                                        transactionType = xmlReader.ReadElementContentAsString();

                                        while (xmlReader.ReadToFollowing("Alias"))
                                        {
                                            alias = xmlReader.ReadElementContentAsString();

                                            while (xmlReader.ReadToFollowing("CardBrand"))
                                            {
                                                cardBrand = xmlReader.ReadElementContentAsString();

                                                while (xmlReader.ReadToFollowing("MaskedAcctNum"))
                                                {
                                                    maskedAcctNum = xmlReader.ReadElementContentAsString();

                                                    while (xmlReader.ReadToFollowing("ExpDate"))
                                                    {
                                                        expDate = xmlReader.ReadElementContentAsString();

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //End ResponseCode == 005 (Alias Created) Parsing

                        if (responseCode == "000")
                        {
                            Approval = true;

                            while (xmlReader.ReadToFollowing("ResponseDescription"))
                            {
                                responseDescription = xmlReader.ReadElementContentAsString();

                                while (xmlReader.ReadToFollowing("TransactionID"))
                                {
                                    transactionID = xmlReader.ReadElementContentAsString();

                                    while (xmlReader.ReadToFollowing("TransactionType"))
                                    {
                                        transactionType = xmlReader.ReadElementContentAsString();

                                        while (xmlReader.ReadToFollowing("Amount"))
                                        {
                                            amount = xmlReader.ReadElementContentAsString();

                                            while (xmlReader.ReadToFollowing("CardBrand"))
                                            {
                                                cardBrand = xmlReader.ReadElementContentAsString();

                                                while (xmlReader.ReadToFollowing("MaskedAcctNum"))
                                                {
                                                    maskedAcctNum = xmlReader.ReadElementContentAsString();

                                                    while (xmlReader.ReadToFollowing("ExpDate"))
                                                    {
                                                        expDate = xmlReader.ReadElementContentAsString();

                                                        while (xmlReader.ReadToFollowing("AcctNumSource"))
                                                        {
                                                            acctNumSource = xmlReader.ReadElementContentAsString();

                                                            while (xmlReader.ReadToFollowing("Alias"))
                                                            {
                                                                alias = xmlReader.ReadElementContentAsString();

                                                                while (xmlReader.ReadToFollowing("ApprovalCode"))
                                                                {
                                                                    approvalCode = xmlReader.ReadElementContentAsString();

                                                                    while (xmlReader.ReadToFollowing("AVSResponseCode"))
                                                                    {
                                                                        avsResponseCode = xmlReader.ReadElementContentAsString();

                                                                        while (xmlReader.ReadToFollowing("CardCodeResponse"))
                                                                        {
                                                                            cardCodeResponse = xmlReader.ReadElementContentAsString();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        // End ResponseCode == 000 Approval Parsing
                        if (responseCode == "100")
                        {
                            while (xmlReader.ReadToFollowing("ResponseDescription"))
                            {
                                OtKTransaction = true;
                                responseDescription = xmlReader.ReadElementContentAsString();
                                
                                while (xmlReader.ReadToFollowing("OTK"))
                                {
                                    otk = xmlReader.ReadElementContentAsString();

                                }
                            }
                        }
                        //End Response code == 100 Otk Parsing

                        if (responseCode == "102")
                        {
                            Pending = true;
                            responseDescription = "Pending";
                        }                     
                    }
                }
            }




            StringBuilder logResultFile = new StringBuilder();

            //Create new instance of XML Settings, set indent
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;
            ws.OmitXmlDeclaration = true;

            //Create XML to be placed inside ResultFiles.XML
            using (XmlWriter xmlWriter = XmlWriter.Create(logResultFile, ws))
            {
                //Start Xml Writer
                //Start Root Element
                xmlWriter.WriteStartElement("ResultFile");

                xmlWriter.WriteAttributeString("timestamp", timestamp);

                if (Pending == true)
                {
                    xmlWriter.WriteStartElement("ResponseDescription");
                    xmlWriter.WriteString(responseDescription);
                    xmlWriter.WriteEndElement();
                }
                if (Approval == true)
                {
                    xmlWriter.WriteStartElement("ResponseDescription");
                    xmlWriter.WriteString(responseDescription);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("TransactionID");
                    xmlWriter.WriteString(transactionID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("TransactionType");
                    xmlWriter.WriteString(transactionType);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Amount");
                    xmlWriter.WriteString(amount);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("CardBrand");
                    xmlWriter.WriteString(cardBrand);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("MaskedAcctNum");
                    xmlWriter.WriteString(maskedAcctNum);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("ExpDate");
                    xmlWriter.WriteString(expDate);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("AcctNumSource");
                    xmlWriter.WriteString(acctNumSource);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Alias");
                    xmlWriter.WriteString(alias);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("ApprovalCode");
                    xmlWriter.WriteString(approvalCode);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("AVSResponseCode");
                    xmlWriter.WriteString(avsResponseCode);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("CardCodeResponse");
                    xmlWriter.WriteString(cardCodeResponse);
                    xmlWriter.WriteEndElement();

                }
                if (OtKTransaction == true)
                {
                    xmlWriter.WriteStartElement("ResponseDescription");
                    xmlWriter.WriteString(responseDescription);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("OtK");
                    xmlWriter.WriteString(otk);
                    xmlWriter.WriteEndElement();
                }
                if (AliasTransaction == true)
                {
                    xmlWriter.WriteStartElement("ResponseDescription");
                    xmlWriter.WriteString(responseDescription);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("TransactionID");
                    xmlWriter.WriteString(transactionID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("TransactionType");
                    xmlWriter.WriteString(transactionType);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("CardBrand");
                    xmlWriter.WriteString(cardBrand);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("MaskedAcctNum");
                    xmlWriter.WriteString(maskedAcctNum);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("ExpDate");
                    xmlWriter.WriteString(expDate);
                    xmlWriter.WriteEndElement();


                    xmlWriter.WriteStartElement("Alias");
                    xmlWriter.WriteString(alias);
                    xmlWriter.WriteEndElement();

                }
                

                //End Root Element
                xmlWriter.WriteEndElement();
                //End Xml Writer
                xmlWriter.WriteEndDocument();

            }
            
            string logContent = logResultFile.ToString();
            string decodedLogContent = HttpUtility.HtmlDecode(logContent);

            // This Text is Always Added, Making the Fiel Longer over time if it is not deleted.
            File.AppendAllText(path, logContent);
           
        }

        public static void saveAlias (string resultXML)
        {
            string parsedAlias = null;
            string transactionID = null;
            string timestamp = DateTime.Now.ToString();

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
            sA.OmitXmlDeclaration = true;

            using (XmlWriter xmlWriter = XmlWriter.Create(saveAliasSB, sA))
            {
                //Start Root Element
                xmlWriter.WriteStartElement("TokenInformation");

                xmlWriter.WriteStartElement("Timestamp");
                xmlWriter.WriteString(timestamp);
                xmlWriter.WriteEndElement();

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
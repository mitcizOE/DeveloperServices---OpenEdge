using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace HPF_eComm_Demo.v2Test
{
    public class BatchReport
    {
        public static string gatewayURL = "https://test.t3secure.net/x-chargeweb.dll";

        public static string buildRequest(string xWebID, string authKey, string terminalID)
        {
            string request;

            string today = DateTime.Today.ToString("MMDDYY");
            string tomorrow = DateTime.Today.AddDays(1).ToString("MMDDYY");

            //create StringBuilder for OTK Request to gateway
            StringBuilder sbRequest = new StringBuilder();

            //Create new Instance of XML Settings , Set Indent
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;

            //Create OTK Request, generates XML request
            using (XmlWriter xmlWriter = XmlWriter.Create(sbRequest, ws))
            {
                //Start Xml Writer
                xmlWriter.WriteStartDocument();
                //Start Root
                xmlWriter.WriteStartElement("GatewayRequest");

                xmlWriter.WriteStartElement("SpecVersion");
                xmlWriter.WriteString("XWeb3.5");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("XWebID");
                xmlWriter.WriteString(xWebID);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("AuthKey");
                xmlWriter.WriteString(authKey);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("TerminalID");
                xmlWriter.WriteString(terminalID);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Industry");
                xmlWriter.WriteString("RETAIL");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("PageNumber");
                xmlWriter.WriteString("1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("RecordsPerPage");
                xmlWriter.WriteString("100");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("PinCapabilities");
                xmlWriter.WriteString("FALSE");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("TrackCapabiltiies");
                xmlWriter.WriteString("BOTH");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Details");
                xmlWriter.WriteString("TRUE");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("POSType");
                xmlWriter.WriteString("PC");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("TransactionType");
                xmlWriter.WriteString("BatchRequestTransaction");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("BatchTransactionType");
                xmlWriter.WriteString("REPORT");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("BatchNum");
                xmlWriter.WriteString("ALL");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

            }
            request = sbRequest.ToString();

            return request;
        }

        public string callGateway(string request)
        {
            string response;

            try
            {
                //Create connection to XWeb test gateway
                HttpWebRequest tokRequest = (HttpWebRequest)WebRequest.Create(gatewayURL);

                //Connection settings
                tokRequest.KeepAlive = false;
                tokRequest.Timeout = 60000;
                tokRequest.Method = "POST";

                //Connection settings
                byte[] tokByteArray = Encoding.ASCII.GetBytes(request);
                tokRequest.ContentType = "application/x-www-form-urlencoded";
                tokRequest.ContentLength = tokByteArray.Length;

                //Create OTK request stream to gateway
                Stream tokDataStream = tokRequest.GetRequestStream();

                //Stream settings 
                tokDataStream.Write(tokByteArray, 0, tokByteArray.Length);
                tokDataStream.Close();

                //Create and save connection response
                WebResponse tokResponse = tokRequest.GetResponse();

                tokDataStream = tokResponse.GetResponseStream();
                StreamReader tokReader = new StreamReader(tokDataStream);


                //Read and save connection response
                response = tokReader.ReadToEnd();

                //Close connections
                tokReader.Close();
                tokDataStream.Close();
                tokResponse.Close();


                return response;


            }
            catch (Exception EX)
            {
                return response = EX.Message;
            }

            
        }
    }
}
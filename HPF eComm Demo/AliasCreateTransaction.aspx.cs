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
using System.Data.SqlClient;

namespace HPF_eComm_Demo
{
    public partial class CheckAliasCreateTransaction : System.Web.UI.Page
    {
        private enum callType { otk, result };
        private static string gatewayURL = "https://test.t3secure.net/x-chargeweb.dll";
        private static string hpfURL = "https://integrator.t3secure.net/hpf/hpf.aspx";

        internal static string otk = null;
        
        private string generateRequest(callType reason)
        {
            string request;

            //Create StringBuilder for OTK request to gateway
            StringBuilder sbRequest = new StringBuilder();

            //Create new instance of XML settings, set indent
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;

            //Create OTK request, generates XML request
            using (XmlWriter xmlWriter = XmlWriter.Create(sbRequest, ws))
            {
                //Start XML writer
                xmlWriter.WriteStartDocument();
                //StartRoot element
                xmlWriter.WriteStartElement("GatewayRequest");

                //Start XWeb credentials
                xmlWriter.WriteStartElement("XWebID");
                xmlWriter.WriteString("800000001377");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("TerminalID");
                xmlWriter.WriteString("80021450");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("AuthKey");
                xmlWriter.WriteString("swUleKfQRuEm6VACYvYkyIuyXQC7jZDv");
                xmlWriter.WriteEndElement();
                //End XWeb credentials

                xmlWriter.WriteStartElement("Industry");
                xmlWriter.WriteString("RETAIL");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("SpecVersion");
                xmlWriter.WriteString("XWebSecure3.4");
                xmlWriter.WriteEndElement();

                if (reason == callType.otk)
                {
                    xmlWriter.WriteStartElement("TransactionType");
                    xmlWriter.WriteString("AliasCreateTransaction");
                    xmlWriter.WriteEndElement();

                    
                }
                else
                {
                    xmlWriter.WriteStartElement("OTK");
                    xmlWriter.WriteString(otk);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("ResponseMode");
                    xmlWriter.WriteString("POLL");
                    xmlWriter.WriteEndElement();
                }

                //End root element
                xmlWriter.WriteEndElement();
                //End XML writer
                xmlWriter.WriteEndDocument();
            }
            request = sbRequest.ToString();

            return request;
        }

        internal string callGateway(string request)
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

        private string parseXML(string toParse, callType reason)
        {
            string parsedData = null;

            //Parse gateway reponse for OTK
            using (XmlTextReader xmlReader = new XmlTextReader(new StringReader(toParse)))
            {
                while (xmlReader.Read())
                {
                    while (xmlReader.ReadToFollowing("ResponseCode"))
                    {
                        string responseCode = xmlReader.ReadElementContentAsString();
                            
                        if (reason == callType.otk)
                        {
                            if (responseCode == "100")
                            {
                                while (xmlReader.ReadToFollowing("OTK"))
                                {
                                    parsedData = xmlReader.ReadElementContentAsString();
                                }
                            }
                        }
                        else
                        {
                            parsedData = responseCode;
                        }
                    }
                }

                xmlReader.Close();

                return parsedData;
            }
        }

        private string parseAlias(string toParse, callType reason)
        {
            string parsedData = null;

            //parse Gateway for Alias
            using (XmlTextReader xmlReader = new XmlTextReader(new StringReader(toParse)))
            {
                while (xmlReader.Read())
                {
                    while (xmlReader.ReadToFollowing("ResponseCode"))
                    {
                        string responseCode = xmlReader.ReadElementContentAsString();

                        if (reason == callType.result)
                        {
                            if (responseCode == "005")
                            {
                                while (xmlReader.ReadToFollowing("Alias"))
                                {
                                    parsedData = xmlReader.ReadElementContentAsString();
                                }
                            }
                        }
                        else
                        {
                            parsedData = responseCode;
                        }
                    }
                }
                xmlReader.Close();

                return parsedData;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get OTK

            //Send to Gateway.

            //Set iFrame URL.
            if (!Page.IsPostBack)
            {
                string request = generateRequest(callType.otk);

                string result = callGateway(request);

                //Parse the OTK from the OTK request.
                otk = parseXML(result, callType.otk);

                if (!String.IsNullOrEmpty(otk))
                {
                    //Display the Hosted Payment Form within an iFrame on your page.
                    xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
                }
            }
        }

        protected void TimerResultCall_Tick(object sender, EventArgs e)
        {
            string responseCode = null;

            string cardAlias = null;
            
            string request = generateRequest(callType.result);

            string result = callGateway(request);

            //Parse the OTK from the OTK request.
            responseCode = parseXML(result, callType.result);

            cardAlias = parseAlias(result, callType.result);
            
            /*if (responseCode == "000")
            {
                redirect(true);
            }
            */

            if (cardAlias != null)
            {
                TextBoxResultDisplay.Visible = true;
                TextBoxResultDisplay.Text = "Credit Card Alias:" + Environment.NewLine;
                TextBoxResultDisplay.Text += cardAlias.ToString() + Environment.NewLine;       
            }
            
            
        }

        private void redirect(bool success)
        {
            if (success)
            {
                HttpContext.Current.Response.Redirect("success.aspx", false);
            }
        }
    }
}
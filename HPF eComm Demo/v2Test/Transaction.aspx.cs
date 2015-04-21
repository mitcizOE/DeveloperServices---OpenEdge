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

namespace HPF_eComm_Demo
{
    public partial class Transaction : System.Web.UI.Page
    {
        private enum callType { otk, result };
        public enum TranType { CreditSale, DebitSale, CreditReturn, DebitReturn, AliasCreate, ResultsCall };
        private static string gatewayURL = "https://test.t3secure.net/x-chargeweb.dll";
        private static string hpfURL = "https://integrator.t3secure.net/hpf/hpf.aspx";

        internal static string otk = null;
        
        private string generateRequest(callType reason, TranType type)
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
                xmlWriter.WriteString("800000001844");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("TerminalID");
                xmlWriter.WriteString("80022706");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("AuthKey");
                xmlWriter.WriteString("zWCvjz8pZcy10t5QWHsex7jRhwl992jd");
                xmlWriter.WriteEndElement();
                //End XWeb credentials

                xmlWriter.WriteStartElement("Industry");
                xmlWriter.WriteString("RETAIL");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("SpecVersion");
                xmlWriter.WriteString("XWebSecure3.4");
                xmlWriter.WriteEndElement();

                //Transaction Type Writer
                if (reason == callType.otk)
                {
                    if (type == TranType.CreditSale)
                    {
                        xmlWriter.WriteStartElement("TransactionType");
                        xmlWriter.WriteString("CreditSaleTransaction");
                        xmlWriter.WriteEndElement();
                    }

                    if (type == TranType.CreditReturn)
                    {
                        xmlWriter.WriteStartElement("TransactionType");
                        xmlWriter.WriteString("CreditReturnTransaction");
                        xmlWriter.WriteEndElement();
                    }

                    if (type == TranType.DebitSale)
                    {
                        xmlWriter.WriteStartElement("TransactionType");
                        xmlWriter.WriteString("DebitSaleTransaction");
                        xmlWriter.WriteEndElement();
                    }

                    if (type == TranType.DebitReturn)
                    {
                        xmlWriter.WriteStartElement("TransactionType");
                        xmlWriter.WriteString("DebitReturnTransaction");
                        xmlWriter.WriteEndElement();
                    }
                    if (type == TranType.AliasCreate)
                    {
                        xmlWriter.WriteStartElement("TransactionType");
                        xmlWriter.WriteString("AliasCreateTransaction");
                        xmlWriter.WriteEndElement();
                    }
                    
                    if (type != TranType.AliasCreate)
                    {
                        //Flags Transaction to return Alias.
                        xmlWriter.WriteStartElement("CreateAlias");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("DuplicateMode");
                        xmlWriter.WriteString("CHECKING_OFF");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("Amount");
                        xmlWriter.WriteString("1.00");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("AmountLocked");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("ShowReceipt");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();
                    }
                 
                }
                if (type == TranType.ResultsCall)
                {
                    xmlWriter.WriteStartElement("OTK");
                    xmlWriter.WriteString(otk);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("ResponseMode");
                    xmlWriter.WriteString("POLL");
                    xmlWriter.WriteEndElement();
                }

                else
                {

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
            /*if (!Page.IsPostBack)
            {
                string request = generateRequest(callType.otk, );

                string result = callGateway(request);

                //Parse the OTK from the OTK request.
                otk = parseXML(result, callType.otk);

                if (!String.IsNullOrEmpty(otk))
                {
                    //Display the Hosted Payment Form within an iFrame on your page.
                    xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
                }
            }*/
        }

        protected void TimerResultCall_Tick(object sender, EventArgs e)
        {
            string responseCode = null;

            string cardAlias = null;
            
            string request = generateRequest(callType.result, TranType.ResultsCall);

            string result = callGateway(request);

            //Parse the OTK from the OTK request.
            responseCode = parseXML(result, callType.result);

            cardAlias = parseAlias(result, callType.result);
            /*if (responseCode == "000")
            {
                redirect(true);
            }
            */
            TextBoxResultDisplay.Text += responseCode.ToString() + Environment.NewLine;

            if (cardAlias != null)
            {
                TextBoxResultDisplay.Visible = true;
                TextBoxResultDisplay.Text = "Alias:" + Environment.NewLine;
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

        protected void creditSaleTransaction_Click(object sender, EventArgs e)
        {

            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");

            //Call and  Go
            request = generateRequest(callType.otk, TranType.CreditSale);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            creditSaleTransaction.BackColor = System.Drawing.Color.SteelBlue;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }

        }

        protected void creditReturnTransaction1_Click(object sender, EventArgs e)
        {
            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");
            
            request = generateRequest(callType.otk, TranType.CreditReturn);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            creditReturnTransaction.BackColor = System.Drawing.Color.SteelBlue;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }
        }

        protected void debitSaleTransaction_Click(object sender, EventArgs e)
        {
            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");

            request = generateRequest(callType.otk, TranType.DebitSale);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            debitSaleTransaction.BackColor = System.Drawing.Color.SteelBlue;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }
        }

        protected void debitReturnTransaction_Click(object sender, EventArgs e)
        {
            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");

            request = generateRequest(callType.otk, TranType.DebitReturn);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            debitReturnTransaction.BackColor = System.Drawing.Color.SteelBlue;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }
        }

        protected void createAliasTransaction_Click(object sender, EventArgs e)
        {
            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");

            request = generateRequest(callType.otk, TranType.AliasCreate);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.SteelBlue;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }


        }

        
    }
}
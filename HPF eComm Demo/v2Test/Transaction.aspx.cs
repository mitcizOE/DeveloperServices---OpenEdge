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
        public enum TranType { CreditSale, DebitSale, CreditReturn, DebitReturn, AliasCreate, ResultsCall, CheckAlias, CreditEMV, CreditEMVReturn, CheckSale, CheckCredit };
        private static string gatewayURL = "https://test.t3secure.net/x-chargeweb.dll";
        private static string hpfURL = "https://integrator.t3secure.net/hpf/hpf.aspx";


        //Custom XWEb Creds
        public static string xWebIDCustom;
        public static string authKeyCustom;
        public static string terminalIDCustom;
        public static string industryCustom;
        public static bool useCustomCreds = false;

        //For Result call Usage ONLY! (Sigh) Custom Creds Result Calls NYI
        public static string currentXWebID;
        public static string currentAuthKey;
        public static string currentTerminalID;

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

                //Check for Custom Creds
                if (useCustomCreds == false)
                {
                    // Standard XWeb Creds
                    if (type == TranType.CreditSale || type == TranType.CreditReturn || type == TranType.AliasCreate)
                    {
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
                    }
                    //Credit EMV Creds (PG)
                    if (type == TranType.CreditEMV || type == TranType.CreditEMVReturn)
                    {
                        //Start XWeb credentials
                        xmlWriter.WriteStartElement("XWebID");
                        xmlWriter.WriteString("800000001694");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("TerminalID");
                        xmlWriter.WriteString("80022125");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("AuthKey");
                        xmlWriter.WriteString("t6Gr99v6eo8xfLFSzYieujuPLkkDfbHI");
                        xmlWriter.WriteEndElement();
                        //End XWeb credentials

                        xmlWriter.WriteStartElement("Industry");
                        xmlWriter.WriteString("RETAIL");
                        xmlWriter.WriteEndElement();
                    }
                    //Debit EMV Creds (PG)
                    if (type == TranType.DebitSale || type == TranType.DebitReturn)
                    {
                        //Start XWeb credentials
                        xmlWriter.WriteStartElement("XWebID");
                        xmlWriter.WriteString("800000001694");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("TerminalID");
                        xmlWriter.WriteString("80022120");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("AuthKey");
                        xmlWriter.WriteString("NAFV4pikCkOydaS2SXZbArEdcc2xXlkj");
                        xmlWriter.WriteEndElement();
                        //End XWeb credentials

                        xmlWriter.WriteStartElement("Industry");
                        xmlWriter.WriteString("RETAIL");
                        xmlWriter.WriteEndElement();
                    }
                    //Check Creds
                    if (type == TranType.CheckAlias || type == TranType.CheckSale || type == TranType.CheckCredit)
                    {
                        //Start XWeb credentials
                        xmlWriter.WriteStartElement("XWebID");
                        xmlWriter.WriteString("800000001844");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("TerminalID");
                        xmlWriter.WriteString("80022690");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("AuthKey");
                        xmlWriter.WriteString("XisLfXu2QVDYuBSt4k4r9go7ZELxRlie");
                        xmlWriter.WriteEndElement();
                        //End XWeb credentials
                    }
                }
                //Use Custom Creds
                if (useCustomCreds == true)
                {
                    xmlWriter.WriteStartElement("XWebID");
                    xmlWriter.WriteString(xWebIDCustom);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("TerminalID");
                    xmlWriter.WriteString(terminalIDCustom);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("AuthKey");
                    xmlWriter.WriteString(authKeyCustom);
                    xmlWriter.WriteEndElement();

                    if (industryCustom != null || industryCustom != "")
                    {
                        xmlWriter.WriteStartElement("Industry");
                        xmlWriter.WriteString(industryCustom);
                        xmlWriter.WriteEndElement();
                    }
                }

                xmlWriter.WriteStartElement("SpecVersion");
                xmlWriter.WriteString("XWebSecure3.5");
                xmlWriter.WriteEndElement();

                //Transaction Type Writer
                if (reason == callType.otk)
                {
                    if (type == TranType.CreditSale || type == TranType.CreditEMV)
                    {
                        xmlWriter.WriteStartElement("TransactionType");
                        xmlWriter.WriteString("CreditSaleTransaction");
                        xmlWriter.WriteEndElement();
                    }

                    if (type == TranType.CreditReturn || type == TranType.CreditEMVReturn)
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
                    if (type == TranType.CheckSale)
                    {
                        xmlWriter.WriteStartElement("TransactionType");
                        xmlWriter.WriteString("CheckSaleTransaction");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("EntryClass");
                        xmlWriter.WriteString("WEB");
                        xmlWriter.WriteEndElement();
                    }
                    if (type == TranType.CheckCredit)
                    {
                        xmlWriter.WriteStartElement("TransactionType");
                        xmlWriter.WriteString("CheckCreditTransaction");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("EntryClass");
                        xmlWriter.WriteString("PPD");
                        xmlWriter.WriteEndElement();
                    }
                    if (type == TranType.CheckAlias)
                    {
                        xmlWriter.WriteStartElement("TransactionType");
                        xmlWriter.WriteString("CheckAliasCreateTransaction");
                        xmlWriter.WriteEndElement();
                    }
                    
                    //Flag to Create Alias for Transactiosn that Support CreateAlias = TRUE
                    if (type != TranType.AliasCreate && type != TranType.DebitSale && type != TranType.DebitReturn && type != TranType.CheckAlias)
                    {
                        if (type != TranType.CheckCredit && type != TranType.CheckSale)
                        {
                            //Flags Transaction to return Alias.
                            xmlWriter.WriteStartElement("CreateAlias");
                            xmlWriter.WriteString("TRUE");
                            xmlWriter.WriteEndElement();

                            xmlWriter.WriteStartElement("DuplicateMode");
                            xmlWriter.WriteString("CHECKING_OFF");
                            xmlWriter.WriteEndElement();
                            /*
                            xmlWriter.WriteStartElement("ShowReceipt");
                            xmlWriter.WriteString("TRUE");
                            xmlWriter.WriteEndElement();
                             */
                        }

                        xmlWriter.WriteStartElement("Amount");
                        xmlWriter.WriteString("1.00");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("AmountLocked");
                        xmlWriter.WriteString("TRUE");
                        xmlWriter.WriteEndElement();

                        

                    }
                 
                }

                //Results call Writer
                if (type == TranType.ResultsCall)
                {
                    xmlWriter.WriteStartElement("XWebID");
                    xmlWriter.WriteString(currentXWebID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("TerminalID");
                    xmlWriter.WriteString(currentTerminalID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("AuthKey");
                    xmlWriter.WriteString(currentAuthKey);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("OTK");
                    xmlWriter.WriteString(otk);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("ResponseMode");
                    xmlWriter.WriteString("POLL");
                    xmlWriter.WriteEndElement();
                }

                // Else if for not
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
            DB.DBFunctions.SaveResult(toParse);
                    
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
            DB.DBFunctions.saveAlias(toParse);

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
                            if (responseCode == "005" || responseCode == "000")
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

            

            //Hardcoded Credit XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001844";
            currentAuthKey = "zWCvjz8pZcy10t5QWHsex7jRhwl992jd";
            currentTerminalID = "80022706";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }

            //Call and  Go
            request = generateRequest(callType.otk, TranType.CreditSale);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.SteelBlue;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;

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

            //Hardcoded Credit XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001844";
            currentAuthKey = "zWCvjz8pZcy10t5QWHsex7jRhwl992jd";
            currentTerminalID = "80022706";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }
            
            request = generateRequest(callType.otk, TranType.CreditReturn);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.SteelBlue;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;

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

            //Hardcoded Debit XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001694";
            currentAuthKey = "NAFV4pikCkOydaS2SXZbArEdcc2xXlkj";
            currentTerminalID = "80022120";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }

            request = generateRequest(callType.otk, TranType.DebitSale);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.SteelBlue;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;

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

            //Hardcoded Debit XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001694";
            currentAuthKey = "NAFV4pikCkOydaS2SXZbArEdcc2xXlkj";
            currentTerminalID = "80022120";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }

            request = generateRequest(callType.otk, TranType.DebitReturn);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.SteelBlue;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;

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

            //Hardcoded Credit XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001844";
            currentAuthKey = "zWCvjz8pZcy10t5QWHsex7jRhwl992jd";
            currentTerminalID = "80022706";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }

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
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }
        }

        protected void checkSaleTransaction_Click(object sender, EventArgs e)
        {
            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");

            //Hardcoded Check XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001844";
            currentAuthKey = "XisLfXu2QVDYuBSt4k4r9go7ZELxRlie";
            currentTerminalID = "80022690";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }

            request = generateRequest(callType.otk, TranType.CheckSale);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.SteelBlue;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;

            /* Placeholder Button Changing Color AWesome!
            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;
             */


            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }
        }

        protected void checkCreditTransaction_Click(object sender, EventArgs e)
        {
            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");

            //Hardcoded Check XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001844";
            currentAuthKey = "XisLfXu2QVDYuBSt4k4r9go7ZELxRlie";
            currentTerminalID = "80022690";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }

            request = generateRequest(callType.otk, TranType.CheckCredit);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.SteelBlue;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }
        }

        protected void checkAliasCreateTransaction_Click(object sender, EventArgs e)
        {
            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");

            //Hardcoded Check XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001844";
            currentAuthKey = "XisLfXu2QVDYuBSt4k4r9go7ZELxRlie";
            currentTerminalID = "80022690";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }

            request = generateRequest(callType.otk, TranType.CheckAlias);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.SteelBlue;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }
        }

        protected void emvCreditSaleTransaction_Click(object sender, EventArgs e)
        {
            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");

            //Hardcoded Credit EMV XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001694";
            currentAuthKey = "t6Gr99v6eo8xfLFSzYieujuPLkkDfbHI";
            currentTerminalID = "80022125";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }

            request = generateRequest(callType.otk, TranType.CreditEMV);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.SteelBlue;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }
        }

        protected void emvCreditReturnTransaction_Click(object sender, EventArgs e)
        {
            //Clearing all the things
            string request = "";
            string result = "";
            xwebIFrame.Attributes.Add("src", "about.blank");

            //Hardcoded Credit EMV XWeb ID for Results Calls (Custom Creds NYI!)
            currentXWebID = "800000001694";
            currentAuthKey = "t6Gr99v6eo8xfLFSzYieujuPLkkDfbHI";
            currentTerminalID = "80022125";

            if (boxCustomCreds.Checked)
            {
                currentXWebID = xWebIDCustom;
                currentAuthKey = authKeyCustom;
                currentTerminalID = terminalIDCustom;
            }

            request = generateRequest(callType.otk, TranType.CreditEMVReturn);
            otkCall.Text = request;

            result = callGateway(request);
            resultsXML.Text = result;

            createAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            debitSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditReturnTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkAliasCreateTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            emvCreditReturnTransaction.BackColor = System.Drawing.Color.SteelBlue;
            checkSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
            checkCreditTransaction.BackColor = System.Drawing.Color.DarkGray;

            //Parse the OTK from the OTK request.
            otk = parseXML(result, callType.otk);

            if (!String.IsNullOrEmpty(otk))
            {
                //Display the Hosted Payment Form within an iFrame on your page.
                xwebIFrame.Attributes.Add("src", hpfURL + "?otk=" + otk);
            }
        }


        
        protected void HideButton_Click(object sender, EventArgs e)
        {
            //Hide Panel and make Advanced Button Visible.
            CustomCredsPanel.Visible = false;
            AdvancedButton.BackColor = System.Drawing.Color.SteelBlue;

        }

        protected void CustomCredsButton_Click(object sender, EventArgs e)
        {
            //Show Panel and Hide Advanced Button
            CustomCredsPanel.Visible = true;
            AdvancedButton.BackColor = System.Drawing.Color.SteelBlue;
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            
            //Save Custom XWeb Params, make Advance Button Non Visible (just cuz)
            xWebIDCustom = customXWebID.Text;
            authKeyCustom = customAuthKey.Text;
            terminalIDCustom = customTerminalID.Text;
            industryCustom = customIndustry.Text;

            if (boxCustomCreds.Checked)
            {
                useCustomCreds = true;
            }

            if (!boxCustomCreds.Checked)
            {
                useCustomCreds = false;
            }

        }

        public void resultCallParse()
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

        protected void TextBoxResultDisplay_DataBinding(object sender, EventArgs e)
        {

        }


  

    }
}
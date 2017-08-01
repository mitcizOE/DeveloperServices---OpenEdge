using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPF_eComm_Demo.OEHP
{
    public partial class Success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string orderID = Request.QueryString["order_id"].ToString();
            string responseCode = Request.QueryString["response_code"].ToString();
            if (responseCode == "1")
            {
                HostPayFunctions.TransactionRequest tr = new HostPayFunctions.TransactionRequest();
                string parameters = tr.DirectPostBuilder(HostPayFunctions.DemoVariables.AccountToken, orderID, "CREDIT_CARD", "QUERY");
                HostPayFunctions.GatewayRequest gr = new HostPayFunctions.GatewayRequest();
                string result = gr.TestDirectPost(parameters);
                NameValueCollection parsedResult = HttpUtility.ParseQueryString(result);
                TransactionStatus.Text = "Your Payment Has Been Accepted." + Environment.NewLine + "TeeTime Date " + HostPayFunctions.DemoVariables.TeeTimeDate + Environment.NewLine + "Authorization Code: " + parsedResult["bank_approval_code"];
            }
            else
            {
                TransactionStatus.Text = "Your payment has declined, please call" + Environment.NewLine + "your Pool Service Provider or re-attempt" + Environment.NewLine + "your transaction at a later time";
            }

        }
    }
}
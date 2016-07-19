using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OEHP.NET;

namespace HostPayFunctions
{
    public partial class OEHPDemo : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SaleButton_Click(object sender, EventArgs e)
        {
            try
            {
                TransactionRequest tr = new TransactionRequest();
                GatewayRequest gr = new GatewayRequest();
                string otk = gr.TestPayPagePost(tr.CreditCardParamBuilder(DemoVariables.AccountToken, "CREDIT_CARD", "SALE", "KEYED", tr.OrderIDRandom(8), "1.00", DemoVariables.CustomKeyPairs));
                oehpIFrame.Attributes.Add("src", otk);

            }
            catch
            {

            }
        }
    }

    
}
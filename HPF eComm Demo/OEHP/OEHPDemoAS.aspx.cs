using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HostPayFunctions
{
    public partial class OEHPDemoAS : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                TransactionRequest tr = new TransactionRequest();
                GatewayRequest gr = new GatewayRequest();
                string otk = gr.TestPayPagePost(tr.CreditCardParamBuilder(DemoVariables.AccountToken, "CREDIT_CARD", ChargeTypeDropDown.SelectedValue, CustomerTypeDropDown.SelectedValue, tr.OrderIDRandom(8), AmountBox.Text, DemoVariables.CustomKeyPairs));
                oehpIFrame.Attributes.Add("src", otk);

            }
            catch
            {

            }
        }

        protected void AmountBox_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(AmountBox.Text, @"[0-9]+(\.[0-9][0-9]?)?"))
            {
                AmountBox.BackColor = System.Drawing.Color.Red;
            }


            decimal value;
            if (!decimal.TryParse(AmountBox.Text, out value))
            {
                AmountBox.BackColor = System.Drawing.Color.Red;
            }
        }
        
    }

    
}
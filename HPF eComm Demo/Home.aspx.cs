using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace HPF_eComm_Demo
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                    }

        protected void creditSaleTransaction_Click(object sender, EventArgs e)
        {
            hpfIFrame.Attributes.Add("src", "CreditSaleTransaction.aspx");
            creditSaleTransaction.BackColor = System.Drawing.Color.SteelBlue;
            createAliasButton.BackColor = System.Drawing.Color.DarkGray;
            checkAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
        }

        protected void createAliasButton_Click(object sender, EventArgs e)
        {
            hpfIFrame.Attributes.Add("src", "AliasCreateTransaction.aspx");
            createAliasButton.BackColor = System.Drawing.Color.SteelBlue;
            checkAliasTransaction.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;

        }

        protected void checkAliasTransaction_Click(object sender, EventArgs e)
        {
            hpfIFrame.Attributes.Add("src", "CheckAliasCreateTransaction.aspx");
            checkAliasTransaction.BackColor = System.Drawing.Color.SteelBlue;
            createAliasButton.BackColor = System.Drawing.Color.DarkGray;
            creditSaleTransaction.BackColor = System.Drawing.Color.DarkGray;
        }
    }
}
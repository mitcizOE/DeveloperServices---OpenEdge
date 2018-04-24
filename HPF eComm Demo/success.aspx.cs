using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPF_eComm_Demo
{
    public partial class success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonRedirect_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("UpdatePanelResultDemo.aspx", false);
        }
    }
}
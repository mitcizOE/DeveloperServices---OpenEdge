using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPF_eComm_Demo
{
    public partial class HomeEMV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hpfIFrame.Attributes.Add("src", "HomeEMV.aspx");
        }
    }
}
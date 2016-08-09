using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HostPayFunctions
{
    public partial class Postback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string results = ReceivePostback();
            string doStuff = null;
        }
        public string ReceivePostback()
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
            string results = sr.ReadToEnd();
            return results;
        }
    }
}
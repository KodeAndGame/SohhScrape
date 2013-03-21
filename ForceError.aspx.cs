using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SohhScrape
{
    public partial class ForceError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            throw new Exception("This is a test");
        }
    }
}
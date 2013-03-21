using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataScrapingHelper;
using System.Configuration;

namespace SohhScrape
{
    public partial class MobileSohhMaster : System.Web.UI.MasterPage
    {
        public String XslTransformedContent
        {
            get
            {
                return xslTransformedContent.Text;
            }
            set
            {
                xslTransformedContent.Text = value;
            }
        }
    }
}
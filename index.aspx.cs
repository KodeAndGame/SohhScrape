using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohhScrape
{
    public partial class index : MobileSohhBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "MPiff.com";
            base.Page_Load(sender, e);
        }
    }
}
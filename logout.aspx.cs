using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using DataScrapingHelper;

namespace SohhScrape
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //When user logs out clear session variables...
            Session["cookies"] = new CookieContainer();
            Session["username"] = String.Empty;

            //... and Cookies
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(aCookie);
            }

            //Redirect to home page
            Response.Redirect("index.aspx");


        }
    }
}
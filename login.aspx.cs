using System;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using DataScrapingHelper;
using HtmlAgilityPack;

namespace SohhScrape
{
    public partial class login : System.Web.UI.Page
    {
        protected const String SOHH_HOSTNAME = "http://forums.projectcovo.com";

        protected String Username
        {
            get
            {
                return Request.Form["username"];
            }
        }
        protected String HashedPassword
        {
            get
            {
                return Request.Form["vb_login_md5password"];
            }
        }
        protected String HashedPasswordUtf
        {
            get
            {
                return Request.Form["vb_login_md5password_utf"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Log In";

            //Once user has submitted form, log them into SOHH
            if (Page.IsPostBack)
            {
                //Submit form using provided data
                HtmlDocumentScraper scraper = new HtmlDocumentScraper("http://forums.projectcovo.com/login.php?do=login", String.Empty);
                String postData = String.Format("vb_login_username={0}&cookieuser=1&vb_login_password=&s=&securitytoken=guest&do=login&vb_login_md5password={1}&vb_login_md5password_utf={2}",
                    Username, HashedPassword, HashedPasswordUtf);
                CookieContainer sessionCookies = Session["cookies"] as CookieContainer;
                scraper.SubmitForm(ref sessionCookies, postData);
                Session["cookies"] = sessionCookies;

                //Check status of SOHH login
                HtmlNode errorNode = scraper.HtmlDocument.DocumentNode.SelectSingleNode("//td[@class='panelsurround']/div[@class='panel']/div/div");
                HtmlNode successNode = scraper.HtmlDocument.DocumentNode.SelectSingleNode("//td[@class='panelsurround']");
                int usernameIndex = successNode.InnerText.IndexOf(Username, StringComparison.OrdinalIgnoreCase);

                if (successNode != null && errorNode == null && usernameIndex >= 0)
                {
                    
                    //Successful login - add Session variables and cookies
                    Session["username"] = successNode.InnerText.Substring(usernameIndex, Username.Length);
                    var authTicket = new FormsAuthenticationTicket(
                                          1,
                                          Session["username"].ToString(),
                                          DateTime.Now,
                                          DateTime.Now.AddHours(3),
                                          true,
                                          String.Empty);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authenticationCookie); 

                    Response.SetCookie(new HttpCookie("username", Session["username"].ToString()));

                    CookieCollection sessionCookieCollection = sessionCookies.GetCookies(new Uri(SOHH_HOSTNAME));
                    foreach (Cookie cookie in sessionCookieCollection)
                    {
                        String cookieName = "SOHH_" + cookie.Name;
                        Response.Cookies[cookieName].Value = cookie.Value;
                        Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(30);
                    }                    

                    //Redirect back to index
                    Response.Redirect("index.aspx");
                }
                else
                {
                    //Failed login - explain why
                    errorMessage.Text = errorNode.InnerHtml + "<br/><br/>";
                }

            }
        }
    }
}
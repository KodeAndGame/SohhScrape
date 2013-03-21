using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Net;

namespace SohhScrape
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            HttpContext ctx = HttpContext.Current;

            Exception ex = ctx.Server.GetLastError();
            ErrorLogger.HandleException(ex);


            String errorMsg = String.Empty;
            String exceptionDetails = String.Empty;

            if (ex.GetType() == typeof(System.Net.WebException) || 
                (ex.InnerException != null && ex.InnerException.GetType() == typeof(System.Net.WebException)))
            {
                errorMsg = "It seems MPiff.com was not able to contact the SOHH servers. Maybe the hamsters gave out - try again later <img src='images/smilies/old.gif' />";
            }
            else
            {
                errorMsg = "We're sorry - an error has occurred on MPiff.com. The details have been logged... we'll get to the bottom of this <img src='images/smilies/scheme.gif' />";
            }

            exceptionDetails = "<br>Offending URL: " + ctx.Request.Url.ToString() +
               "<br>Source: " + ex.Source +
               "<br>Message: " + ex.Message +
               "<br>Stack trace: " + ex.StackTrace;

            String errorHtml = String.Format(@"<html>
<head runat='server'>
    <title>Error Occurred</title>
    <link href='../images/errors.css' rel='stylesheet' type='text/css' />
    <link rel='icon' type='image/vnd.microsoft.icon' href='/images/favicon.ico'>
    <link rel='apple-touch-icon' href='/images/apple-touch-icon.png'>
    <meta name='viewport' content='width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no;'>
</head>
<body>
    <form id='form1' runat='server'>
    <div>
        <br />
        <br />

        <div class='header' id='header'>An Error Has Occurred <img src='images/smilies/bron.gif' /></div>
        <br />
        {0}

        <br/>
        <br/>

    </div>
    </form>
</body>
</html>", errorMsg);

            ctx.Response.Write(errorHtml);

            // --------------------------------------------------
            // To let the page finish running we clear the error
            // --------------------------------------------------
            ctx.Server.ClearError();

        }

        void Session_Start(object sender, EventArgs e)
        {
            //Process Cookies
            CookieContainer sessionCookies = new CookieContainer();
            Session["username"] = String.Empty;

            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                if (Request.Cookies[i].Name == "username")
                {
                    Session["username"] = Request.Cookies[i].Value;
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
                }
                else if (Request.Cookies[i].Name.StartsWith("SOHH_"))
                {
                    sessionCookies.Add(new Cookie(Request.Cookies[i].Name.Replace("SOHH_", String.Empty), Request.Cookies[i].Value, "/", "forums.projectcovo.com"));
                }
            }

            Session["cookies"] = sessionCookies;
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}

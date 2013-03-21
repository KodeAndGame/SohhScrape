using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataScrapingHelper;
using System.Net;
using HtmlAgilityPack;

namespace SohhScrape
{
    public partial class post : System.Web.UI.Page
    {
        protected const String SOHH_POSTPAGE_URL = "http://forums.projectcovo.com/newreply.php?{0}";
        protected const String SOHH_EDITPAGE_URL = "http://forums.projectcovo.com/editpost.php?do=editpost&postid={0}";
        protected const String SOHH_NEWTHREAD_URL = "http://forums.projectcovo.com/newthread.php?do=newthread&f={0}";

        protected const String NEWREPLY_POST_DATA =
            @"title=&message={0}&wysiwyg=0&iconid=0&s=&securitytoken={1}&do=postreply&t={2}&p={3}&posthash=&poststarttime=&loggedinuser={4}&multiquoteempty=&sbutton=Submit+Reply&signature=1&parseurl=1&emailupdate={5}&folderid=0&rating=0";
        protected const String UPDATEPOST_POST_DATA =
            @"reason=&title=&message={0}&wysiwyg=0&iconid=0&s=&securitytoken={1}&do=updatepost&p={2}&posthash=&poststarttime=&sbutton=Save+Changes&signature=1&parseurl=1&emailupdate={3}&folderid=0";
        protected const String NEWTHREAD_POST_DATA =
            @"subject={0}&message={1}&wysiwyg=0&iconid=0&s=&securitytoken={2}&f={3}&do=postthread&posthash=&poststarttime=&loggedinuser={4}&sbutton=Submit+New+Thread&signature=1&parseurl=1&emailupdate={5}&folderid=0&polloptions=4";

        protected const int SECURITYTOKEN_PREFIX_LENGTH = 21;

        protected bool IsNewReply { set; get; }
        protected String CurrentPage
        {
            get
            {
                return Request.QueryString["page"];
            }
        }
        protected String ReferredPostId
        {
            get
            {
                return Request.QueryString["p"];
            }
        }
        protected String ThreadName { get; set; }
        protected String SecurityToken { get; set; }
        protected String LoggedInUserId { get; set; }
        protected String ThreadId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            #region First Visit
            if (!Page.IsPostBack)
            {
                String action = Request.QueryString["do"];
                IsNewReply = Request.QueryString["do"] == "newreply";
                String webPageUrl = String.Empty;

                switch (action)
                {
                    case "newreply":
                        Page.Title = "New Reply";
                        webPageUrl = String.Format(SOHH_POSTPAGE_URL, Request.QueryString);
                        break;
                    case "editpost":
                        Page.Title = "Edit Post";
                        webPageUrl = String.Format(SOHH_EDITPAGE_URL, Request.QueryString["p"]);
                        break;
                    case "newthread":
                        Page.Title = "New Thread";
                        webPageUrl = String.Format(SOHH_NEWTHREAD_URL, Request.QueryString["f"]);
                        break;
                    default:
                        break;
                }

                HtmlDocumentScraper scraper = new HtmlDocumentScraper(webPageUrl, String.Empty);
                CookieContainer cookies = Session["cookies"] as CookieContainer;
                scraper.LoadWebPage(ref cookies);
                Session["cookies"] = cookies;

                message.Text = Server.HtmlDecode(scraper.HtmlDocument.DocumentNode.SelectSingleNode("//td[@class='controlbar']/textarea").InnerText);

                HtmlNode threadNameNode = scraper.HtmlDocument.DocumentNode.SelectSingleNode("//span[@class='navbar']/a[starts-with(@href, 'showthread.php?p=')]");
                if (threadNameNode != null)
                {
                    ThreadName = threadNameNode.InnerText;
                }

                int securityTokenBegin = scraper.HtmlDocument.DocumentNode.InnerText.IndexOf("var SECURITYTOKEN = \"");
                int securityTokenEnd = scraper.HtmlDocument.DocumentNode.InnerText.IndexOf("\";", securityTokenBegin);

                SecurityToken = scraper.HtmlDocument.DocumentNode.InnerText.Substring(
                    securityTokenBegin + SECURITYTOKEN_PREFIX_LENGTH, (securityTokenEnd - (securityTokenBegin + SECURITYTOKEN_PREFIX_LENGTH)));

                String userIdLink = scraper.HtmlDocument.DocumentNode.SelectSingleNode("//a[starts-with(@href, 'member.php?u=')]").Attributes["href"].Value;
                LoggedInUserId = userIdLink.Substring(userIdLink.IndexOf('='));
                //LoggedInUserId = scraper.HtmlDocument.DocumentNode.SelectSingleNode("//input[@name='loggedinuser']").Attributes["value"].Value;

                //Get Unique Thread Id
                HtmlNode threadNode = scraper.HtmlDocument.DocumentNode.SelectSingleNode("//input[@name='t']");
                if (Request.QueryString["t"] != null && Request.QueryString["t"] != String.Empty)
                {
                    ThreadId = Request.QueryString["t"];
                }
                else if (threadNode != null)
                {
                    ThreadId = threadNode.Attributes["value"].Value;
                }

                hThreadId.Value = ThreadId;
                hReferredPostId.Value = ReferredPostId;
                hPageNumber.Value = Request.QueryString["page"];
                hForumId.Value = Request.QueryString["f"];
                hSecurityToken.Value = SecurityToken;
                hLoggedInUserId.Value = LoggedInUserId;
                hDo.Value = Request.QueryString["do"];
            }
            #endregion First Visit
            #region Postback
            else
            {
                String action = hDo.Value;
                String webPageUrl = String.Empty;
                String postData = String.Empty;

                switch (action)
                {
                    case "newreply":
                        webPageUrl = String.Format(SOHH_POSTPAGE_URL, "do=postreply&t=" + hThreadId.Value);
                        postData = String.Format(NEWREPLY_POST_DATA,
                           HttpUtility.UrlEncode(message.Text),
                           hSecurityToken.Value,
                           hThreadId.Value,
                           hLoggedInUserId.Value,
                           hReferredPostId.Value,
                           ddlEmailUpdate.SelectedValue);
                        break;
                    case "editpost":
                        webPageUrl = String.Format(SOHH_EDITPAGE_URL, hReferredPostId.Value);
                        postData = String.Format(UPDATEPOST_POST_DATA,
                            HttpUtility.UrlEncode(message.Text),
                            hSecurityToken.Value,
                            hReferredPostId.Value,
                            ddlEmailUpdate.SelectedValue);
                        break;
                    case "newthread":
                        webPageUrl = String.Format(SOHH_NEWTHREAD_URL, hForumId.Value);
                        postData = String.Format(NEWTHREAD_POST_DATA,
                            HttpUtility.UrlEncode(txtTitle.Text),
                            HttpUtility.UrlEncode(message.Text),
                            hSecurityToken.Value,
                            hForumId.Value,
                            hLoggedInUserId.Value,
                            ddlEmailUpdate.SelectedValue);
                        break;
                    default:
                        break;
                }

                HtmlDocumentScraper scraper = new HtmlDocumentScraper(webPageUrl, String.Empty);
                CookieContainer cookies = Session["cookies"] as CookieContainer;
                HttpWebResponse response;
                scraper.SubmitForm(ref cookies, postData, out response);
                Session["cookies"] = cookies;
                Response.Redirect("showthread.aspx" + response.ResponseUri.Query);
            }
            #endregion Postback
        }
    }
}
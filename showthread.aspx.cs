using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using DataScrapingHelper;
using HtmlAgilityPack;


namespace SohhScrape
{
    public partial class showthread : System.Web.UI.Page
    {
        protected const string POST_HTML = @"<div class='post' id='post{0}'>
    <div class='{1}'>
        <div class='replyToPost'>
        {2}
        </div>
        <div class='avatar'>
        {3}
        </div>
        <p class='userName'>{4}</p>
        <p class='userTag'>{5}</p>
        <p class='postTimestamp'>{6}</p>
    </div>
    <div class='message'>
        {7}
    </div>
</div>
";
        protected const string SOHH_SHOWTHREAD_URL = "http://forums.projectcovo.com/showthread.php?";
        protected const string SOHH_HOSTNAME = "http://forums.projectcovo.com";
        protected const string REGEX_IMAGESOURCE = "src=\"[^(http://)][^\"\\r\\n]*\"|src='[^(http://)][^'\\r\\n]*'";
        protected const string REGEX_SOHHURL = "href=[\"']http://forums\\.projectcovo\\.com/(showthread)\\.php[^'\"\\r\\n]*[\"']";

        protected HtmlDocumentScraper Scraper { get; set; }
        protected String ForumTitle { get; set; }
        protected String ForumId { get; set; }
        protected String PageCountString { get; set; }
        protected int CurrentPage { get; set; }
        protected int MaxPages { get; set; }
        protected String ThreadTitle { get; set; }
        protected String ThreadId { get; set; }
        protected String RunningPostCount { get; set; }
        protected String LastPostId { get; set; }
        protected bool IsRequestingRefresh
        {
            get
            {
                return Request.Path.Contains("Update") ;
            }
        }
        protected String LastPostBeforeUpdate
        {
            get
            {
                return Request.QueryString["lastPost"];
            }
        }
        protected bool RefreshContainsData
        {
            get
            {
                if (IsRequestingRefresh && Convert.ToInt32(RunningPostCount) > Convert.ToInt32(LastPostBeforeUpdate))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        protected bool IsLoggedIn
        {
            get
            {
                return (Session["username"] != null && Session["username"].ToString() != String.Empty);
            }
        }
        
        protected String ScrapePosts()
        {
            StringBuilder posts = new StringBuilder();
            HtmlNodeCollection postNodes = Scraper.HtmlDocument.DocumentNode.SelectNodes("//table[starts-with(@id, 'post')]");


            String threadIdParameter = (ThreadId == null) ? String.Empty : "t=" + ThreadId.ToString();
            foreach (HtmlNode node in postNodes)
            {
                bool posterIsCurrentUser = (node.SelectSingleNode(".//img[@src='images/buttons/edit.gif']") != null);
                //Post ID
                String currentPostId = node.Id.Remove(0, 4);
                LastPostId = currentPostId;

                RunningPostCount = node.SelectSingleNode(".//strong").InnerText;

                if (IsRequestingRefresh && Convert.ToInt32(RunningPostCount) <= Convert.ToInt32(LastPostBeforeUpdate))
                {
                    continue;
                }

                //Image HTML
                String imageHtml = String.Empty;
                HtmlNode imageNode = node.SelectSingleNode(".//img[contains(@alt, 'Avatar')]");
                if (imageNode != null && imageNode.Attributes["src"] != null)
                {
                    String imageUrl = imageNode.Attributes["src"].Value;
                    if (!imageUrl.StartsWith("http://") && !imageUrl.StartsWith("www."))
                    {
                        imageUrl = SOHH_HOSTNAME + "/" + imageUrl;
                    }
                    imageHtml = @"<img src='" + imageUrl + "' alt='avatar'/>";
                }

                StringBuilder message = new StringBuilder(node.SelectSingleNode(".//td[contains(@id, 'td_post_')]//div[contains(@id, 'post_message_')]").InnerHtml);
                message.Replace("<a href=\"showthread.php", "<a href=\"showthread.aspx");
                message.Replace("&lt;", "<");
                message.Replace("&gt;", ">");
                message.Replace("&quot;", "\"");
                message.Replace("&amp;", "&");
                

                String quoteUrl = String.Empty;
                if (IsLoggedIn)
                {
                    
                    if (posterIsCurrentUser)
                    {
                        quoteUrl = String.Format("<a href='post.aspx?do=editpost&{0}&p={1}&page={2}' class='editPencil'></a>", threadIdParameter, currentPostId, CurrentPage);
                        //quoteUrl = String.Format("<a href='post.aspx?do=newreply&t={0}&p={1}&page={2}' class='replyQuotes'></a>", threadIdParameter, currentPostId, CurrentPage);
                    }
                    else
                    {
                        quoteUrl = String.Format("<a href='post.aspx?do=newreply&t={0}&p={1}&page={2}' class='replyQuotes'></a>", threadIdParameter, currentPostId, CurrentPage);
                    }
                }
                else
                {
                    quoteUrl = String.Format("<a href='http://forums.projectcovo.com/newreply.php?do=newreply&t={0}&p={1}&page={2}' class='replyQuotes' target='reply'></a>", threadIdParameter, currentPostId, CurrentPage);
                }

                String messageString = message.ToString();
                messageString = Regex.Replace(messageString, REGEX_IMAGESOURCE, PrependPathToUrl);
                messageString = Regex.Replace(messageString, REGEX_SOHHURL, ReplaceSohhUrl);

                posts.Append(String.Format(POST_HTML,
                    currentPostId,
                    posterIsCurrentUser ? "author authorHighlight" : "author",
                    quoteUrl,
                    imageHtml,
                    node.SelectSingleNode(".//a[@class='bigusername']").InnerText,
                    node.SelectSingleNode(".//div[@class='smallfont']").InnerText,
                    "Post #" + RunningPostCount + " - " + node.SelectSingleNode(".//td[@class='thead']/div[position()=2]").InnerText,
                    messageString));
            }

            return posts.ToString();
        }

        protected void ScrapeGeneralData()
        {
            //Get Unique Thread Id
            if (Request.QueryString["t"] != null && Request.QueryString["t"] != String.Empty)
            {
                ThreadId = Request.QueryString["t"];
            }
            else
            {
                HtmlNode threadIdContainer = Scraper.HtmlDocument.DocumentNode.SelectSingleNode("//input[@name='searchthreadid']");
                if (threadIdContainer == null || threadIdContainer.Attributes["value"] == null)
                {
                    threadIdContainer = Scraper.HtmlDocument.DocumentNode.SelectSingleNode("//input[@id='qr_threadid']");
                }

                if (threadIdContainer != null && threadIdContainer.Attributes["value"] != null)
                {
                    ThreadId = threadIdContainer.Attributes["value"].Value;
                }

                if (ThreadId == null)
                {
                    String nextThreadUrl = Scraper.HtmlDocument.DocumentNode.SelectSingleNode("//a[. = 'Next Thread']/@href").GetAttributeValue("href", String.Empty);
                    if (nextThreadUrl != null)
                    {
                        int start = nextThreadUrl.IndexOf("t=");
                        int end = nextThreadUrl.IndexOf("&", start);
                        if (end < 0)
                        {
                            end = nextThreadUrl.Length - 1;
                        }
                        ThreadId = nextThreadUrl.Substring(start, end - start - 1);
                    }
                }  
            }

            //Get thread title
            ThreadTitle = Scraper.HtmlDocument.DocumentNode.SelectSingleNode("//td[@class='navbar']/strong").InnerText.Replace(
                    "<!-- BEGIN TEMPLATE: navbar_link -->\n\r\n", String.Empty).Replace("\r\n<!-- END TEMPLATE: navbar_link -->", String.Empty);

            //Get forum info
            HtmlNode forumNode = Scraper.HtmlDocument.DocumentNode.SelectSingleNode(".//span[@class='navbar' and position()=last()]/a");
            String forumLink = forumNode.Attributes["href"].Value;
            int forumIdStartIndex = forumLink.IndexOf("f=") + 2;
            int forumIdEndIndex = forumLink.IndexOf("&", forumIdStartIndex);
            forumIdEndIndex = (forumIdEndIndex == -1) ? forumLink.Length - 1 : forumIdEndIndex;

            ForumTitle = forumNode.InnerText;
            ForumId = forumLink.Substring(forumIdStartIndex, (forumIdEndIndex - forumIdStartIndex + 1));

            //Get Page Info
            HtmlNode pageCountNode = Scraper.HtmlDocument.DocumentNode.SelectSingleNode("//div[@class='pagenav']//td[contains(., 'Page')]");
            if (pageCountNode != null)
            {
                PageCountString = pageCountNode.InnerText;
                int indexOfDelimiter = PageCountString.IndexOf(" of ");
                CurrentPage = Convert.ToInt32(PageCountString.Substring(5, indexOfDelimiter - 5));
                MaxPages = Convert.ToInt32(PageCountString.Substring(indexOfDelimiter + 4));
            }
            else
            {
                PageCountString = "Page 1 of 1";
                CurrentPage = 1;
                MaxPages = 1;
            }

            if (!IsRequestingRefresh)
            {
                Page.Title = ThreadTitle;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Load SOHH page and scrape
            String webPageUrl = SOHH_SHOWTHREAD_URL + Request.QueryString;
            Scraper = new HtmlDocumentScraper(webPageUrl, String.Empty);
            CookieContainer cookies = Session["cookies"] as CookieContainer;
            Scraper.LoadWebPage(ref cookies);
            Session["cookies"] = cookies;
            ScrapeGeneralData();
            PostContent.Text = ScrapePosts();
        }

        protected String ReplaceSohhUrl(Match m)
        {
            String toReturn = m.Value;
            toReturn = toReturn.Replace("http://forums.projectcovo.com", "");
            toReturn = toReturn.Replace("php", "aspx");

            return toReturn;
        }
        protected String PrependPathToUrl(Match m)
        {
            int singleQuoteIndex = m.Value.IndexOf("'");
            int doubleQuoteIndex = m.Value.IndexOf("\"");
            int quoteIndex;

            if (singleQuoteIndex == -1 || doubleQuoteIndex == -1)
            {
                quoteIndex = Math.Max(singleQuoteIndex, doubleQuoteIndex);
            }
            else
            {
                quoteIndex = Math.Min(singleQuoteIndex, doubleQuoteIndex);
            }

            return m.Value.Insert(quoteIndex + 1, SOHH_HOSTNAME + "/");
        }
    }
}
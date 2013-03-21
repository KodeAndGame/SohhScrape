using System;
using System.Configuration;
using System.Net;
using System.Text.RegularExpressions;
using DataScrapingHelper;

namespace SohhScrape
{
    public partial class MobileSohhBase : System.Web.UI.Page
    {
        protected const string SOHH_HOSTNAME = "http://forums.projectcovo.com";

        protected HtmlDocumentScraper Scraper { get; set; }

        protected virtual String SohhPagePath
        {
            get
            {
                return Request.Path.Replace("aspx", "php");
            }
        }

        protected virtual bool ReadyToScrape()
        {
            return true;
        }

        protected virtual void ConcreteScrapeLogic()
        {
            return;
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (ReadyToScrape())
            {
                String webPageUrl = SOHH_HOSTNAME + SohhPagePath + "?" + Request.QueryString;
                String xsltPath = Server.MapPath(ConfigurationManager.AppSettings[Request.Path.Replace('/', '_').Replace('.', '_')]);

                Scraper = new HtmlDocumentScraper(webPageUrl, xsltPath);

                CookieContainer cookies = Session["cookies"] as CookieContainer;
                Scraper.LoadWebPage(ref cookies);
                Session["cookies"] = cookies;

                String output = Scraper.TransformHtmlDocument();
                
                //Replace Links
                output = output.Replace(".php", ".aspx");

                //Replace images
                //output = Regex.Replace(output, "src=\"[^\"\\r\\n]*\"|src='[^'\\r\\n]*'", PrependPathToUrl);

                Master.XslTransformedContent = output;
                
                ConcreteScrapeLogic();
            }
        }

        private String PrependPathToUrl(Match m)
        {
            int quotationIndex = Math.Min(m.Value.IndexOf("\""), m.Value.IndexOf("'"));

            return m.Value.Insert(quotationIndex + 1, SOHH_HOSTNAME + "/");
        }
    }
}
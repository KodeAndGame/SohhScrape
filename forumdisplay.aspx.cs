using System;
using System.Web.UI;
using DataScrapingHelper;

namespace SohhScrape
{
    public partial class forumdisplay : MobileSohhBase
    {
        protected String ForumTitle {get; set;}
        protected int ForumId { get; set; }
        protected String PageCountString { get; set; }
        protected int CurrentPage { get; set; }
        protected int MaxPages { get; set; }
        protected override bool ReadyToScrape()
        {
            return (Request.QueryString != null && Request.QueryString["f"] != null);
        }

        protected override void ConcreteScrapeLogic()
        {
            ForumTitle = Scraper.HtmlDocument.DocumentNode.SelectSingleNode("//td[@class='navbar']/strong").InnerText.Replace(
                    "<!-- BEGIN TEMPLATE: navbar_link -->\n\r\n", String.Empty).Replace("\r\n<!-- END TEMPLATE: navbar_link -->", String.Empty);
            ForumId = Convert.ToInt32(Request.QueryString["f"]);
            PageCountString = Scraper.HtmlDocument.DocumentNode.SelectSingleNode("//td[@class='vbmenu_control' and contains(., 'Page')]").InnerText;
            int indexOfDelimiter = PageCountString.IndexOf(" of ");
            CurrentPage = Convert.ToInt32(PageCountString.Substring(5, indexOfDelimiter - 5));
            MaxPages = Convert.ToInt32(PageCountString.Substring(indexOfDelimiter + 4));

            Page.Title = ForumTitle;
        }
    }
}
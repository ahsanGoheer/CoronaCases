using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
namespace HtmlHandlerLib
{
    public class HtmlScraper
    {
        private string websiteUrl;
        private string websiteData;
        public HtmlScraper(string url)
        {
            this.websiteUrl = url;
        }

        public string WebsiteUrl 
        {
            get
            {
                return this.websiteUrl;
            } 
            set 
            {
                this.websiteUrl = value;    
            } 
        }
        public string WebsiteData { get { return websiteData; } }
        public async Task getHtml()
        {
            HttpClient htmlFetcher = new HttpClient();
            this.websiteData = await htmlFetcher.GetStringAsync(this.websiteUrl);   
        }

        public HtmlDocument HtmlDocLoader()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(this.websiteData);
            return htmlDoc;
        }

    }
}

using Collector.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Utilities
{
    public class Net
    {
        public String Criteria = "";
        public ScrapeType function;
        public TwitterCreiteriaQuestion Query;
        string protocol = "https://";
        private String URL = "twitter.com/i/search/timeline?f={0}&q={1}&src=typd&max_position={2}";
        private String ModifiedURL;

        public String BuildSearchURL(ScrapeType function = ScrapeType.tweets, string MaxPosition = "", bool ForceCriteriaRefresh = false, String URL = "", bool ReplaceURL = false)
        {
            string tempURL = this.URL;

            if (!string.IsNullOrEmpty(URL))
            {
                tempURL = URL;

                if (ReplaceURL)
                    this.URL = URL;
            }
            this.function = function;

            if (ForceCriteriaRefresh || string.IsNullOrEmpty(this.Criteria))
                this.Criteria = Query.ToString();

            ModifiedURL = string.Format(tempURL, new string[] { function.ToString(), this.Criteria, MaxPosition });

            Uri myUri = new Uri(ModifiedURL, UriKind.RelativeOrAbsolute);

            ModifiedURL = protocol + myUri.ToString().Replace("http://","").Replace("https://", "").Replace(" ", "%20").Replace("#", "%23").Replace(":", "%3A");

            return ModifiedURL;
        }


        public string DownloadJson()
        {
            var c = new WebClient();
            c.Headers.Set("Host", "twitter.com");
            c.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64)");
            c.Headers.Set("Accept", "application/json, text/javascript, */*; q=0.01");
            c.Headers.Set("Accept-Language", "en-US;q=0.7,en;q=0.3");
            c.Headers.Set("X-Requested-With", "XMLHttpRequest");
            c.Headers.Set("content-type", "application/json;charset=utf-8");
            c.Headers.Set("Referer", ModifiedURL);

            try
            {
                string json = c.DownloadString(ModifiedURL);

                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DownloadJson(String URL)
        {
            ModifiedURL = URL;
            return DownloadJson();
        }

    }
}

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
        public TwitterCreiteriaQuestion Query;
        private String URL = "https://twitter.com/i/search/timeline?f=tweets&q={0}&src=typd&max_position={1}";
        private String ModifiedURL;

        public String BuildSearchURL(string MaxPosition = "", bool ForceCriteriaRefresh = false, String URL = "", bool ReplaceURL = false)
        {
            string tempURL = this.URL;

            if (!string.IsNullOrEmpty(URL))
            {
                tempURL = URL;

                if (ReplaceURL)
                    this.URL = URL;
            }

            if (ForceCriteriaRefresh || string.IsNullOrEmpty(this.Criteria))
                this.Criteria = Query.ToString();

            ModifiedURL = string.Format(tempURL, new string[] { this.Criteria, MaxPosition });

            Uri myUri = new Uri(ModifiedURL, UriKind.RelativeOrAbsolute);

            ModifiedURL = myUri.ToString().Replace(" ", "%20");

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
            c.Headers.Set("content-type","application/json;charset=utf-8");
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

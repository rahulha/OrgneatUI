using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Generics
{

    public class TwitterResponseJson
    {
        public String min_position = "";
        public bool has_more_items = false;

        private String ItemsHtml;

        private HtmlNodeCollection Tweet_Html_collection;

        public string items_html
        {
            get => ItemsHtml;

            set
            {
                try
                {

                    if (!string.IsNullOrEmpty(value))
                    {
                        try
                        {
                            using (var htmlParser = new Collector.Utilities.HTMLParser())
                            {
                                htmlParser.HTMLString = value.Trim().Replace("  ", " ").Replace("\n", "");
                                ItemsHtml = value;

                                HtmlNodeCollection tweets = htmlParser.getNodes(@"//li[@class='js-stream-item stream-item stream-item']");

                                this.TweetHtmlcollection = tweets;
                            }

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        throw new Exception("No HTML in JSON.");
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public HtmlNodeCollection TweetHtmlcollection
        {
            get => Tweet_Html_collection;

            set
            {
                Tweet_Html_collection = value;
            }
        }
    }

}

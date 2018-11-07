using Collector.Generics;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Collector.Utilities;

namespace Collector
{

    public class Scraper
    {
        public event TweetProcessedEventHandler TweetsProcessed;

        public TwitterCreiteriaQuestion q;
        private Int64 TweetCount = 0;

        //private String Criteria = "";

        private Net TwitterNetworkUtil = new Net();

        public TwitterCreiteriaQuestion setCriteria(DateTime since, DateTime until,
            string AllWordAndPhrase = "", string SkipWords = "", string AnyWordsAndPhrase = "",
            string AllHashtags = "", string AnyHashtags = "", string AllFrom = "", string anyfrom = "", string allto = "",
            string anyto = "", string allmention = "", string anymention = "", string near = "", string within = "", string lang = "all", Boolean toptweets = true, int maxtweets = 0)
        {
            TwitterCreiteriaQuestion q = new TwitterCreiteriaQuestion();

            if (!string.IsNullOrEmpty(AllWordAndPhrase))
                q.AllWordsAndPhrase = AllWordAndPhrase.Split(',');

            if (!string.IsNullOrEmpty(SkipWords))
                q.SkipWordsAndPhrase = SkipWords.Split(',');

            if (!string.IsNullOrEmpty(AnyWordsAndPhrase))
                q.AnyWordsAndPhrase = AnyWordsAndPhrase.Split(',');

            if (!string.IsNullOrEmpty(AllHashtags))
                q.AllHashtags = AllHashtags.Split(',');

            if (!string.IsNullOrEmpty(AnyHashtags))
                q.AnyHashtags = AnyHashtags.Split(',');

            if (!string.IsNullOrEmpty(AllFrom))
                q.AllFrom = AllFrom.Split(',');

            if (!string.IsNullOrEmpty(anyfrom))
                q.AnyFrom = anyfrom.Split(',');

            if (!string.IsNullOrEmpty(allto))
                q.AllTo = allto.Split(',');

            if (!string.IsNullOrEmpty(anyto))
                q.AnyTo = anyto.Split(',');

            if (!string.IsNullOrEmpty(allmention))
                q.AllMention = allmention.Split(',');

            if (!string.IsNullOrEmpty(anymention))
                q.AnyMention = anymention.Split(',');


            q.since = since;
            q.until = until;
            q.toptweets = toptweets;
            q.maxtweets = maxtweets;

            if (!string.IsNullOrEmpty(near))
                q.near = near;

            if (!string.IsNullOrEmpty(within))
                q.within = within;

            if (!string.IsNullOrEmpty(lang))
                q.lang = lang;

            this.q = q;

            return q;
        }




        private TwitterResponseJson ProcessJson(string json)
        {
            if (json != null && !string.IsNullOrEmpty(json))
            {
                try
                {
                    TwitterResponseJson jData = Newtonsoft.Json.JsonConvert.DeserializeObject<TwitterResponseJson>(json);

                    return jData;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("No JSON downloaded.");

            }
        }

        private List<Tweet> ProcessTweetCollection(HtmlNodeCollection nodes)
        {
            List<Tweet> Tweets = new List<Tweet>();

            if (nodes != null)
                foreach (HtmlNode node in nodes)
                {
                    Tweet tweet = new Tweet();

                    HtmlNode div = node.SelectSingleNode(".//div[contains(@class,'tweet')]");

                    tweet.ID = getAttributeValue(div, "data-tweet-id");
                    tweet.itemID = getAttributeValue(div, "data-item-id");
                    tweet.Permalink = getAttributeValue(div, "data-permalink-path");
                    tweet.ConversationID = getAttributeValue(div, "data-conversation-id");
                    tweet.Nonce = getAttributeValue(div, "data-tweet-nonce");
                    tweet.StatInitialized = Convert.ToBoolean(getAttributeValue(div, "data-tweet-stat-initialized"));

                    //User data
                    tweet.Author.ScreenName = getAttributeValue(div, "data-screen-name");
                    tweet.Author.Name = getAttributeValue(div, "data-name");
                    tweet.Author.ID = getAttributeValue(div, "data-user-id");
                    tweet.isVerified = getNodeInnerText(div, ".//span[@class='UserBadges']//span[@class='u-hiddenVisually']").Contains(" Retweeted ");

                    //Reply to user
                    string replytoJson = getAttributeValue(div, "data-reply-to-users-json").Replace("&quot;", @"""");
                    List<User> jData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(replytoJson);
                    foreach (User u in jData)
                        if (u.ID != tweet.Author.ID)
                            tweet.ReplyToUser = u;

                    tweet.DisclosureType = getAttributeValue(div, "data-disclosure-type");

                    String temp = getAttributeValue(div, "data-has-cards");
                    tweet.HasCards = Convert.ToBoolean(string.IsNullOrEmpty(temp) ? "False" : temp);
                    tweet.ComponentContext = getAttributeValue(div, "data-component-context");

                    temp = getAttributeValue(div, "data-is-reply-to");
                    tweet.isReply = Convert.ToBoolean(string.IsNullOrEmpty(temp) ? "False" : temp);
                    tweet.isRetweet = getNodeInnerText(div, ".//p[@class='u-hiddenVisually']").Contains(" Retweeted ");

                    temp = getAttributeValue(div, "data-has-parent-tweet");
                    tweet.HasParentTweet = Convert.ToBoolean(string.IsNullOrEmpty(temp) ? "False" : temp);
                    tweet.Mentions = getAttributeValue(div, "data-mentions");

                    tweet.Date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(getAttributeValue(div, "data-time-ms", ".//span[contains(@class,'_timestamp')]")));

                    //Convert.ToDateTime( Convert.ToDecimal(getAttributeValue(div, "data-time-ms", "//span[contains(@class,'_timestamp')]")));

                    HtmlNode Tweet_Text = div.SelectSingleNode(".//p[contains(@class,'js-tweet-text')]");
                    tweet.Text = Tweet_Text.InnerText;
                    tweet.Language = Tweet_Text.Attributes["lang"].Value;

                    HtmlNode footer = div.SelectSingleNode(".//div[contains(@class,'stream-item-footer')]");
                    tweet.Replies = Convert.ToInt32(getAttributeValue(footer, "data-tweet-stat-count", ".//span[contains(@class,'ProfileTweet-action--reply')]/span"));
                    tweet.Retweets = Convert.ToInt32(getAttributeValue(footer, "data-tweet-stat-count", ".//span[contains(@class,'ProfileTweet-action--retweet')]/span"));
                    tweet.Favorites = Convert.ToInt32(getAttributeValue(footer, "data-tweet-stat-count", ".//span[contains(@class,'ProfileTweet-action--favorite')]/span"));


                    tweet.isPartOfConversation = !(tweet.ID == tweet.ConversationID);
                    tweet.isRootOFConversation = (tweet.ID == tweet.ConversationID);



                    //Quoted TWeet
                    HtmlNode QuotedTweet = div.SelectSingleNode(".//div[contains(@class,'QuoteTweet-innerContainer')]");
                    tweet.QuotedTweetID = getAttributeValue(QuotedTweet, "data-item-id");
                    tweet.QuotedTweetItemType = getAttributeValue(QuotedTweet, "data-item-type");
                    tweet.QuotedTweetUser = getAttributeValue(QuotedTweet, "data-screen-name");
                    tweet.QuotedTweetUserID = getAttributeValue(QuotedTweet, "data-user-id");
                    tweet.QuotedTweetConversationID = getAttributeValue(QuotedTweet, "data-conversation-id");

                    Tweets.Add(tweet);

                    this.TweetCount += 1;

                    tweet = null;
                    if (q.maxtweets <= this.TweetCount)
                        break;

                }


            return Tweets;
        }

        private string getAttributeValue(HtmlNode doc, String AttributeName, String xPath = "", int NodeIndex = 0)
        {
            if (doc != null)
            {
                if (!String.IsNullOrEmpty(xPath))
                {
                    if (NodeIndex == 0)
                    {
                        HtmlNode node = doc.SelectSingleNode(xPath);

                        if (node.Attributes.Contains(AttributeName))
                            return node.Attributes[AttributeName].Value;

                        else
                            return "";
                    }
                    else
                    {
                        HtmlNodeCollection nodes = doc.SelectNodes(xPath);

                        if (NodeIndex < nodes.Count)
                        {
                            if (nodes[NodeIndex].Attributes.Contains(AttributeName))
                                return nodes[NodeIndex].Attributes[AttributeName].Value;

                            else
                                return ""; //throw new Exception("Invalid Attribute to search");
                        }
                        else
                            return ""; //throw new Exception("Index out of range.");
                    }
                }
                else
                {
                    if (doc.Attributes.Contains(AttributeName))
                        return doc.Attributes[AttributeName].Value;

                    else
                        return ""; //throw new Exception("Invalid Attribute to search");

                }
            }
            else
                return "";
        }

        private string getNodeInnerText(HtmlNode doc, String xPath, int NodeIndex = 0)
        {
            if (doc != null)
            {
                if (String.IsNullOrEmpty(xPath))
                {

                    if (NodeIndex == 0)
                    {
                        HtmlNode node = doc.SelectSingleNode(xPath);

                        if (node != null)
                            return node.InnerText;

                        else
                            return "";
                    }
                    else
                    {
                        HtmlNodeCollection nodes = doc.SelectNodes(xPath);

                        if (NodeIndex < nodes.Count)
                            return nodes[NodeIndex].InnerText;

                        else
                            return ""; //throw new Exception("Index out of range.");
                    }
                }
                else
                    return doc.InnerText;

            }
            else
                return "";
        }

        private int WrongHasItems = 0;
        public void Process()
        {
            TweetCount = 0;
            TwitterNetworkUtil.Query = this.q;

            String SearchURL = TwitterNetworkUtil.BuildSearchURL();

            Boolean Run = true;

            while (Run)
            {
                string json = TwitterNetworkUtil.DownloadJson(SearchURL);

                TwitterResponseJson resp = ProcessJson(json);

                List<Tweet> tweets = ProcessTweetCollection(resp.TweetHtmlcollection);

                var e = new TweetProcessedEventArgs(null, false, null) { Result = tweets };

                TweetsProcessed.BeginInvoke(this, e, EndTweetProcessedEvent, null);

                string min_pos = resp.min_position;

                if (min_pos.StartsWith("cm+") && tweets.Count > 0)
                    min_pos = "TWEET-" + tweets[tweets.Count - 1].ID + "-" + tweets[0].ID;

                SearchURL = TwitterNetworkUtil.BuildSearchURL(min_pos);

                if (q.maxtweets <= this.TweetCount)
                    Run = false;
                else
                {
                    Run = resp.has_more_items;


                    if (!resp.has_more_items && tweets.Count > 0)
                    {
                        if (q.since.Date < tweets[tweets.Count - 1].Date.Date && WrongHasItems < 5)
                        {
                            Run = true;
                            WrongHasItems += 1;
                        }
                        else if (WrongHasItems >= 5)
                        {
                            q.until = q.until.AddDays(-1);
                            WrongHasItems = 0;
                            Run = (q.since < q.until);
                        }
                        else
                            Run = false;
                    }
                    else
                        WrongHasItems = 0;
                }


            }
        }


        private void EndTweetProcessedEvent(IAsyncResult iar)
        {
            var ar = (System.Runtime.Remoting.Messaging.AsyncResult)iar;
            var invokedMethod = (TweetProcessedEventHandler)ar.AsyncDelegate;

            try
            {
                invokedMethod.EndInvoke(iar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public delegate void TweetProcessedEventHandler(object sender, TweetProcessedEventArgs e);

    public class TweetProcessedEventArgs : AsyncCompletedEventArgs
    {
        public TweetProcessedEventArgs(Exception error, bool cancelled, object userState)
            : base(error, cancelled, userState)
        {

        }

        public List<Tweet> Result;

    }
}

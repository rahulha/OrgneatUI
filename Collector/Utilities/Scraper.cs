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

        #region "Public Members"

        /// <summary>
        /// Type of file to Store
        /// <list type="bullet">
        /// <item>Json=0</item>
        /// <item>CSV=1</item>
        /// </list>
        /// </summary>
        public FileType FileType { get; set; }


        /// <summary>
        /// Occurs when Scraper.TweetsProcessed() is called.
        /// Returns: TweetProcessedEventArgs
        /// Result includes Tweets result set, Scraper Indentifier number and Query being processed.
        /// </summary>
        public event TweetProcessedEventHandler TweetsProcessed;

        public event ScraperCompletedEventHandler ScraperCompleted;

        /// <summary>
        /// Scraper identifier number.
        /// </summary>
        public int MyNumber = 0;

        /// <summary>
        /// When set indicates this scraper can write to file, set in constructor.
        /// </summary>
        public bool CanWriteToFile
        {
            get => this.canWriteToFile;
            set
            {
                this.canWriteToFile = value;

                if (this.canWriteToFile)
                {
                    if (string.IsNullOrEmpty(this.dir) || string.IsNullOrEmpty(this.filename))
                        throw new ArgumentNullException("When CanWriteToFile is being set from property provide Directory path and File name first.");

                    if (this.fm == null)
                    {
                        this.fm = new FileManager(this.FileType);
                        this.fm.Directory = this.dir;
                        this.fm.FileName = this.filename;
                        this.fm.WriteHeader(this.Header);
                    }
                }
            }
        }

        /// <summary>
        /// This parameter increments by one for each processed tweet. Once reached to Max limit of Int64 then resets to 0 and TweetCountMaxReached increments by one.
        /// </summary>
        public long TweetCountSinceLastMaxReached { get => this.totalTweetCount; }

        /// <summary>
        /// This parameter increments by one everytime TweetCountSinceLastMaxReached reaches to Int64.MaxValue.
        /// </summary>
        public long TweetCountMaxReached { get => this.multiple; }

        /// <summary>
        /// Gets total number of tweets processed so far since scraper started running.
        /// </summary>
        public UInt64 TotalTweetSinceStart
        {
            get
            {
                return Convert.ToUInt64((this.multiple * Int64.MaxValue) + this.totalTweetCount);
            }
        }

        /// <summary>
        /// Directory to store CSV file.
        /// </summary>
        public string Directory
        {
            get => this.dir;
        }

        /// <summary>
        /// Name of CSV file to store tweets scraped.
        /// </summary>
        public string FileName { get => this.filename; }

        /// <summary>
        /// Gets the Date and Time when Processing started with this Scraper
        /// </summary>
        public DateTime StartDateTime { get => this.startDT; }

        /// <summary>
        /// Twitter Query to be processed by this Scraper.
        /// </summary>
        public TwitterCreiteriaQuestion MyQuery
        {
            get => this.myQuery;
            set
            {
                this.myQuery = value;
                this.TotalDaysInQuery = this.myQuery.until.Subtract(this.myQuery.since).Days;
            }
        }

        public TimeSpan EstimatedCompletionTime { get => this.estimatedCompletionTime; }

        public ScrapeType Function
        {
            get => this.f;
            set => this.f = value;
        }


        /// <summary>
        /// Scraper Constructor.
        /// </summary>
        /// <param name="Query">Twitter query that defines what to search on Twitter in order to scrape tweets.</param>
        /// <param name="canWriteToFile">When set Scraper writes scraped tweets to given file.</param>
        /// <param name="DirectoryToStoreFiles">Directory path to store file. Required parameter when canWriteToFile is set</param>
        /// <param name="FileNameWithoutExtension">Name of file without Extension. Required parameter when canWriteToFile is set</param>
        /// <param name="myNumber"></param>
        public Scraper(TwitterCreiteriaQuestion Query, bool canWriteToFile = false, String DirectoryToStoreFiles = "", int myNumber = 0, FileType ft = FileType.CSV)//ScrapeType function, 
        {
            this.FileType = ft;

            this.myQuery = Query ?? throw new ArgumentNullException(nameof(Query));

            //this.f = function;

            this.canWriteToFile = canWriteToFile;
            if (this.canWriteToFile)
            {
                if (string.IsNullOrEmpty(DirectoryToStoreFiles))
                    throw new ArgumentNullException("Directory path and File name required when CanWriteToFile is set to True.");

                String FileNameWithoutExtension = string.Concat(new object[] { this.myQuery.since.Year, "-", this.myQuery.since.Month, "-", this.myQuery.since.Day, " to ",
                      this.myQuery.until.Year, "-", this.myQuery.until.Month, "-", this.myQuery.until.Day });

                this.dir = DirectoryToStoreFiles;
                this.filename = FileNameWithoutExtension;

                this.fm = new FileManager(this.FileType);
                this.fm.Directory = this.dir;
                this.fm.FileName = this.filename;

                if (this.FileType == FileType.CSV)
                    this.fm.WriteHeader(this.Header);
            }

            this.MyNumber = myNumber;

        }



        #endregion



        #region "Private Members"
        private String Header = "ID,ConvID,Context,Disclosure Type,Has Parent Tweet,Has Cards,Date,Language,Permalink,is part of conversation?,Author ID,Author Name," +
                        "is Author verified,Text,Replies,Retweets,Favorites,Mentions,is Reply?,is Retweet?,Reply to User ID,Reply To User Name,Quoted Tweet ID, Quoted Tweet Conv. ID,Quoted Tweet Type," +
                        "Quoted Tweets User ID,Quoted Tweet User";

        private TwitterCreiteriaQuestion myQuery;

        private TimeSpan estimatedCompletionTime;

        private bool canWriteToFile = false;

        private Int64 totalTweetCount = 0;

        private Int64 multiple = 0;

        private FileManager fm;

        private int WrongHasitems = 0;
        private int JsonDownloadIssue = 0;

        //private BackgroundWorker bw;

        private string dir;
        private string filename;

        private DateTime startDT;

        private bool Cancel = false;

        private double TotalDaysInQuery = 0;
        private DateTime LastTweet;
        private ScrapeType f = ScrapeType.tweets;
        #endregion



        #region "Public Methods"

        public TwitterCreiteriaQuestion setCriteria(DateTime since, DateTime until,
        string AllWordAndPhrase = "", string SkipWords = "", string AnyWordsAndPhrase = "",
        string AllHashtags = "", string AnyHashtags = "", string AllFrom = "", string anyfrom = "", string allto = "",
        string anyto = "", string allmention = "", string anymention = "", string near = "", string within = "", string lang = "all", Boolean toptweets = true,
        int maxtweets = 0)
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

            this.TotalDaysInQuery = q.until.Subtract(q.since).Days;

            q.toptweets = toptweets;
            q.maxtweets = maxtweets;

            if (!string.IsNullOrEmpty(near))
                q.near = near;

            if (!string.IsNullOrEmpty(within))
                q.within = within;

            if (!string.IsNullOrEmpty(lang))
                q.lang = lang;

            this.myQuery = q;

            return q;
        }

        /// <summary>
        /// Start scraper asynchornously.Use ScraperProgressChanged and ScraperCompleted events to monitor Scraper activity.
        /// </summary>
        public async Task StartAsync()
        {
            this.Cancel = false;
            this.multiple = 0;
            this.totalTweetCount = 0;

            this.LastTweet = this.myQuery.until;
            this.startDT = DateTime.Now;
            this.TotalDaysInQuery = this.myQuery.until.Subtract(this.myQuery.since).Days;

            await this.DoWork();

            //this.bw.RunWorkerAsync();
        }

        /// <summary>
        /// Stops this Scraper activity immidiately after last json is processed. ScraperCompleted event is raised after succesful suspension of scraper.
        /// Check isCanceled parameter in ScraperCompletedEventArgument of ScraperCompleted event.
        /// </summary>
        public void StopAsync()
        {
            this.Cancel = true;
        }

        #endregion



        #region "Private methods"

        //private void Bw_DoWork(object sender, DoWorkEventArgs e)
        private async Task DoWork()
        {
            Exception workerException = null;

            this.totalTweetCount = 0;

            Net TwitterNetworkUtil = new Net();
            TwitterNetworkUtil.Query = this.myQuery;
            TwitterNetworkUtil.function = this.f;
            List<Tweet> tweets = null;

            String SearchURL = TwitterNetworkUtil.BuildSearchURL();

            Boolean Run = true;

            await Task.Run(() =>
            {


                while (Run && !this.Cancel)
                {
                    try
                    {
                        //Download json from Twitter
                        string json = TwitterNetworkUtil.DownloadJson(SearchURL);

                        //Convert Json string to json object for processing
                        TwitterResponseJson resp = ProcessJson(json);


                        //Process Tweets
                        tweets = ProcessTweetCollection(resp.TweetHtmlcollection);


                        //This counter checks if there is any issue in json download.
                        JsonDownloadIssue += 1;

                        //Saving tweets, stat calculations
                        if (tweets.Count > 0)
                        {
                            //if there is no error in json download, reset counter.
                            JsonDownloadIssue = 0;

                            if (this.canWriteToFile)
                                if (this.FileType == FileType.CSV)
                                    this.fm.AppendTextToFileAsync(GetTweetString(tweets));
                                else if (this.FileType == FileType.Json)
                                    this.fm.AppendTextToFileAsync(json);


                            if ((this.TotalTweetSinceStart % 1000) < 20)
                            {
                                DateTime FirstTweet = tweets[tweets.Count - 1].Date;
                                double ProcessingTime = DateTime.Now.Subtract(this.startDT).TotalSeconds; //Total time in sec from start
                                double ProcessedTime = this.LastTweet.Subtract(FirstTweet).TotalSeconds; //Tweet time span in secs

                                long ETASecs = Convert.ToInt64(this.TotalDaysInQuery * 86400 * (ProcessingTime / ProcessedTime));

                                this.estimatedCompletionTime = new TimeSpan(ETASecs * 10000000);
                            }

                            string min_pos = resp.min_position;

                            if (min_pos.StartsWith("cm+"))
                                min_pos = "TWEET-" + tweets[tweets.Count - 1].ID + "-" + tweets[0].ID;

                            SearchURL = TwitterNetworkUtil.BuildSearchURL(MaxPosition: min_pos);
                        }

                        //Determine whether to continue running loop.

                        //If max tweets is set, check and stop loop
                        if (this.myQuery.maxtweets != 0 && this.myQuery.maxtweets <= this.totalTweetCount)
                            Run = false;

                        else if (!resp.has_more_items)
                        {
                            if (tweets.Count > 0)
                            {
                                if (this.myQuery.since.Date < tweets[tweets.Count - 1].Date.Date && this.WrongHasitems < 5) //Since date not reached. Twitter by mistake responded with no more items flag. Try again for next batch with min_position in URL
                                    this.WrongHasitems += 1;

                                else if (this.WrongHasitems >= 5) //Since date has not reached but retry 5 times failed. Now trying with until date less than last tweet received.
                                {
                                    this.myQuery.until = tweets[tweets.Count - 1].Date.Date;
                                    this.WrongHasitems = 0;
                                    Run = (this.myQuery.since < this.myQuery.until);
                                }
                                else
                                    Run = false; //Since date reached
                            }
                            else
                            {
                                //Tweets not received and has items flas is false. Since date not reached but is less than until dte so trying again with same query.
                                if (JsonDownloadIssue < 5 && this.myQuery.since.AddDays(1) < this.myQuery.until)
                                    workerException = new Exception("Error downloading Json. Attempt " + JsonDownloadIssue);

                                else if (JsonDownloadIssue >= 5)
                                {
                                    workerException = new Exception("There was an issue downloading Json. We tried downloading 5 times. Check the Twitter URL for to see if the Twitter response is null. URL: " + SearchURL);

                                    this.myQuery.until = this.myQuery.until.AddDays(-1);
                                    this.WrongHasitems = 0;
                                    this.JsonDownloadIssue = 0;
                                    Run = (this.myQuery.since < this.myQuery.until);
                                }
                                else
                                    Run = false; //Since date and until date are now same as result of decrementing until date for any reason.

                            }
                        }
                        else
                            this.WrongHasitems = 0;



                    }
                    catch (Exception ex)
                    {
                        workerException = ex;
                    }
                    finally
                    {
                        var TweetProcessedResult = new TweetProcessedEventArgs(workerException, false, null) { Result = tweets, AssociatedQuery = this.myQuery, Number = this.MyNumber };

                        TweetsProcessed.BeginInvoke(this, TweetProcessedResult, EndTweetProcessedEvent, null);

                    }

                }

                if (this.fm != null)
                    this.fm.CloseAsync();

                this.WrongHasitems = 0;

                var ScraperComplete = new ScraperCompletedEventArgs(null, false, null) { AssociatedQuery = this.myQuery, Number = this.MyNumber, isCanceled = this.Cancel };

                ScraperCompleted.BeginInvoke(this, ScraperComplete, EndScraperCompletedEvent, null);

            });

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

                    try
                    {
                        HtmlNode div = node.SelectSingleNode(".//div[contains(@class,'tweet')]");

                        tweet.ID = getAttributeValue(div, "data-tweet-id");
                        tweet.ItemID = getAttributeValue(div, "data-item-id");
                        tweet.Permalink = getAttributeValue(div, "data-permalink-path");
                        tweet.ConversationID = getAttributeValue(div, "data-conversation-id");
                        tweet.Nonce = getAttributeValue(div, "data-tweet-nonce");

                        //try
                        //{
                        //    tweet.StatInitialized = Convert.ToBoolean( getAttributeValue(div, "data-tweet-stat-initialized"));
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine(ex.Message);
                        //}


                        //User data
                        tweet.Author.ScreenName = getAttributeValue(div, "data-screen-name");
                        tweet.Author.UserName = getAttributeValue(div, "data-name");
                        tweet.Author.ID = getAttributeValue(div, "data-user-id");
                        tweet.IsVerified = getNodeInnerText(div, ".//span[@class='UserBadges']//span[@class='u-hiddenVisually']").Contains(" Retweeted ");

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
                        tweet.IsReply = Convert.ToBoolean(string.IsNullOrEmpty(temp) ? "False" : temp);
                        tweet.IsRetweet = getNodeInnerText(div, ".//p[@class='u-hiddenVisually']").Contains(" Retweeted ");

                        temp = getAttributeValue(div, "data-has-parent-tweet");
                        tweet.HasParentTweet = Convert.ToBoolean(string.IsNullOrEmpty(temp) ? "False" : temp);
                        tweet.Mentions = getAttributeValue(div, "data-mentions");

                        tweet.Date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(getAttributeValue(div, "data-time-ms", ".//span[contains(@class,'_timestamp')]")));

                        //Convert.ToDateTime( Convert.ToDecimal( getAttributeValue(div, "data-time-ms", "//span[contains(@class,'_timestamp')]")));

                        HtmlNode Tweet_Text = div.SelectSingleNode(".//p[contains(@class,'js-tweet-text')]");
                        tweet.Text = Tweet_Text.InnerText.Replace("# ", "#").Replace("@ ", "@").Replace(",", ";").Replace("\n", "").Replace(Environment.NewLine, "");
                        tweet.Language = Tweet_Text.Attributes["lang"].Value;

                        HtmlNode footer = div.SelectSingleNode(".//div[contains(@class,'stream-item-footer')]");
                        tweet.Replies = Convert.ToInt32(getAttributeValue(footer, "data-tweet-stat-count", ".//span[contains(@class,'ProfileTweet-action--reply')]/span"));
                        tweet.Retweets = Convert.ToInt32(getAttributeValue(footer, "data-tweet-stat-count", ".//span[contains(@class,'ProfileTweet-action--retweet')]/span"));
                        tweet.Favorites = Convert.ToInt32(getAttributeValue(footer, "data-tweet-stat-count", ".//span[contains(@class,'ProfileTweet-action--favorite')]/span"));


                        tweet.IsPartOfConversation = !(tweet.ID == tweet.ConversationID);
                        tweet.IsRootOFConversation = (tweet.ID == tweet.ConversationID);



                        //Quoted TWeet
                        HtmlNode QuotedTweet = div.SelectSingleNode(".//div[contains(@class,'QuoteTweet-innerContainer')]");
                        tweet.QuotedTweetID = getAttributeValue(QuotedTweet, "data-item-id");
                        tweet.QuotedTweetItemType = getAttributeValue(QuotedTweet, "data-item-type");
                        tweet.QuotedTweetUser = getAttributeValue(QuotedTweet, "data-screen-name");
                        tweet.QuotedTweetUserID = getAttributeValue(QuotedTweet, "data-user-id");
                        tweet.QuotedTweetConversationID = getAttributeValue(QuotedTweet, "data-conversation-id");

                        Tweets.Add(tweet);

                        this.totalTweetCount += 1;
                        if (this.totalTweetCount == Int64.MaxValue)
                        {
                            this.multiple += 1;
                            this.totalTweetCount = 0;
                        }

                        tweet = null;
                        if (this.myQuery.maxtweets != 0 && this.myQuery.maxtweets <= this.totalTweetCount)
                            break;
                    }
                    catch (Exception ex)
                    {
                    }
                }


            return Tweets;
        }

        private string getAttributeValue(HtmlNode doc, String AttributeName, String xPath = "", int NodeIndex = 0)
        {
            String ret = "";

            if (doc != null)
            {
                if (!String.IsNullOrEmpty(xPath))
                {
                    if (NodeIndex == 0)
                    {
                        HtmlNode node = doc.SelectSingleNode(xPath);

                        if (node.Attributes.Contains(AttributeName))
                            ret = node.Attributes[AttributeName].Value;
                    }
                    else
                    {
                        HtmlNodeCollection nodes = doc.SelectNodes(xPath);

                        if (NodeIndex < nodes.Count)
                            if (nodes[NodeIndex].Attributes.Contains(AttributeName))
                                ret = nodes[NodeIndex].Attributes[AttributeName].Value;
                    }
                }
                else
                    if (doc.Attributes.Contains(AttributeName))
                    ret = doc.Attributes[AttributeName].Value;
            }

            return ret;
        }

        private string getNodeInnerText(HtmlNode doc, String xPath, int NodeIndex = 0)
        {
            String ret = "";

            if (doc != null)
            {
                if (String.IsNullOrEmpty(xPath))
                {

                    if (NodeIndex == 0)
                    {
                        HtmlNode node = doc.SelectSingleNode(xPath);

                        if (node != null)
                            ret = node.InnerText;

                    }
                    else
                    {
                        HtmlNodeCollection nodes = doc.SelectNodes(xPath);

                        if (NodeIndex < nodes.Count)
                            ret = nodes[NodeIndex].InnerText;

                    }
                }
                else
                    ret = doc.InnerText;

            }

            return ret;
        }



        private string GetTweetString(List<Tweet> tweets)
        {
            string strTweets = "";

            foreach (Tweet t in tweets)
            {
                //URLs, 
                try
                {
                    strTweets += String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}, " +
                        "{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26}" + Environment.NewLine,
                        t.ID, t.ConversationID, t.ComponentContext, t.DisclosureType, t.HasParentTweet, t.HasCards,
                        t.Date, t.Language, t.Permalink, t.IsPartOfConversation,
                        t.Author.ID, t.Author.name, t.IsVerified,
                        t.Text, t.Replies, t.Retweets, t.Favorites, t.Mentions,
                        t.IsReply, t.IsRetweet, t.ReplyToUser.ID, t.ReplyToUser.ScreenName, t.QuotedTweetID, t.QuotedTweetConversationID, t.QuotedTweetItemType,
                        t.QuotedTweetUserID, t.QuotedTweetUser
                        );


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return strTweets;
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

        private void EndScraperCompletedEvent(IAsyncResult iar)
        {
            var ar = (System.Runtime.Remoting.Messaging.AsyncResult)iar;
            var invokedMethod = (ScraperCompletedEventHandler)ar.AsyncDelegate;

            try
            {
                invokedMethod.EndInvoke(iar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

    public delegate void TweetProcessedEventHandler(object sender, TweetProcessedEventArgs e);
    public delegate void ScraperCompletedEventHandler(object sender, ScraperCompletedEventArgs e);

    public class TweetProcessedEventArgs : AsyncCompletedEventArgs
    {
        public TweetProcessedEventArgs(Exception error, bool cancelled, object userState)
            : base(error, cancelled, userState)
        {

        }

        public List<Tweet> Result;
        public int Number;
        public TwitterCreiteriaQuestion AssociatedQuery;

    }


    public class ScraperCompletedEventArgs : AsyncCompletedEventArgs
    {
        public ScraperCompletedEventArgs(Exception error, bool cancelled, object userState)
            : base(error, cancelled, userState)
        {

        }

        public int Number;
        public TwitterCreiteriaQuestion AssociatedQuery;
        public bool isCanceled;
    }

    public enum FileType
    {
        Json = 0,
        CSV = 1
    }
}

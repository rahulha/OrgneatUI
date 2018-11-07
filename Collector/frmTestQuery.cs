using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Collector.Generics;

namespace Collector
{
    public partial class frmTestQuery : Form
    {
        public TwitterCreiteriaQuestion TwitterQuery;
        private int InternetSpeed = 100;

        private Utilities.Net TwitterNetworkUtil = new Utilities.Net();
        private Scraper scraper = new Scraper();

        private string SampleURL = "https://twitter.com/search?f=tweets&q={0}&src=typd";

        private System.Threading.Thread th;

        List<Tweet> sampleTweets = new List<Tweet>();

        public frmTestQuery(TwitterCreiteriaQuestion Query, int InternetSpeed)
        {
            InitializeComponent();

            Query.maxtweets = 1000;

            this.TwitterQuery = Query;
            this.InternetSpeed = InternetSpeed;
        }

        private void frmTestQuery_Load(object sender, EventArgs e)
        {
            scraper.TweetsProcessed += Scraper_TweetsProcessed;

            th = new System.Threading.Thread(StartSampleProcess);
            th.Start();
        }

        private void Scraper_TweetsProcessed(object sender, TweetProcessedEventArgs e)
        {
            sampleTweets.AddRange(e.Result);

            if (ProcessDone)
            {
                DateTime endT = DateTime.Now;

                long size = 0;
                using (Stream s = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(s, sampleTweets);
                    size = s.Length / 1024;
                }


                double sec = endT.Subtract(t).TotalSeconds;
                double speed = size / sec;

                DateTime FirstTweet = sampleTweets[sampleTweets.Count - 1].Date;
                DateTime LastTweet = sampleTweets[0].Date;

                double TweetsTimespan = LastTweet.Subtract(FirstTweet).TotalSeconds;
                double SingleDayTweetsEstimate = (86400 / TweetsTimespan) * sampleTweets.Count;
                double QueriedDays = this.TwitterQuery.until.Subtract(TwitterQuery.since).TotalDays;
                TimeSpan ETA = new TimeSpan(Convert.ToInt64(((SingleDayTweetsEstimate * QueriedDays) / sampleTweets.Count) * sec));
                int Threads = Convert.ToInt32((InternetSpeed * 1024) / speed);
                TimeSpan ETAThread = new TimeSpan(Convert.ToInt64(ETA.TotalSeconds / Threads));


                //Performance
                lblFileSize.Text = size.ToString() + " kb";
                lblTime.Text = sec.ToString().ToString() + " sec";
                lblSpeed.Text = speed.ToString() + " kbps";

                //Estimation
                lblEstimatedTweets.Text = (SingleDayTweetsEstimate * QueriedDays).ToString();
                lblETASingle.Text = ETA.Days.ToString() + "  days, " + ETA.Minutes.ToString() + " mins, " + ETA.Seconds.ToString() + " secs.";
                lblThread.Text = Threads.ToString();
                lblEstimatedThreads.Text = ETAThread.Days.ToString() + "  days, " + ETAThread.Minutes.ToString() + " mins, " + ETAThread.Seconds.ToString() + " secs.";
            }
        }

        DateTime t;
        bool ProcessDone = false;
        private void StartSampleProcess()
        {
            t = DateTime.Now;

            lblMessage.Text = "Starting...";

            linkLabel1.Text = TwitterNetworkUtil.BuildSearchURL(URL: SampleURL);

            //Scraping started
            scraper.q = this.TwitterQuery;

            scraper.Process();

            //Scraping done
            ProcessDone = true;
        }

    }
}

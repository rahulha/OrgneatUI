using Collector;
using Collector.Generics;
using Collector.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace TwitterUI
{
    public partial class frmTestQuery : Form
    {
        public TwitterCreiteriaQuestion TwitterQuery;
        private int InternetSpeed = 100;
        private Performance perf;

        private Net TwitterNetworkUtil;

        private string SampleURL = "https://twitter.com/search?f=tweets&q={0}&src=typd";

        List<Tweet> sampleTweets = new List<Tweet>();

        private Scraper scraper;



        public frmTestQuery(TwitterCreiteriaQuestion Query, int InternetSpeed)
        {
            InitializeComponent();

            Query.maxtweets = 1000;

            this.TwitterQuery = Query;
            this.InternetSpeed = InternetSpeed;
            perf = new Performance(Process.GetCurrentProcess());
        }

        private void frmTestQuery_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "Initiating...";
            timer2.Start();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();

            scraper = new Scraper(TwitterQuery);
            scraper.TweetsProcessed += Scraper_TweetsProcessed;
            scraper.ScraperCompleted += Scraper_ScraperCompleted;


            TwitterNetworkUtil = new Net();
            TwitterNetworkUtil.Query = TwitterQuery;

            lblMessage.Text = "Starting...";

            scraper.StartAsync();
            timer1.Start();

        }


        double LargetTimeDifference = 0;
        private void Scraper_TweetsProcessed(object sender, TweetProcessedEventArgs e)
        {
            sampleTweets.AddRange(e.Result);

            for (int i = 0; i < e.Result.Count - 2; i++)
            {
                double tDiff = e.Result[i].Date.Subtract(e.Result[i + 1].Date).TotalSeconds;

                if (LargetTimeDifference < tDiff)
                    LargetTimeDifference = tDiff;
            }
        }


        private void InvokeUI(Action a)
        {
            this.BeginInvoke(new MethodInvoker(a));
        }

        private void Scraper_ScraperCompleted(object sender, ScraperCompletedEventArgs e)
        {
            DateTime endT = DateTime.Now;
            timer1.Stop();

            InvokeUI(() =>
            {
                linkLabel1.Text = TwitterNetworkUtil.BuildSearchURL(URL: SampleURL);
            });


            if (sampleTweets.Count > 0)
            {
                double size = 0;
                using (Stream s = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(s, sampleTweets);
                    size = s.Length / 1024;
                }


                double sec = endT.Subtract(scraper.StartDateTime).TotalSeconds;
                double speed = size / sec;

                DateTime FirstTweet = sampleTweets[sampleTweets.Count - 1].Date;
                DateTime LastTweet = sampleTweets[0].Date;

                double TweetsTimespan = LastTweet.Subtract(FirstTweet).TotalSeconds;
                double SingleDayTweetsEstimate = (86400 / TweetsTimespan) * sampleTweets.Count;
                double QueriedDays = this.TwitterQuery.until.Subtract(TwitterQuery.since).TotalDays;
                TimeSpan ETA = new TimeSpan(Convert.ToInt64(((SingleDayTweetsEstimate * QueriedDays) / sampleTweets.Count) * sec) * 10000000);

                int Threads = Math.Min((90 / (int)cpu), Convert.ToInt32((InternetSpeed * 1024) / speed));
                TimeSpan ETAThread = new TimeSpan(Convert.ToInt64(ETA.TotalSeconds / Threads) * 10000000);

                InvokeUI(() =>
                {
                    lblPUtilization.Text = Math.Round(cpu).ToString() + "%";
               
                    //Performance
                    lblFileSize.Text = Math.Round(size, 2).ToString() + " kb";
                    label4.Text = sampleTweets.Count.ToString() + " downloaded in: ";
                    lblTime.Text = Math.Round(sec, 2).ToString().ToString() + " sec(s)";
                    lblSpeed.Text = Math.Round(speed, 2).ToString() + " kbps";
                    lblFTDT.Text = FirstTweet.ToString();
                    lblLTDT.Text = LastTweet.ToString();
                    lblLargestDiff.Text = LargetTimeDifference.ToString() + " sec(s)";

                    //Estimation
                    lblEstimatedTweets.Text = String.Format("{0:n0}", SingleDayTweetsEstimate * QueriedDays);
                    lblETASingle.Text = ETA.Days.ToString() + "  days, " + ETA.Minutes.ToString() + " mins, " + ETA.Seconds.ToString() + " secs.";
                    lblThread.Text = Threads.ToString();
                    lblEstimatedThreads.Text = ETAThread.Days.ToString() + "  days, " + ETAThread.Minutes.ToString() + " mins, " + ETAThread.Seconds.ToString() + " secs.";

                    lblMessage.Text = "All done.";
                });

            }
            else
                InvokeUI(() =>
                {
                    lblMessage.Text = "The query provided is not invalid. Please click on link and see if Twitter is providing any Tweets. If Twitter has no result, please go back and correct your query.";
                });
        }


        float cpu, ram;
        long secElapsed = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            secElapsed += 5000000;
            TimeSpan elapsed = new TimeSpan(secElapsed);

            lblMessage.Text = "Tweets downloaded: " + sampleTweets.Count.ToString() + Environment.NewLine + "Time elapsed: " + elapsed.TotalSeconds.ToString();

            float c = Performance.GetProcessorUsage();
            float r = Performance.GetMemoryUsage();

            cpu = c > cpu ? c : cpu;
            ram = r > ram ? r : ram;
        }
    }
}

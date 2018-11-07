using Collector;
using Collector.Generics;
using Collector.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
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

        //private System.Threading.Thread th;
        private BackgroundWorker bw;

        List<Tweet> sampleTweets = new List<Tweet>();

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

            System.Threading.Thread.Sleep(1000);

            TwitterNetworkUtil = new Net();
            TwitterNetworkUtil.Query = TwitterQuery;

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;

            bw.DoWork += Bw_DoWork;
            bw.ProgressChanged += Bw_ProgressChanged;

            lblMessage.Text = "Starting...";
            bw.RunWorkerAsync();
            timer1.Start();
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                lblMessage.Text = "Running";
                t = DateTime.Now;
            }

            if (e.ProgressPercentage == 50)
            {
                //ProcessDone = true;
                lblMessage.Text = "Done downloading and processing. Now estimating performance and calculating recommendations.";
            }

            if (e.ProgressPercentage == 100)
            {
                DateTime endT = DateTime.Now;
                timer1.Stop();

                linkLabel1.Text = TwitterNetworkUtil.BuildSearchURL(URL: SampleURL);

                if (sampleTweets.Count > 0)
                {
                    double size = 0;
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
                    TimeSpan ETA = new TimeSpan(Convert.ToInt64(((SingleDayTweetsEstimate * QueriedDays) / sampleTweets.Count) * sec) * 10000000);

                    int Threads = Math.Min((90 / (int)cpu), Convert.ToInt32((InternetSpeed * 1024) / speed));
                    TimeSpan ETAThread = new TimeSpan(Convert.ToInt64(ETA.TotalSeconds / Threads) * 10000000);

                    lblPUtilization.Text = Math.Round(cpu).ToString() + "%";

                    //Performance
                    lblFileSize.Text = Math.Round(size, 2).ToString() + " kb";
                    label4.Text = sampleTweets.Count.ToString() + " downloaded in: ";
                    lblTime.Text = Math.Round(sec, 2).ToString().ToString() + " sec";
                    lblSpeed.Text = Math.Round(speed, 2).ToString() + " kbps";
                    lblFTDT.Text = FirstTweet.ToString();
                    lblLTDT.Text = LastTweet.ToString();



                    //Estimation
                    //lblEstimatedTweets.Text = Math.Round((SingleDayTweetsEstimate * QueriedDays)).ToString();
                    lblEstimatedTweets.Text = String.Format("{0:n0}", SingleDayTweetsEstimate * QueriedDays);


                    lblETASingle.Text = ETA.Days.ToString() + "  days, " + ETA.Minutes.ToString() + " mins, " + ETA.Seconds.ToString() + " secs.";
                    lblThread.Text = Threads.ToString();
                    lblEstimatedThreads.Text = ETAThread.Days.ToString() + "  days, " + ETAThread.Minutes.ToString() + " mins, " + ETAThread.Seconds.ToString() + " secs.";

                    lblMessage.Text = "All done.";
                }
                else
                    lblMessage.Text = "The query provided is not invalid. Please click on link and see if Twitter is providing any Tweets. If Twitter has no result, please go back and correct your query.";

            }
        }

        private void Scraper_TweetsProcessed(object sender, TweetProcessedEventArgs e)
        {
            sampleTweets.AddRange(e.Result);
        }

        DateTime t;
        //bool ProcessDone = false;

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Scraper scraper = new Scraper();

            scraper.TweetsProcessed += Scraper_TweetsProcessed;

            scraper.q = this.TwitterQuery;

            bw.ReportProgress(1);

            //Scraping started
            scraper.Process();

            //Scraping done
            bw.ReportProgress(50);

            //if (ProcessDone)
            //{
            bw.ReportProgress(100);
            //}
        }

        float cpu, ram;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblMessage.Text += ".";
            float c = Performance.GetProcessorUsage();
            float r = Performance.GetMemoryUsage();

            cpu = c > cpu ? c : cpu;
            ram = r > ram ? r : ram;
        }
    }
}

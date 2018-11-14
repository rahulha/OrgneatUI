using Collector;
using Collector.Generics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TwitterUI
{
    public partial class ScraperWindow : Form
    {
        public int NumberOfThreads;
        public TwitterCreiteriaQuestion MainQuery;

        public String MainDirectory = "";

        List<Scraper> workers;

        public ScraperWindow(int NumberOfThreads, TwitterCreiteriaQuestion TwitterQuery, String SaveFolderParth)
        {
            InitializeComponent();

            this.NumberOfThreads = NumberOfThreads;
            this.MainQuery = TwitterQuery;
            this.MainDirectory = SaveFolderParth;

            workers = new List<Scraper>();
        }

        private void InvokeUI(Action a)
        {
            this.BeginInvoke(new MethodInvoker(a));
        }

        private void Log(String Text)
        {
            InvokeUI(() =>
            {
                var d = DateTime.Now;

                txtLog.Text += d.ToShortDateString() + " - " + d.ToShortTimeString() + " :: " + Text + Environment.NewLine;
            });
        }

        private void AddWorker(TwitterCreiteriaQuestion tempQuery, int i)
        {
            Scraper scraper = new Scraper(tempQuery, true, MainDirectory, i);

            scraper.TweetsProcessed += Scraper_TweetsProcessed;
            scraper.ScraperCompleted += Scraper_ScraperCompleted;
            workers.Add(scraper);

            scraper.StartAsync();

            InvokeUI(() =>
            {
                ListViewItem itm = new ListViewItem(i.ToString());
                itm.SubItems.Add("0");
                itm.SubItems.Add("Calculating");
                listView1.Items.Add(itm);
            });

            Log("Thread " + i.ToString() + " started");
        }

        private void UpdateListView(string TextID, int attribute, string NewText)
        {
            InvokeUI(() =>
            {
                listView1.FindItemWithText(TextID).SubItems[attribute].Text = NewText;
            });
        }

        private void ScraperWindow_Load(object sender, EventArgs e)
        {
            Log("Initializing...");
            timer2.Start();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            Log("Starting scraping process.");

            double numberofdays = MainQuery.until.Subtract(MainQuery.since).TotalDays;
            double ThreadDays = Math.Floor(numberofdays / NumberOfThreads) * -1;

            int i;
            TwitterCreiteriaQuestion tempQuery = (TwitterCreiteriaQuestion)MainQuery.Clone();

            tempQuery.until = MainQuery.until;
            tempQuery.since = tempQuery.until.AddDays(ThreadDays);

            for (i = 1; i < NumberOfThreads; i++)
            {
                AddWorker(tempQuery, i);
                tempQuery.until = tempQuery.since;
                tempQuery.since = tempQuery.until.AddDays(ThreadDays);
            }

            if (tempQuery.since > MainQuery.since)
                tempQuery.since = MainQuery.since;

            AddWorker(tempQuery, i);

        }

        private void Scraper_ScraperCompleted(object sender, ScraperCompletedEventArgs e)
        {
            InvokeUI(() =>
            {
                Log("Scraper number " + e.Number + " done");

            });

            if (e.isCanceled)
                UpdateListView(e.Number.ToString(), 2, "Canceled");

        }

        private void Scraper_TweetsProcessed(object sender, TweetProcessedEventArgs e)
        {
            //sampleTweets.AddRange(e.Result);

            Scraper s = (Scraper)sender;

            UpdateListView(e.Number.ToString(), 1, s.TotalTweetSinceStart.ToString());
            UpdateListView(e.Number.ToString(), 2, s.EstimatedCompletionTime.Days.ToString() + "  days, " + s.EstimatedCompletionTime.Minutes.ToString() + " mins, " + s.EstimatedCompletionTime.Seconds.ToString() + " secs.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Scraper w in workers)
            {
                w.StopAsync();
            }
        }
    }
}
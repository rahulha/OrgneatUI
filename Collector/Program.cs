using Collector.Generics;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Windows.Forms;

namespace Collector
{

    class Program
    {
        // static String ConStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=dbTwitterHelper;Data Source=.\MSSQLLocalDB";
        //String URL = "https://twitter.com/i/search/timeline?f=tweets&q={0}&src=typd&max_position={1}";

        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.Run(new TwitterUI());
        }

        //static SqlConnection con = new SqlConnection(ConStr);
        //private static DataTable GetApproved()
        //{
        //    SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM tblApplications WHERE isApproval=1", con);
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        if (con.State != ConnectionState.Open)
        //            con.Open();

        //        adp.Fill(dt);
        //    }
        //    catch { }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return dt;
        //}

        //private TwitterCreiteriaQuestion getCriteria(string allwords = "", string phrase = "", string skipwords = "", string anywords = "", string allhashtags = "", string anyhashtags = "", string allfrom = "", string anyfrom = "", string allto = "", string anyto = "", string allmention = "", string anymention = "", string near = "", string within = "", string since = "", string until = "", string lang = "all", Boolean toptweets = true, int maxtweets = 0)
        //{
        //    TwitterCreiteriaQuestion tweetCriteria = new TwitterCreiteriaQuestion();

        //    if (allwords != "")
        //        tweetCriteria.allwords = allwords;

        //    if (phrase != "")
        //        tweetCriteria.phrase = phrase;

        //    if (skipwords != "")
        //        tweetCriteria.skipwords = skipwords.Split(' ');

        //    if (anywords != "")
        //        tweetCriteria.anywords = anywords.Split(' ');

        //    if (allhashtags != "")
        //        tweetCriteria.allhashtags = allhashtags.Split(' ');

        //    if (anyhashtags != "")
        //        tweetCriteria.anyhashtags = anyhashtags.Split(' ');

        //    if (allfrom != "")
        //        tweetCriteria.allfrom = allfrom.Split(' ');

        //    if (anyfrom != "")
        //        tweetCriteria.anyfrom = anyfrom.Split(' ');

        //    if (allto != "")
        //        tweetCriteria.allto = allto.Split(' ');

        //    if (anyto != "")
        //        tweetCriteria.anyto = anyto.Split(' ');

        //    if (allmention != "")
        //        tweetCriteria.allmention = allmention.Split(' ');

        //    if (anymention != "")
        //        tweetCriteria.anymention = anymention.Split(' ');


        //    if (since != "")
        //        tweetCriteria.since = since;

        //    if (until != "")
        //        tweetCriteria.until = until;

        //    tweetCriteria.toptweets = toptweets;

        //    tweetCriteria.maxtweets = maxtweets;

        //    if (near != "")
        //        tweetCriteria.near = near;

        //    if (within != "")
        //        tweetCriteria.within = within;

        //    if (lang != "")
        //        tweetCriteria.lang = lang;

        //    return tweetCriteria;
        //}


        //private string DownloadJson(String URL)
        //{
        //    var c = new WebClient();
        //    c.Headers.Set("Host", "twitter.com");
        //    c.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64)");
        //    c.Headers.Set("Accept", "application/json, text/javascript, */*; q=0.01");
        //    c.Headers.Set("Accept-Language", "de,en-US;q=0.7,en;q=0.3");
        //    c.Headers.Set("X-Requested-With", "XMLHttpRequest");
        //    c.Headers.Set("Referer", URL);
        //    c.Headers.Set("Connection", "keep-alive");

        //    string json = c.DownloadString(URL);

        //    return json;
        //}
    }
}

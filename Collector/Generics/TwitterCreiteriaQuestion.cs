using System;

namespace Collector.Generics
{
    public class TwitterCreiteriaQuestion : ICloneable
    {
        public String near = "", within = "", lang = "all";
        public String[] AllWordsAndPhrase = { }, SkipWordsAndPhrase = { }, AnyWordsAndPhrase = { }, AllHashtags = { }, AnyHashtags = { }, AllFrom = { }, AnyFrom = { }, AllTo = { }, AnyTo = { }, AllMention = { }, AnyMention = { };
        public String[] SkipFromUsers = { }, SkipToUsers = { }, SkipMentions = { };
        public DateTime since, until;
        

        public Boolean toptweets = true;
        public int maxtweets = 0;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            String urlGetData = "";

            // include all words
            if (AllWordsAndPhrase.Length > 0)
                foreach (String w in this.AllWordsAndPhrase)
                    if (w.Contains(" "))
                        urlGetData += @" """ + w + @"""";
                    else
                        urlGetData += ' ' + w;

            // none of these words
            if (SkipWordsAndPhrase.Length > 0)
                foreach (String w in this.SkipWordsAndPhrase)
                    if (w.Contains(" "))
                        urlGetData += @" -""" + w + @"""";
                    else
                        urlGetData += " -" + w;

            // any of these words
            if (AnyWordsAndPhrase.Length > 0)
            {
                foreach (String w in this.AnyWordsAndPhrase)
                    if (w.Contains(" "))
                        urlGetData += @" """ + w + @""" OR";
                    else
                        urlGetData += ' ' + w + " OR";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 2);
            }

            // all of these Hashtags
            if (AllHashtags.Length > 0)
            {
                foreach (String w in this.AllHashtags)
                    if (w.Substring(0, 1) != "#")
                        urlGetData += " #" + w + " AND";
                    else
                        urlGetData += " " + w + " AND";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 3);
            }

            // any of these hashtags
            if (AnyHashtags.Length > 0)
            {
                foreach (String w in this.AnyHashtags)
                    if (w.Substring(0, 1) != "#")
                        urlGetData += " #" + w + " OR";
                    else
                        urlGetData += ' ' + w + " OR";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 2);
            }

            // from all these users
            if (AllFrom.Length > 0)
            {
                foreach (String w in this.AllFrom)
                    if (w.Substring(0, 1) != "@")
                        urlGetData += " FROM:" + w + " AND";
                    else
                        urlGetData += " FROM:" + w.Substring(1) + " AND";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 3);
            }

            // from any of these users
            if (AnyFrom.Length > 0)
            {
                foreach (String w in this.AnyFrom)
                    if (w.Substring(0, 1) != "@")
                        urlGetData += " FROM:" + w + " OR";
                    else
                        urlGetData += " FROM:" + w.Substring(1) + " OR";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 2);
            }

            // Skip from any of these users
            if (SkipFromUsers.Length > 0)
            {
                foreach (String w in this.SkipFromUsers)
                    if (w.Substring(0, 1) != "@")
                        urlGetData += " -FROM:" + w + " OR";
                    else
                        urlGetData += " -FROM:" + w.Substring(1) + " OR";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 2);
            }

            // to all these users    
            if (AllTo.Length > 0)
            {
                foreach (String w in this.AllTo)
                    if (w.Substring(0, 1) != "@")
                        urlGetData += " TO:" + w + " AND";
                    else
                        urlGetData += " TO:" + w.Substring(1) + " AND";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 3);
            }

            // to any of these users    
            if (AnyTo.Length > 0)
            {
                foreach (String w in this.AnyTo)
                    if (w.Substring(0, 1) != "@")
                        urlGetData += " TO:" + w + " OR";
                    else
                        urlGetData += " TO:" + w.Substring(1) + " OR";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 2);
            }

            // Skip To any of these users    
            if (SkipToUsers.Length > 0)
            {
                foreach (String w in this.SkipToUsers)
                    if (w.Substring(0, 1) != "@")
                        urlGetData += " -TO:" + w + " OR";
                    else
                        urlGetData += " -TO:" + w.Substring(1) + " OR";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 2);
            }

            // All of these mentions
            if (AllMention.Length > 0)
            {
                foreach (String w in this.AllMention)
                    if (w.Substring(0, 1) != "@")
                        urlGetData += " @" + w + " AND";
                    else
                        urlGetData += " " + w.Substring(1) + " AND";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 3);
            }

            // Any of these mentions
            if (AnyMention.Length > 0)
            {
                foreach (String w in this.AnyMention)
                    if (w.Substring(0, 1) != "@")
                        urlGetData += " @" + w + " OR";
                    else
                        urlGetData += " " + w.Substring(1) + " OR";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 2);
            }

            // Skip these mentions
            if (SkipMentions.Length > 0)
            {
                foreach (String w in this.AnyMention)
                    if (w.Substring(0, 1) != "@")
                        urlGetData += " @" + w + " OR";
                    else
                        urlGetData += " " + w.Substring(1) + " OR";

                urlGetData = urlGetData.Substring(0, urlGetData.Length - 2);
            }

            if (!String.IsNullOrEmpty(near))
                urlGetData += @" near:""" + this.near + '"';

            if (!String.IsNullOrEmpty(within))
                if (this.within != "0")
                    urlGetData += " within:" + this.within;

            urlGetData += " since:" + since.Year.ToString() + "-" + since.Month.ToString() + "-" + since.Day.ToString();

            urlGetData += " until:" + until.Year.ToString() + "-" + until.Month.ToString() + "-" + until.Day.ToString();

            if (!String.IsNullOrEmpty(lang))
                urlGetData += "&lang=" + this.lang;

            return urlGetData.Trim();
        }
    }

    public enum ScrapeType
    {
        top,
        tweets,
        users,
        images,
        videos,
        news,
        broadcasts
    }
}

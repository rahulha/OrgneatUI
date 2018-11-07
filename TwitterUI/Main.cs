using Collector.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitterUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public TwitterCreiteriaQuestion Query;

      
        private void TwitterUI_Load(object sender, EventArgs e)
        {
            Until.MaxDate = DateTime.Now;
            Since.MaxDate = DateTime.Now;
            Until.MinDate = new DateTime(2006, 06, 01);
            Since.MinDate = new DateTime(2006, 06, 01);
        }

        private void AddItem(ref TextBox tb, ref ListBox lb)
        {
            string cText = "";

            cText = (tb.Text.Contains(" ") ? @""" + tb.Text + @""" : tb.Text);

            lb.Items.Add(cText);
            tb.Clear();
        }

        private void RemoveItem(ref ListBox lb)
        {
            lb.Items.Remove(lb.SelectedItems);
        }

        private String GetLanguage(String Language)
        {
            if (Language.Contains("All languages"))
                return "all";

            if (Language.Contains("English"))
                return "en";

            if (Language.Contains("Japanese"))
                return "ja";

            if (Language.Contains("Arabic"))
                return "ar";

            if (Language.Contains("Spanish"))
                return "es";

            if (Language.Contains("Amharic"))
                return "am";

            if (Language.Contains("Armenian"))
                return "hy";

            if (Language.Contains("Bangla"))
                return "bn";

            if (Language.Contains("Bulgarian"))
                return "bg";

            if (Language.Contains("Burmese"))
                return "my";

            if (Language.Contains("Central Kurdish"))
                return "ckb";

            if (Language.Contains("Chinese"))
                return "zh";

            if (Language.Contains("Danish"))
                return "da";

            if (Language.Contains("Divehi"))
                return "dv";

            if (Language.Contains("Dutch"))
                return "nl";

            if (Language.Contains("Estonian"))
                return "et";

            if (Language.Contains("Finnish"))
                return "fi";

            if (Language.Contains("French"))
                return "fr";

            if (Language.Contains("Georgian"))
                return "ka";

            if (Language.Contains("German"))
                return "de";

            if (Language.Contains("Greek"))
                return "el";

            if (Language.Contains("Gujarati"))
                return "gu";

            if (Language.Contains("Haitian Creole"))
                return "ht";

            if (Language.Contains("Hebrew"))
                return "he";

            if (Language.Contains("Hindi"))
                return "hi";

            if (Language.Contains("Hungarian"))
                return "hu";

            if (Language.Contains("Icelandic"))
                return "is";

            if (Language.Contains("Indonesian"))
                return "id";

            if (Language.Contains("Italian"))
                return "it";

            if (Language.Contains("Kannada"))
                return "kn";

            if (Language.Contains("Khmer"))
                return "km";

            if (Language.Contains("Korean"))
                return "ko";

            if (Language.Contains("Lao"))
                return "lo";

            if (Language.Contains("Latvian"))
                return "lv";

            if (Language.Contains("Lithuanian"))
                return "lt";

            if (Language.Contains("Malayalam"))
                return "ml";

            if (Language.Contains("Marathi"))
                return "mr";

            if (Language.Contains("Nepali"))
                return "ne";

            if (Language.Contains("Norwegian"))
                return "no";

            if (Language.Contains("Odia"))
                return "or";

            if (Language.Contains("Pashto"))
                return "ps";

            if (Language.Contains("Persian"))
                return "fa";

            if (Language.Contains("Polish"))
                return "pl";

            if (Language.Contains("Portuguese"))
                return "pt";

            if (Language.Contains("Punjabi"))
                return "pa";

            if (Language.Contains("Romanian"))
                return "ro";

            if (Language.Contains("Russian"))
                return "ru";

            if (Language.Contains("Serbian"))
                return "sr";

            if (Language.Contains("Sindhi"))
                return "sd";

            if (Language.Contains("Sinhala"))
                return "si";

            if (Language.Contains("Slovenian"))
                return "sl";

            if (Language.Contains("Swedish"))
                return "sv";

            if (Language.Contains("Tagalog"))
                return "tl";

            if (Language.Contains("Tamil"))
                return "ta";

            if (Language.Contains("Telugu"))
                return "te";

            if (Language.Contains("Thai"))
                return "th";

            if (Language.Contains("Tibetan"))
                return "bo";

            if (Language.Contains("Turkish"))
                return "tr";

            if (Language.Contains("Urdu"))
                return "ur";

            if (Language.Contains("Uyghur"))
                return "ug";

            if (Language.Contains("Vietnamese"))
                return "vi";


            return "en";

        }

        private void PrepareQuery()
        {
            TwitterCreiteriaQuestion q = new TwitterCreiteriaQuestion();

            q.AllFrom = ConcatItems(lstFromAllUsers);
            q.AllHashtags = ConcatItems(lstAllHashtags);
            q.AllMention = ConcatItems(lstAnyHashtags);
            q.AllTo = ConcatItems(lstToAllUSers);
            q.AllWordsAndPhrase = ConcatItems(AllWordsAndPhrase);
            q.AnyFrom = ConcatItems(lstFromAnyUsers);
            q.AnyHashtags = ConcatItems(lstAnyHashtags);
            q.AnyMention = ConcatItems(lstAnyMentions);
            q.AnyTo = ConcatItems(lstToAnyUsers);
            q.AnyWordsAndPhrase = ConcatItems(AnyWordsAndPhrase);
            q.SkipWordsAndPhrase = ConcatItems(SkipWordsAndPhrase);
            q.SkipFromUsers = ConcatItems(lstSkipFromUsers);
            q.SkipToUsers = ConcatItems(lstSkipToUsers);
            q.SkipMentions = ConcatItems(lstSkipMentions);

            q.lang = GetLanguage(Lang.SelectedText);
            q.maxtweets = Convert.ToInt32(MaxTweets.Text);
            q.near = Near.Text;
            q.within = Within.Text;
            q.since = Since.Value.Date;
            q.until = Until.Value.Date;
            q.toptweets = false;

            Query = q;
        }

        private String[] ConcatItems(ListBox lb)
        {
            List<String> itms = new List<string>();

            foreach (string itm in lb.Items)
                itms.Add(itm);

            return itms.ToArray<String>();
        }

        private void btnAllWordsAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref AllWords, ref AllWordsAndPhrase);
        }

        private void btnAllPhraseAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref AllPhrase, ref AllWordsAndPhrase);
        }

        private void btnAllWPRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref AllWordsAndPhrase);
        }

        private void btnAnyWordsAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref AnyWords, ref AnyWordsAndPhrase);
        }

        private void btnAnyPhraseRemove_Click(object sender, EventArgs e)
        {
            AddItem(ref AnyPhrase, ref AnyWordsAndPhrase);
        }

        private void btnAnyWPRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref AnyWordsAndPhrase);
        }

        private void btnSkipWordsAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref SkipWords, ref SkipWordsAndPhrase);
        }

        private void btnSkipPhraseAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref SkipPhrase, ref SkipWordsAndPhrase);
        }

        private void btnSkipWPRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref SkipWordsAndPhrase);
        }

        private void btnAllHTAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref AllHashtags, ref lstAllHashtags);
        }

        private void btnAllHTRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstAllHashtags);
        }

        private void btnAnyHTAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref AnyHashtags, ref lstAnyHashtags);
        }

        private void btnAnyHTRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstAnyHashtags);
        }

        private void btnFromAllUserAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref FromAllUsers, ref lstFromAllUsers);
        }

        private void btnFromAllUserRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstFromAllUsers);
        }

        private void btnFromAnyUserAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref FromAnyUsers, ref lstFromAnyUsers);
        }

        private void btnFromAnyUserRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstFromAnyUsers);
        }

        private void btnSkipFromUserAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref SkipFromUsers, ref lstSkipFromUsers);
        }

        private void btnSkipFromUserRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstSkipFromUsers);
        }

        private void btnToAllUserAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref ToAllUsers, ref lstToAllUSers);
        }

        private void btnToAllUserRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstToAllUSers);
        }

        private void btnToAnyUserAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref ToAnyUsers, ref lstToAnyUsers);
        }

        private void btnToAnyUserRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstToAnyUsers);
        }

        private void btnSkipToUserAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref SkipToUsers, ref lstSkipToUsers);
        }

        private void btnSkipToUserRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstSkipToUsers);
        }

        private void btnAllMentionsAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref AllMentions, ref lstAllMentions);
        }

        private void btnAllMentionsRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstAllMentions);
        }

        private void btnAnyMentionsAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref AnyMentions, ref lstAnyMentions);
        }

        private void btnAnyMentionsRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstAnyMentions);
        }

        private void btnSkipMentionsAdd_Click(object sender, EventArgs e)
        {
            AddItem(ref SkipMentions, ref lstSkipMentions);
        }

        private void btnSkipMentionsRemove_Click(object sender, EventArgs e)
        {
            RemoveItem(ref lstSkipMentions);
        }

        private void Since_ValueChanged(object sender, EventArgs e)
        {
            Until.MinDate = Since.Value;
        }

        private void Until_ValueChanged(object sender, EventArgs e)
        {
            Since.MaxDate = Until.Value;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            PrepareQuery();
            frmTestQuery f = new frmTestQuery(this.Query, Convert.ToInt32(speed.Text));

            f.ShowDialog();
        }

    }
}

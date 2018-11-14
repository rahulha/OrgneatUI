using System;
using Collector.Utilities;

namespace Collector.Generics
{
    [Serializable]
    public class Tweet
    {
        private string iD;

        private string conversationID;

        private User author = new User();

        private DateTime date;

        private string text;

        private string language;

        private int replies;

        private int retweets;

        private int favorites;

        private string mentions;

        private string permalink;

        //public string Hashtags;

        //public string URLs;

        private bool isPartOfConversation;

        private bool isRootOFConversation;

        private bool isReply;

        private bool isRetweet;

        private User replyToUser = new User();

        private string quotedTweetID;

        private String quotedTweetItemType;

        private string quotedTweetConversationID;
        private string quotedTweetUser;

        private string quotedTweetUserID;

        private bool isVerified;

        private string nonce;

        //public bool StatInitialized;

        private string itemID;

        private string disclosureType;

        private bool hasCards;

        private string componentContext;

        private bool hasParentTweet;




        public string ID { get => iD; set => iD = TextManager.CleanTextForCSV(value); }
        public string ConversationID { get => conversationID; set => conversationID = TextManager.CleanTextForCSV(value);  }
        public string Mentions { get => mentions; set => mentions = TextManager.CleanTextForCSV(value);  }
        public string Permalink { get => permalink; set => permalink = TextManager.CleanTextForCSV(value);  }
        public string Text { get => text; set => text = TextManager.CleanTextForCSV(value);  }
        public string Language { get => language; set => language = TextManager.CleanTextForCSV(value);  }
        public string QuotedTweetID { get => quotedTweetID; set => quotedTweetID = TextManager.CleanTextForCSV(value);  }
        public string QuotedTweetItemType { get => quotedTweetItemType; set => quotedTweetItemType = TextManager.CleanTextForCSV(value);  }
        public string QuotedTweetConversationID { get => quotedTweetConversationID; set => quotedTweetConversationID = TextManager.CleanTextForCSV(value);  }
        public string QuotedTweetUser { get => quotedTweetUser; set => quotedTweetUser = TextManager.CleanTextForCSV(value);  }
        public string QuotedTweetUserID { get => quotedTweetUserID; set => quotedTweetUserID = TextManager.CleanTextForCSV(value);  }
        public string Nonce { get => nonce; set => nonce = TextManager.CleanTextForCSV(value);  }
        public string ItemID { get => itemID; set => itemID = TextManager.CleanTextForCSV(value);  }
        public string DisclosureType { get => disclosureType; set => disclosureType = TextManager.CleanTextForCSV(value);  }
        public string ComponentContext { get => componentContext; set => componentContext = TextManager.CleanTextForCSV(value);  }


        public User Author { get => author; set => author = value; }
        public User ReplyToUser { get => replyToUser; set => replyToUser = value; }

        public DateTime Date { get => date; set => date = value; }
        public int Replies { get => replies; set => replies = value; }
        public int Retweets { get => retweets; set => retweets = value; }
        public int Favorites { get => favorites; set => favorites = value; }
        public bool IsPartOfConversation { get => isPartOfConversation; set => isPartOfConversation = value; }
        public bool IsRootOFConversation { get => isRootOFConversation; set => isRootOFConversation = value; }
        public bool IsReply { get => isReply; set => isReply = value; }
        public bool IsRetweet { get => isRetweet; set => isRetweet = value; }
        public bool IsVerified { get => isVerified; set => isVerified = value; }
        public bool HasCards { get => hasCards; set => hasCards = value; }
        public bool HasParentTweet { get => hasParentTweet; set => hasParentTweet = value; }
    }
}

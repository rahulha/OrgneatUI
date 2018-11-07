using System;

namespace Collector.Generics
{
    [Serializable]
    public class Tweet
    {
        public string ID;

        public string ConversationID;

        public User Author=new User();

        public DateTime Date;

        public string Text;

        public string Language;

        public int Replies;

        public int Retweets;

        public int Favorites;

        public string Mentions;

        public string Permalink;

        //public string Hashtags;

        //public string URLs;

        public bool isPartOfConversation;

        public bool isRootOFConversation;

        public bool isReply;

        public bool isRetweet;

        public User ReplyToUser =new User();

        public string QuotedTweetID;

        public String QuotedTweetItemType;

        public string QuotedTweetConversationID;
        public string QuotedTweetUser;

        public string QuotedTweetUserID;

        public bool isVerified;

        public string Nonce;

        public bool StatInitialized;

        public string itemID;

        public string DisclosureType;

        public bool HasCards;

        public string ComponentContext;

        public bool HasParentTweet;

    }
}

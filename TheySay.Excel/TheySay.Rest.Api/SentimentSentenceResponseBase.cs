namespace TheySay.Rest.Api
{
    public class SentimentSentenceResponseBase 
    {
        public Sentiment sentiment { get; set; }

        public int start { get; set; }
        public int end { get; set; }
        public string text { get; set; }
    }
}
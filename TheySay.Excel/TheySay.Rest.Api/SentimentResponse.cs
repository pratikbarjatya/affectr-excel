namespace TheySay.Rest.Api
{
    public class SentimentResponse 
    {
        public Sentiment sentiment { get; set; }
        public int wordCount { get; set; }
    }
}

namespace TheySay.Rest.Api
{
    public class SentimentEntityResponse : SentimentSentenceResponseBase
    {
        public string sentence { get; set; }
        public string sentenceHtml { get; set; }
        public string headNoun { get; set; }
        public int headNounIndex { get; set; }
        public double salience { get; set; }
    }
}
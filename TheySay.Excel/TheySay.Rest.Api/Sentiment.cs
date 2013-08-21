namespace TheySay.Rest.Api
{
    public class Sentiment
    {
        public string label { get; set; }
        public double positive { get; set; }
        public double negative { get; set; }
        public double neutral { get; set; }

        public double confidence { get; set; }
    }
}
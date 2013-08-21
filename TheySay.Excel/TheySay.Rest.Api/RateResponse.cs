namespace TheySay.Rest.Api
{
    public class Rate
    {
        public int remaining { get; set; }
        public int limit { get; set; }
    }

    public class RateResponse
    {
        public Rate rate { get; set; }
    }
}

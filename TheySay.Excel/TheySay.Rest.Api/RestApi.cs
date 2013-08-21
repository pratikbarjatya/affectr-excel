using System.Threading.Tasks;
namespace TheySay.Rest.Api
{
    public class RestApi : RestBase
    {
        public RestApi(string username, string password, string baseuri) : base(username, password, baseuri)
        {
        }

        public ResponseWrapper<string> Ping()
        {
            var resp = Get("/ping");
            return resp.Result;
        }

        public Task<ResponseWrapper<string>>  PingAsync()
        {
            return Get("/ping");
        }

        public Task<ResponseWrapper<SentimentResponse>> DocumentSentimentAsync(AnalysisCell cell)
        {
            var input = new SentimentRequest { text = cell.Text };

            return PostRequest<SentimentResponse, SentimentRequest>(input, "/v1/sentiment", cell);
        }

        public Task<ResponseWrapper<SentimentSentenceResponse[]>> SentenceSentimentAsync(AnalysisCell cell)
        {
            var input = new SentimentRequest { text = cell.Text, level = "sentence" };

            return PostRequest<SentimentSentenceResponse[], SentimentRequest>(input, "/v1/sentiment", cell);
        }

        public Task<ResponseWrapper<SentimentEntityResponse[]>> EntitySentimentAsync(AnalysisCell cell)
        {
            var input = new SentimentRequest { text = cell.Text, level = "entity" };

            return PostRequest<SentimentEntityResponse[], SentimentRequest>(input, "/v1/sentiment", cell);
        }

        public Task<ResponseWrapper<RateResponse>> GetLimitAsync()
        {
            return GetRequest<RateResponse>("/rate_limit");
        }
    }
}

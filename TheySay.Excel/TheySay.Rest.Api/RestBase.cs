using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TheySay.Rest.Api
{
    public class Limit
    {
        public enum ELimit
        {
            DayRemaining = 1,
            DayLimit,
            DayLimitResetTime,
            RateLimitInterval,
            WindowRateLimit,
            WindowLimitRemaining,
            WindowLimitResetTime
        }
        public string Name { get; set; }
        public ELimit LimitType { get; set; }

    }
    public class RestBase
    {
        protected string Username { get; set; }
        protected string Password { get; set; }

        //todo config for base url
        protected string BaseUri { get; set; }

        // + responseheader fields for rate limit values
        protected Limit[] Ratelimits =
        {
            new Limit {Name = "X-RequestLimit-Remaining", LimitType = Limit.ELimit.DayRemaining}, // daily rate
            new Limit {Name = "X-RequestLimit-Limit", LimitType = Limit.ELimit.DayLimit},          // daily limit
            new Limit {Name = "X-RequestLimit-Reset", LimitType = Limit.ELimit.DayLimitResetTime}, // daily reset time
            new Limit {Name = "X-RateLimit-IntervalSecs", LimitType = Limit.ELimit.RateLimitInterval}, // window length in secs
            new Limit {Name = "X-RateLimit-Limit", LimitType = Limit.ELimit.WindowRateLimit},          // window rate
            new Limit {Name = "X-RateLimit-Remaining", LimitType = Limit.ELimit.WindowLimitRemaining}, // window remaining
            new Limit {Name = "X-RateLimit-Reset", LimitType = Limit.ELimit.WindowLimitResetTime}      // next window time
        };

        public RestBase(string username, string password, string baseuri)
        {
            UpdateSettings( username,  password,  baseuri);
        }

        public void UpdateSettings(string username, string password, string baseuri)
        {
            Username = username;
            Password = password;
            BaseUri = baseuri;
        }

        public Task<ResponseWrapper<TOutput>> PostRequest<TOutput>(string path) where TOutput : class
        {
            return Request<TOutput, object>(null, path, null);
        }

        public Task<ResponseWrapper<TOutput>> PostRequest<TOutput, TInput>(TInput obj, string path, AnalysisCell cell) where TOutput : class 
        {
            return Request<TOutput, TInput>(obj, path, cell);
        }

        private async Task<ResponseWrapper<TOutput>> Request<TOutput, TInput>(TInput obj, string path, AnalysisCell cell) where TOutput : class 
        {
            try
            {
                using (var handler = new HttpClientHandler {Credentials = new NetworkCredential(Username, Password)})
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var reqContent = JsonHelper.JsonSerialize(obj);
                    HttpContent content = new StringContent(reqContent, Encoding.UTF8); 
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var resp = await client.PostAsync(path, content);

                    //test response code
                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        var wrapper2 = new ResponseWrapper<TOutput>(null, cell);

                        GetHeaderValues<TOutput, TInput>(resp, wrapper2);
                        wrapper2.ErrorMessage = (int)resp.StatusCode + " (" + resp.ReasonPhrase + ")";
                        
                        Trace.WriteLine("FAILED: " + (int)resp.StatusCode);
                        
                        return wrapper2;
                    }

                    resp.EnsureSuccessStatusCode();
                    var response = await resp.Content.ReadAsStringAsync();

                    var des = JsonHelper.JsonDeserialize<TOutput>(response);

                    var wrapper = new ResponseWrapper<TOutput>(des, cell);
                    GetHeaderValues<TOutput, TInput>(resp, wrapper);
                    
                    return wrapper;
                }
            }
            catch (AggregateException aex)
            {
                var resp = new ResponseWrapper<TOutput>(null, cell);
                aex.Flatten().Handle(ex => // Note that we still need to call Flatten
                                         {
                                             if (ex is HttpRequestException)
                                             {
                                                 resp.ErrorMessage = ex.Message;
                                                 return true; // This exception is "handled"
                                             }
                                             if (ex is IndexOutOfRangeException)
                                             {
                                                 resp.ErrorMessage = ex.Message;

                                                 Trace.WriteLine("Index out of range");
                                                 return true; // This exception is "handled"   
                                             }
                                             return false; // All other exceptions will get rethrown
                                         });
                return resp;
            }
            catch (HttpRequestException ex)
            {
                return new ResponseWrapper<TOutput>(null, cell){ErrorMessage = ex.Message};   
            }
        }

        private void GetHeaderValues<TOutput, TInput>(HttpResponseMessage resp, ResponseWrapper<TOutput> wrapper) where TOutput : class
        {
            try
            {
                foreach (var limit in Ratelimits)
                {
                    var vals = resp.Headers.GetValues(limit.Name).ToList();
                    wrapper[limit.LimitType] = Convert.ToInt64(vals[0]);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception getting headers for staus: " + (int)resp.StatusCode + " " + ex.Message);
            }
        }

        public Task<ResponseWrapper<TOutput>> GetRequest<TOutput>(string path, params string[] args) where TOutput : class
        {
            return Get<TOutput>(BuildUrl(path, args));
        }

        public async Task<ResponseWrapper<string>> Get(string uri)
        {
            try
            {
                using (var handler = new HttpClientHandler { Credentials = new NetworkCredential(Username, Password) })
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(BaseUri);

                    var response = await client.GetStringAsync(uri);

                    var wrapper = new ResponseWrapper<string>(response, null);
                    return wrapper;
                }
            }
            catch (HttpRequestException ex)
            {
                return new ResponseWrapper<string>(null, null) { ErrorMessage = ex.Message };
            }

        }
        private async Task<ResponseWrapper<TOutput>> Get<TOutput>(string uri) where TOutput : class
        {
            try
            {
                using (var handler = new HttpClientHandler { Credentials = new NetworkCredential(Username, Password) })
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(BaseUri);

                    var response = await client.GetStringAsync(uri);

                    var des = JsonHelper.JsonDeserialize<TOutput>(response);

                    var wrapper = new ResponseWrapper<TOutput>(des);

                    return wrapper;
                }
            }
            catch (HttpRequestException ex)
            {
                return new ResponseWrapper<TOutput>(null) { ErrorMessage = ex.Message };
            }
        }

        protected string BuildUrl(string path, params string[] args)
        {
            path = string.Format(path, args);

            return path;
        }
    }
}

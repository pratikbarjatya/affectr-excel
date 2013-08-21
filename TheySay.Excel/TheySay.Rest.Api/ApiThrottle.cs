using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheySay.Rest.Api
{
    public class ApiThrottle : RestApi
    {
        private readonly ApiControlData _controlData;
        private readonly int _warnSpanMinutes;

        public ApiThrottle(string username, string password, string baseuri, int warnTime) : base(username, password, baseuri)
        {
            _controlData = new ApiControlData();
            _warnSpanMinutes = warnTime;
        }

        public ApiControlData GetStatus()
        {
            if (_controlData.NextWindowTime != new DateTime())
            {
                return _controlData;
            }
            return null;
        }

        public async Task<List<SentimentSentenceResponse[]>> AnalyseSentence(List<AnalysisCell> dataList,
            Func<List<Task<ResponseWrapper<SentimentSentenceResponse[]>>>, List<AnalysisCell>, int, int> callbackFunc,
            Func<int, bool> warnTimeCallback)
        {
            return await Analyse(dataList, callbackFunc, SentenceSentimentAsync, warnTimeCallback);
        }

        public async Task AnalyseDocument(List<AnalysisCell> dataList, 
            Func<List<Task<ResponseWrapper<SentimentResponse>>>, List<AnalysisCell>, int, int> callbackFunc, 
            Func<int, bool> warnTimeCallback)
        {
            await Analyse(dataList, callbackFunc, DocumentSentimentAsync, warnTimeCallback);
        }

        protected async Task<List<T>> Analyse<T>(List<AnalysisCell> dataList, 
            Func<List<Task<ResponseWrapper<T>>>, List<AnalysisCell>, int, int> callbackFunc,
            Func<AnalysisCell, Task<ResponseWrapper<T>>> apiCall,
            Func<int, bool> warnTimeCallback = null) where T : class
        {
            if (dataList == null)
                throw new ArgumentNullException("dataList");

            if (!CheckServiceStatus())
                throw new ApplicationException("Api currently unavailable for configured API Root.");

            var tasks = new List<Task<ResponseWrapper<T>>>();
            var completed = new List<Task<ResponseWrapper<T>>>();
            var results = new List<T>();

            var retry = new List<ResponseWrapper<T>>();

            var noProcessed = 0;
            while (noProcessed < dataList.Count || retry.Any())
            {
                var resp = GetLimits();
                if (!resp.Result.Success)
                {
                    throw new ApplicationException(resp.Result.ErrorMessage);
                }
                var callbackIndex = noProcessed - retry.Count;

                tasks.AddRange(retry.Select(responseWrapper => apiCall(responseWrapper.Input) ));

                var retries = retry.Count;
                retry.Clear();

                var remaining = noProcessed + Math.Min(_controlData.WindowRemaining, dataList.Count - noProcessed);
                for (int index = noProcessed; index < remaining - retries; index++)
                {
                    var item = dataList[index];
                    if (item != null)
                    {
                        string val = item.Text;
                        if (!string.IsNullOrEmpty(val))
                        {
                            var task = apiCall(item); 
                            tasks.Add(task);
                            Trace.WriteLine("Created task: " + index);
                        }
                    }
                    noProcessed++;
                }

                foreach (var bucket in Interleaved(tasks))
                {
                    var t = await bucket;
                    try
                    {
                        Process(await t, results, retry);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                    catch (Exception exc)
                    {
                        Handle(exc);
                    }
                }

                if (tasks.Any())
                {
                    callbackFunc(tasks, dataList, callbackIndex);
                }

                if (noProcessed < dataList.Count || retry.Any())
                {
                    if (_controlData.NextWindowTime != new DateTime())
                    {
                        // delay until next window starts
                        var span = _controlData.NextWindowTime - DateTime.UtcNow;
                        Trace.WriteLine("delay span:" + span);
                        if (span.Minutes > _warnSpanMinutes)
                        {
                            if (warnTimeCallback != null)
                            {
                                if (!warnTimeCallback(span.Minutes))
                                {
                                    return results;
                                }
                            }
                        }
                        if (span.TotalMilliseconds > 0)
                            await Task.Delay(span);
                    }
                    else
                    {
                        // we dont have the next window start - so just delay for 20 secs
                        await Task.Delay(20 * 1000);
                    }
                }
                completed.AddRange(tasks);
                tasks.Clear();
            }
            return results;
        }


        public static Task<Task<T>>[] Interleaved<T>(IEnumerable<Task<T>> tasks)
        {
            var inputTasks = tasks.ToList();

            var buckets = new TaskCompletionSource<Task<T>>[inputTasks.Count];
            var results = new Task<Task<T>>[buckets.Length];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new TaskCompletionSource<Task<T>>();
                results[i] = buckets[i].Task;
            }

            int nextTaskIndex = -1;
            Action<Task<T>> continuation = completed =>
            {
                var bucket = buckets[Interlocked.Increment(ref nextTaskIndex)];
                bucket.TrySetResult(completed);
            };

            foreach (var inputTask in inputTasks)
                inputTask.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

            return results;
        }

        private void Handle(Exception exc)
        {
            Trace.WriteLine(exc.Message);
        }

        private void Process<T>(ResponseWrapper<T> responseWrapper, List<T> results, List<ResponseWrapper<T>> retry) where T : class
        {
            if (responseWrapper.Success)
            {
                lock (this)
                {
                    results.Add(responseWrapper.Response);

                    _controlData.DayLimit = responseWrapper.Limit;
                    _controlData.DayWindowResetTime = responseWrapper.ResetTime;
                    _controlData.IntervalInSecs = responseWrapper.IntervalLengthSecs;
                    _controlData.WindowLimit = responseWrapper.WindowLimit;

                    if (responseWrapper.WindowResetTime > _controlData.NextWindowTime)
                    {
                        _controlData.NextWindowTime = responseWrapper.WindowResetTime;
                        _controlData.WindowRemaining = responseWrapper.WindowRemaining;
                        _controlData.DayRemaining = responseWrapper.Remaining;
                    }
                    else
                    {
                        if (responseWrapper.WindowRemaining < _controlData.WindowRemaining)
                        {
                            _controlData.WindowRemaining = responseWrapper.WindowRemaining;                            
                        }
                        if (responseWrapper.Remaining < _controlData.DayRemaining)
                        {
                            _controlData.DayRemaining = responseWrapper.Remaining;
                        }
                    }
                }
                Trace.WriteLine("window remaining: " + _controlData.WindowRemaining);
                Trace.WriteLine("day remaining: " + _controlData.DayRemaining);
                Trace.WriteLine("window next: " + _controlData.NextWindowTime);
            }
            else
            {
                if (responseWrapper.ErrorMessage.Contains("429"))
                {
                    // add to retry - use Input property for retry
                    retry.Add(responseWrapper);
                }
                else
                    throw new Exception("Failed: " + responseWrapper.ErrorMessage);
            }
        }

        private Task<ResponseWrapper<RateResponse>> GetLimits()
        {
            var resp = GetLimitAsync();

            if (resp.Result.Success)
            {
                _controlData.WindowLimit = resp.Result.Response.rate.limit;
                _controlData.WindowRemaining = resp.Result.Response.rate.remaining;
                Trace.WriteLine("Limits window remaining: " + _controlData.WindowRemaining);
            }
            return resp;
        }

        private bool CheckServiceStatus()
        {
            // ping the api - dont get rate limit headers in this call.
            var resp = Ping();

            return resp.Success;
        }

        public async Task<List<SentimentEntityResponse[]>> AnalyseEntity(List<AnalysisCell> cells, 
            Func<List<Task<ResponseWrapper<SentimentEntityResponse[]>>>, List<AnalysisCell>, int, int> processEntityResults,
            Func<int, bool> warnTimeCallback)
        {
            return await Analyse(cells, processEntityResults, EntitySentimentAsync, warnTimeCallback);
        }
    }
}

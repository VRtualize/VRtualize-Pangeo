using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using UnityEngine;

public static class BingApiRequestManager
{
    private static Semaphore pool;
    private static HttpClient client;
    static BingApiRequestManager()
    {
        pool = new Semaphore(5, 5);
        client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
    }
    public static string getUrlData(string url)
    {
        Globals.mut.WaitOne();
        int start = DateTime.Now.Millisecond;
        var response = client.GetAsync(url).Result;
        //while (DateTime.Now.Millisecond - start < 200) { await Task.Delay(1); }
        bool tooManyRequestFlag = false;
        do
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    return response.Content.ToString();
                }
            }
            catch (HttpRequestException e)
            {
                if (e.Message == "429 (Too Many Requests)")
                    tooManyRequestFlag = true;
                else
                    throw e;
            }
        } while (tooManyRequestFlag);
        Globals.mut.ReleaseMutex();
        return "NOTFOUND IN BING API REQUEST MANAGER";
    }
}

namespace Twitter.Data.Infrastructure
{
    using System.Net;

    public class HTMLReader
    {
        public string ReadFromWeb(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent: Other");
                string htmlCode = client.DownloadString(url);
                return htmlCode;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net;

namespace Netease_Get
{
    class Http
    {
        public int DownloadSize { get; set; } = 0;

        public async Task<string> GetFromUrl(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
            string response = await client.GetStringAsync(url);
            return response;
        }

        public async Task<bool> DownloadFormUrl(string url, string path)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
            var response = await client.GetAsync(url);

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var fileInfo = new FileInfo(path);
                using (var fileStream = fileInfo.OpenWrite())
                {
                    await stream.CopyToAsync(fileStream);
                    return true;
                }
            }
        }
    }
}

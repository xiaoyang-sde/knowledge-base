using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;

namespace Netease_Get
{
    class Http
    {
        public async Task<string> GetFromUrl(string url)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            return response;
        }

        public async Task<bool> DownloadFormUrl(string url, string path)
        {
            var client = new HttpClient();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Netease_Get
{
    class API
    {
        public SongList SongList = new SongList();
        private readonly Http HttpClient = new Http();

        //https://music.163.com/api/album/35288173/ Album Json
        //http://music.163.com/song/media/outer/url?id=31081299 Download
        //http://api.javaswing.cn/song/detail?ids=31081299 SingleDetail

        public Task<dynamic> JsonToObject(string jsonString)
        {
            var result = JsonConvert.DeserializeObject<dynamic>(jsonString);
            return result;
        }

        public async Task<string> GetAlbum(string id)
        {
            string api = "https://music.163.com/api/album/";
            string url = api + id;
            string json = await HttpClient.GetFromUrl(url);
            JObject jsonReader = JObject.Parse(json);
            if (jsonReader["code"].ToString() == "200")
            {
                int count = jsonReader["album"]["songs"].Count();
                for (int i = 0; i < count; i++)
                {
                    string name = jsonReader["album"]["songs"][i]["name"].ToString();
                    string sid = jsonReader["album"]["songs"][i]["id"].ToString();
                    string artist = jsonReader["album"]["songs"][i]["artists"][0].ToString();
                    SongList.Add(sid, name, artist);
                }
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }

        public async Task<string> GetSingle(string id)
        {
            string api = "http://api.javaswing.cn/song/detail?ids=";
            string url = api + id;
            string json = await HttpClient.GetFromUrl(url);
            JObject jsonReader = JObject.Parse(json);
            string name = jsonReader["album"]["songs"]["name"].ToString();
            string artist = jsonReader["album"]["songs"]["ar"][0]["name"].ToString();
            SongList.Add(id,name,artist);
            return "Success";
        }

        public async Task<string> GetPlayList(string id)
        {
            string api = "http://api.javaswing.cn/song/detail?ids=";
            string url = api + id;
            string json = await HttpClient.GetFromUrl(url);
            JObject jsonReader = JObject.Parse(json);

            int count = jsonReader["playlist"]["tracks"].Count();
            for (int i = 0; i < count; i++)
            {
                string name = jsonReader["playlist"]["tracks"][i]["name"].ToString();
                string sid = jsonReader["playlist"]["tracks"][i]["id"].ToString();
                string artist = jsonReader["playlist"]["tracks"][i]["ar"][0]["name"].ToString();
                SongList.Add(sid, name, artist);
            }
            return "Success";
        }
    }
}

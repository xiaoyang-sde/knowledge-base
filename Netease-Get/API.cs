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
        private readonly Http HttpClient = new Http();
        private readonly SongList SongList = new SongList();

        //https://music.163.com/api/album/35288173/ Album Json
        //http://music.163.com/song/media/outer/url?id=31081299 Download
        //http://api.javaswing.cn/song/detail?ids=31081299 SingleDetail

        public Task<dynamic> JsonToObject(string jsonString)
        {
            var result = JsonConvert.DeserializeObject<dynamic>(jsonString);
            return result;
        }

        public async Task<List<string>> GetAlbum(string id)
        {
            string api = "https://music.163.com/api/album/";
            string url = api + id;
            string json = await HttpClient.GetFromUrl(url);
            List<string> nameList = new List<string>();

            JObject jsonReader = JObject.Parse(json);
            if (jsonReader["code"].ToString() == "200")
            {
                int count = jsonReader["album"]["songs"].Count();
                for (int i = 0; i < count; i++)
                {
                    string name = jsonReader["album"]["songs"][i]["name"].ToString();
                    string sid = jsonReader["album"]["songs"][i]["id"].ToString();
                    string artist = jsonReader["album"]["songs"][i]["artists"][0]["name"].ToString();
                    nameList.Add(name + "       " + artist);
                    SongList.Add(sid, name);
                }
            }
            return nameList;
        }

        public async Task<string> GetSingle(string id)
        {
            string api = "http://api.javaswing.cn/song/detail?ids=";
            string url = api + id;
            string json = await HttpClient.GetFromUrl(url);
            string name = "";
            JObject jsonReader = JObject.Parse(json);
            if (jsonReader["code"].ToString() == "200")
            {
                name = jsonReader["songs"][0]["name"].ToString();
                string artist = jsonReader["songs"][0]["ar"][0]["name"].ToString();
                SongList.Add(id, name);
                name = name + "      " + artist;
            }
            return name;
        }

        public async Task<List<string>> GetPlayList(string id)
        {
            string api = "http://api.javaswing.cn/playlist/detail?id=";
            string url = api + id;
            string json = await HttpClient.GetFromUrl(url);
            JObject jsonReader = JObject.Parse(json);
            List<string> nameList = new List<string>();

            int count = jsonReader["playlist"]["tracks"].Count();
            for (int i = 0; i < count; i++)
            {
                string name = jsonReader["playlist"]["tracks"][i]["name"].ToString();
                string sid = jsonReader["playlist"]["tracks"][i]["id"].ToString();
                string artist = jsonReader["playlist"]["tracks"][i]["ar"][0]["name"].ToString();
                nameList.Add(name + "       " + artist);
                SongList.Add(sid, name);
            }
            return nameList;
        }
    }
}

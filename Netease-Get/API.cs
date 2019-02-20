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
        public delegate void UpdateStatus();
        public event UpdateStatus Update;

        private readonly Http HttpClient = new Http();
        private readonly SongList SongList = new SongList();
        public string DownloadStatus { get; set; } = "";

        private async Task<int> Download(string id, string name, string appPath)
        {
            string api = "https://api.imjad.cn/cloudmusic/?type=song&id=";
            string url = api + id;
            string json = await HttpClient.GetFromUrl(url);
            JObject jsonReader = JObject.Parse(json);
            if (jsonReader["code"].ToString() == "200")
            {
                if (jsonReader["data"][0]["code"].ToString() == "200")
                {
                    string durl = jsonReader["data"][0]["url"].ToString();
                    string i = "";
                    int j = 0;
                    string path = appPath + "/Music/" + name + i + ".mp3";
                    while (File.Exists(path))
                    {
                        j++;
                        i = j.ToString();
                        path = appPath + "/Music/" + name + i + ".mp3";
                    }
                    await HttpClient.DownloadFormUrl(durl, path);
                }
                else
                {
                    return 404;
                }
            }
            else
            {
                return 503;
            }
            return 200;
        }

        public async Task<bool> DownloadAll(string appPath)
        {
            try
            {
                int Status = 0;
                Dictionary<string, string> dict = SongList.SongDict;
                int i = 1;
                int all = dict.Count();

                if (!Directory.Exists(appPath + "/Music/"))
                {
                    Directory.CreateDirectory(appPath + "/Music/");
                }

                foreach (var item in dict)
                {
                    string index = " ( " + i.ToString() + " / " + all.ToString() + " )";
                    string id = item.Key;
                    string name = item.Value;
                    DownloadStatus = "Downloading: " + name + index;
                    Update();
                    Status = await Download(id, name, appPath);

                    if (Status == 200)
                    {
                        DownloadStatus = "Download Completed: " + name + index;
                    }
                    else if (Status == 404)
                    {
                        DownloadStatus = "Download Failed: " + name + " has been removed." + index;
                    }
                    else if (Status == 503)
                    {
                        DownloadStatus = "Download Failed: API Error" + index;
                    }
                    Update();
                    i = i + 1;
                }
                return true;
            }
            catch (Exception e)
            {
                DownloadStatus = e.Message;
                Update();
                return false;
            }
        }

        public async Task<List<string>> GetAlbum(string id)
        {
            string api = "https://api.imjad.cn/cloudmusic/?type=album&id=";
            string url = api + id;
            string json = await HttpClient.GetFromUrl(url);
            List<string> nameList = new List<string>();

            JObject jsonReader = JObject.Parse(json);
            if (jsonReader["code"].ToString() == "200")
            {
                string title = jsonReader["album"]["name"].ToString();
                string author = jsonReader["album"]["artist"]["name"].ToString();
                if (jsonReader["album"]["alias"].Count() != 0)
                {
                    string alias = jsonReader["album"]["alias"][0].ToString();
                    DownloadStatus = title + "(" + alias + ")      " + author;
                }
                else { DownloadStatus = title + "      " + author; }
                Update();

                int count = jsonReader["songs"].Count();
                for (int i = 0; i < count; i++)
                {
                    string name = jsonReader["songs"][i]["name"].ToString();
                    string sid = jsonReader["songs"][i]["id"].ToString();
                    string artist = jsonReader["songs"][i]["ar"][0]["name"].ToString();

                    if (SongList.SongDict.ContainsKey(sid) == false)
                    {
                        nameList.Add(name + "       " + artist);
                        SongList.Add(sid, name);
                    }
                }
            }

            else if (jsonReader["code"].ToString() == "404")
            {
                DownloadStatus = "Album Not Found";
                Update();
            }
            return nameList;
        }


        public async Task<string> GetSingle(string id)
        {
            string api = "https://api.imjad.cn/cloudmusic/?type=detail&id=";
            string url = api + id;
            string json = await HttpClient.GetFromUrl(url);
            string name = "";
            JObject jsonReader = JObject.Parse(json);
            if (jsonReader["code"].ToString() == "200")
            {
                if (jsonReader["songs"].Count() != 0)
                {
                    name = jsonReader["songs"][0]["name"].ToString();
                    string artist = jsonReader["songs"][0]["ar"][0]["name"].ToString();
                    if (!SongList.SongDict.ContainsKey(id))
                    {
                        SongList.Add(id, name);
                        name = name + "      " + artist;
                        return name;
                    }
                }
                else
                {
                    DownloadStatus = "Single Not Found";
                    Update();
                    return "";
                }
            }
            return "";
        }

        public async Task<List<string>> GetPlayList(string id)
        {
            List<string> nameList = new List<string>();
            {
                string api = "https://api.imjad.cn/cloudmusic/?type=playlist&id=";
                string url = api + id;
                string json = await HttpClient.GetFromUrl(url);
                JObject jsonReader = JObject.Parse(json);

                if (jsonReader["code"].ToString() == "200")
                {
                    string title = jsonReader["playlist"]["name"].ToString();
                    string creator = jsonReader["playlist"]["creator"]["nickname"].ToString();
                    DownloadStatus = title + "      " + creator;
                    Update();

                    int count = jsonReader["playlist"]["tracks"].Count();
                    for (int i = 0; i < count; i++)
                    {
                        string name = jsonReader["playlist"]["tracks"][i]["name"].ToString();
                        string sid = jsonReader["playlist"]["tracks"][i]["id"].ToString();
                        string artist = jsonReader["playlist"]["tracks"][i]["ar"][0]["name"].ToString();

                        if (SongList.SongDict.ContainsKey(sid) == false)
                        {
                            nameList.Add(name + "       " + artist);
                            SongList.Add(sid, name);
                        }
                    }
                }
                else if (jsonReader["code"].ToString() == "404")
                {
                    DownloadStatus = "PlayList  Not Found";
                    Update();
                }


                return nameList;
            }
        }

        public void RemoveAll()
        {
            SongList.DelAll();
        }

        public void Remove(int index)
        {
            SongList.Del(index);
        }
    }
}

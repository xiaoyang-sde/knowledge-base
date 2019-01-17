using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Netease_Get
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private async void AddAlbum_Click(object sender, EventArgs e)
        {
            API api = new API();
            string url = InputBox.Text;
            Regex re = new Regex(@"(album\?id=)\d+", RegexOptions.Compiled);
            string id = re.Match(url).ToString();
            id = id.Replace("album?id=", "");

            List<string> nameList = new List<string>();
            nameList=await api.GetAlbum(id);
            foreach (string name in nameList)
            { 
                DownloadList.Items.Add(name);
           }
        }

        private async void AddSingle_Click(object sender, EventArgs e)
        {
            API api = new API();
            string url = InputBox.Text;
            Regex re = new Regex(@"(song\?id=)\d+", RegexOptions.Compiled);
            string id = re.Match(url).ToString();
            id = id.Replace("song?id=", "");
            string name = "";
            name=await api.GetSingle(id);
            DownloadList.Items.Add(name);
        }

        private async void AddPlayList_Click(object sender, EventArgs e)
        {
            API api = new API();
            string url = InputBox.Text;
            Regex re = new Regex(@"(playlist\?id=)\d+", RegexOptions.Compiled);
            string id = re.Match(url).ToString();
            id = id.Replace("playlist?id=", "");
            List<string> nameList = new List<string>();
            nameList = await api.GetPlayList(id);
            foreach (string name in nameList)
            {
                DownloadList.Items.Add(name);
            }
        }
    }
    }


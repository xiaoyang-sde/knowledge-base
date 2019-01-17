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
using System.IO;
using System.Runtime.InteropServices;

namespace Netease_Get
{
    public partial class MainForm : Form
    {
        private readonly API api = new API();
        private bool isMouseDown = false;
        private Point FormLocation;
        private Point mouseOffset;

        public MainForm()
        {
            InitializeComponent();
            IgnoreDPI();
            api.Update += new API.UpdateStatus(UpdateLabel);
        }



        private async void Add_Click(object sender, EventArgs e)
        {
            try
            {
                string url = InputBox.Text;
                if (url.Contains("song?id=") | url.Contains("song/"))
                {
                    Regex re = new Regex(@"(song\?id=)\d+", RegexOptions.Compiled);
                    string id = re.Match(url).ToString();

                    if (id != "")
                    {
                        id = id.Replace("song?id=", "");
                    }
                    else
                    {
                        re = new Regex(@"(song/)\d+", RegexOptions.Compiled);
                        id = re.Match(url).ToString();
                        id = id.Replace("song/", "");
                    }

                    string name = "";
                    name = await api.GetSingle(id);
                    if (name != "")
                    {
                        DownloadList.Items.Add(name);
                    }
                }
                else if (url.Contains("playlist?id=") | url.Contains("playlist/"))
                {
                    Regex re = new Regex(@"(playlist\?id=)\d+", RegexOptions.Compiled);
                    string id = re.Match(url).ToString();
                    if (id != "")
                    {
                        id = id.Replace("playlist?id=", "");
                    }
                    else
                    {
                        re = new Regex(@"(playlist/)\d+", RegexOptions.Compiled);
                        id = re.Match(url).ToString();
                        id = id.Replace("playlist/", "");
                    }

                    List<string> nameList = new List<string>();
                    nameList = await api.GetPlayList(id);
                    foreach (string name in nameList)
                    {
                        DownloadList.Items.Add(name);
                    }
                }
                else if (url.Contains("album?id=")| url.Contains("album/"))
                {
                    Regex re = new Regex(@"(album\?id=)\d+", RegexOptions.Compiled);
                    string id = re.Match(url).ToString();
                    if (id != "")
                    {
                        id = id.Replace("album?id=", "");
                    }
                    else
                    {
                        re = new Regex(@"(album/)\d+", RegexOptions.Compiled);
                        id = re.Match(url).ToString();
                        id = id.Replace("album/", "");
                    }

                    List<string> nameList = new List<string>();
                    nameList = await api.GetAlbum(id);
                    foreach (string name in nameList)
                    {
                        DownloadList.Items.Add(name);
                    }
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = ex.Message;
            }
            finally
            {
                InputBox.Clear();
            }
        }

        private async void Download_Click(object sender, EventArgs e)
        {
            if (DownloadList.Items.Count != 0)
            {
                Download.Enabled = false;
                string appPath = Application.StartupPath;
                await api.DownloadAll(appPath);
                Download.Enabled = true;
            }

        }

        public void UpdateLabel()
        {
            StatusLabel.Text = api.DownloadStatus;
        }

        private void RemoveSelected_Click(object sender, EventArgs e)
        {
            int index = DownloadList.SelectedIndex;
            if (DownloadList.Items.Count != 0 && index != -1)
            {

                DownloadList.Items.Remove(DownloadList.SelectedItem);
                api.Remove(index);
            }
        }

        private void RemoveAll_Click(object sender, EventArgs e)
        {
            if (DownloadList.Items.Count != 0)
            {
                DownloadList.Items.Clear();
                api.RemoveAll();
            }
        }

        public static int IgnoreDPI()
        {
            SetProcessDPIAware();
            IntPtr screenDC = GetDC(IntPtr.Zero);
            int dpi_x = GetDeviceCaps(screenDC, /*DeviceCap.*/LOGPIXELSX);
            int dpi_y = GetDeviceCaps(screenDC, /*DeviceCap.*/LOGPIXELSY);
            ReleaseDC(IntPtr.Zero, screenDC);

            return dpi_x;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(
        IntPtr hdc, // handle to DC
        int nIndex // index of capability
        );

        [DllImport("user32.dll")]
        internal static extern bool SetProcessDPIAware();

        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;

        private void Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {

            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void GithubBox_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/xAsiimov/Netease-Get");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
            {
                Add.Text = "添加歌曲";
                Download.Text = "下载全部歌曲";
                RemoveSelected.Text = "移除选择歌曲";
                RemoveAll.Text = "移除全部歌曲";
                StatusLabel.Text = "开源协议: Apache 2.0";
            }
        }
    }
}


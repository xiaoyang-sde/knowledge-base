using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netease_Get
{
    class SongList
    {
        private Dictionary<string, string> SongDict { get; set; } = new Dictionary<string, string>();

        public void Add(string id,string name)
        {
            SongDict.Add(id, name);
        }

        public void Del(string id)
        {
            SongDict.Remove(id);
        }
    }
}

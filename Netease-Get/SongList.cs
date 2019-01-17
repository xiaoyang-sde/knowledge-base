using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netease_Get
{
    class SongList
    {
        public Dictionary<string, List<string>> SongDict { get; set; }

        public void Add(string id,string name,string artist)
        {
            List<string> Single = new List<string>
            {
               name,
               artist
            };
            SongDict.Add(id, Single);
        }

        public void Del(string id)
        {
            SongDict.Remove(id);
        }
    }
}

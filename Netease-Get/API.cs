using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Netease_Get
{
    class API
    {
        public Task<dynamic> JsonToObject(string jsonString)
        {
            var result = JsonConvert.DeserializeObject<dynamic>(jsonString);
            return result;
        }
}
}

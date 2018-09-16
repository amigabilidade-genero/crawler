using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SharpDriver.Entities
{
    [JsonObject]
    public class Processo
    {
        [JsonProperty("ro")]
        public string RO { get; set; }
        [JsonProperty("oj")]
        public string OJ { get; set; }
        [JsonProperty("publication", NullValueHandling = NullValueHandling.Ignore)]
        public string Publication { get; set; }
        [JsonProperty("reporter", NullValueHandling = NullValueHandling.Ignore)]
        public string Reporter { get; set; }
        [JsonProperty("fullContent", NullValueHandling = NullValueHandling.Ignore)]
        public string FullContent { get; set; }
    }
}

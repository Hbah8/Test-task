using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestTask.JsonPattern
{
    public class RootObject
    {
        [JsonProperty("facets")]
        public facets[] facets { get; set; }

        [JsonProperty("results")]
        public results[] results { get; set; }

        [JsonProperty("count")]
        public int count { get; set; }

        [JsonProperty("@nextLink")]
        public string @nextLink { get; set; }

    }
    public class facets
    {
        [JsonProperty("category")]
        public category[] category { get; set; }

    }
    public class category
    {
        [JsonProperty("Reference")]
        public Reference[] reference { get; set; }


    }

    public class Reference
    {
        [JsonProperty("type")]
        public int type { get; set; }

        [JsonProperty("from")]
        public string from { get; set; }

        [JsonProperty("to")]
        public string to { get; set; }

        [JsonProperty("value")]
        public string References { get; set; }

        [JsonProperty("count")]
        public int count { get; set; }
    }

    public class results
    {
        [JsonProperty("title")]
        public string title { get; set; }
    }



}

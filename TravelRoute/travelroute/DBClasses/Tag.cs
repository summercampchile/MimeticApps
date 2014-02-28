using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelroute.DBClasses
{
    public class Tag
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "routeId")]
        public string RouteId { get; set; }

        [JsonProperty(PropertyName = "tag")]
        public string TagNom { get; set; }
    }
}

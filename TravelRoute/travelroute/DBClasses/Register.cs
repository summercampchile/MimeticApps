using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace travelroute.DBClasses
{
    public class Register
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "routeId")]
        public string RouteId { get; set; }

        [JsonProperty(PropertyName = "createdByCurrentUser")]
        public bool CreatedByCurrentUser { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "expenses")]
        public int Expenses { get; set; }

        [JsonProperty(PropertyName = "appreciation")]
        public int Appreciation { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "isShared")]

        public bool isShared { get; set; }
        [JsonProperty(PropertyName = "__createdAt")]
        public string CreatedAt { get; set; }
    }
}

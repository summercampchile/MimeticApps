using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace travelroute.DBClasses
{
    public class Route
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "ownerId")]
        public string OwnerId { get; set; }

        [JsonProperty(PropertyName = "originalRouteId")]
        public string OriginalRouteId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }

        [JsonProperty(PropertyName = "routePicture")]
        public string RoutePicture { get; set; }

        [JsonProperty(PropertyName = "copiedNumber")]
        public int CopiedNumber { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "isShared")]
        public bool IsShared { get; set; }

        [JsonProperty(PropertyName = "isPopular")]
        public bool IsPopular { get; set; }

        [JsonProperty(PropertyName = "place")]
        public string Place { get; set; }

        [JsonProperty(PropertyName = "containerName")]
        public string ContainerName { get; set; }

        [JsonProperty(PropertyName = "resourceName")]
        public string ResourceName { get; set; }

        [JsonProperty(PropertyName = "sasQueryString")]
        public string SasQueryString { get; set; }

        [JsonProperty(PropertyName = "price")]
        public int Price { get; set; }

        [JsonProperty(PropertyName = "__createdAt")]
        public string CreatedAt { get; set; }
    }
}

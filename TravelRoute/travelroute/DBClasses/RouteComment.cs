using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelroute.DBClasses
{
    public class RouteComment
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "routeId")]
        public string RouteId { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comentario { get; set; }

        [JsonProperty(PropertyName = "appreciation")]
        public int Appreciation { get; set; }


    }
}

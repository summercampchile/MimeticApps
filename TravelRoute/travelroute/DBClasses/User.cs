using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace travelroute.DBClasses
{
    public class User
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "facebookId")]
        public string FacebookId { get; set; }

        [JsonProperty(PropertyName = "twitterId")]
        public string TwitterId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty(PropertyName = "profilePicture")]
        public string ProfilePicture { get; set; }

        [JsonProperty(PropertyName = "facebookAccessToken")]
        public string FacebookAccessToken { get; set; }

        [JsonProperty(PropertyName = "points")]
        public string Points { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
    }
}

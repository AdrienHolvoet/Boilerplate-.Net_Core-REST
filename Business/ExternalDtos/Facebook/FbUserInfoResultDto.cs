using Newtonsoft.Json;
using System;

namespace Boilerplate.Business.ExternalDtos.Facebook
{
    public class FbUserInfoResultDto
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("picture")]
        public FacebookPicture Picture { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }

    }

    public class FacebookPicture
    {
        [JsonProperty("data")]
        public FacebookPictureData FacebookPictureData { get; set; }
    }

    public class FacebookPictureData
    {

        [JsonProperty("heigth")]
        public long Heigth { get; set; }

        [JsonProperty("is_silhouette")]
        public long IsSilhouette { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }
}


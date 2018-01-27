using System;
using Newtonsoft.Json;

namespace GitRepUserList.Models
{
    public class User
    {
	[JsonProperty("login")]
	public string LoginId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

	[JsonProperty("avatar_url")]
	public string AvatarUrl { get; set; }

	[JsonProperty("bio")]
	public string Bio { get; set; }

	[JsonProperty("repos_url")]
	public string ReposUrl { get; set; }

        [JsonIgnore]
        public string Message { get; set; } = string.Empty;
    }
}

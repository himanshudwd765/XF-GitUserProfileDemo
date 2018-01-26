using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace GitRepUserList.Models
{
    public class Repository
    {
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("html_url")]
		public string HtmlUrl { get; set; }
    }

    public class RepositoryList
    {
		[JsonIgnore]
		public ObservableCollection<Repository> Items { get; set; }

		[JsonIgnore]
		public string Message { get; set; } = string.Empty;

	}
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitRepUserList.Models;
using GitRepUserList.Constant;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System.Collections.ObjectModel;

namespace GitRepUserList.Services
{
    public class Webservice
    {
	HttpClient client;
        User user;
        ObservableCollection<Repository> repos;
        RepositoryList repositoryList;

        public Webservice()
        {
	    client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BaseUrl}/");
            client.DefaultRequestHeaders.Add("User-Agent", "Other");
        }

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="userName">User name.</param>
	public async Task<User> GetUserAsync(string userName)
	{
            user = new User();
	    try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await client.GetAsync($"users/" + userName);
                    if(response.IsSuccessStatusCode)
                    {
			string res = await response.Content.ReadAsStringAsync();
			user = await Task.Run(() => JsonConvert.DeserializeObject<User>(res));
                    }
                    else
                    {
                        user.Message = Constants.MessageUserNotFound;
                    }
                }
                else
                {
                    user.Message = Constants.MessageNoNetwork;
                }
                return user;
	    } 
            catch (Exception ex) 
            {
                user.Message = Constants.MessageAPIRequestFail;
                return user;
	     }
	}

	/// <summary>
	/// Gets the user repository.
	/// </summary>
	/// <returns>The user repos async.</returns>
	/// <param name="user">User.</param>
	public async Task<RepositoryList> GetUserReposAsync(User user)
	{
	    repos = new ObservableCollection<Repository>();
            repositoryList = new RepositoryList();
	    try
	    {
		if (CrossConnectivity.Current.IsConnected)
		{
                    var response = await client.GetAsync($"users/" + user.LoginId+"/repos");
                    if(response.IsSuccessStatusCode)
                    {
			string res = await response.Content.ReadAsStringAsync();
		        repos = await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<Repository>>(res));
			repositoryList.Items = repos;
                    }
                    else
                    {
                        repositoryList.Message = Constants.MessageRepositoryNotFound;
                    }
					
		}
		else
		{
                    repositoryList.Message = Constants.MessageNoNetwork;
		}

                return repositoryList;
	      }
	      catch (Exception ex)
	      {
                 repositoryList.Message = Constants.MessageAPIRequestFail;
                 return repositoryList;
	      }
	}
    }
}

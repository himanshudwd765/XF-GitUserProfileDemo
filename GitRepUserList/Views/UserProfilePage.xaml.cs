using System;
using System.Collections.Generic;
using GitRepUserList.Models;
using GitRepUserList.Services;
using Xamarin.Forms;

namespace GitRepUserList.Views
{
    public partial class UserProfilePage : ContentPage
    {
	Webservice webservice = new Webservice();
	public User profile { get; set; }
	public RepositoryList userRepositoryList { get; set; }

        public UserProfilePage()
        {
            InitializeComponent();
            profile = new User();
            userRepositoryList = new RepositoryList();
        }

        async void Handle_SearchButtonPressed(object sender, System.EventArgs e)
        {
            if(!string.IsNullOrEmpty(searchBar.Text))
            {
		loader.IsRunning = true;
		detailLayoutView.IsVisible = false;
                errorMessage.IsVisible = false;
		profile = await webservice.GetUserAsync(searchBar.Text);
		if (string.IsNullOrEmpty(profile.Message))
		{
		    userRepositoryList = await webservice.GetUserReposAsync(profile);
		    avtarUrl.Source = profile.AvatarUrl;
                    lblUserName.Text = (string.IsNullOrEmpty(profile.Name)) ? profile.LoginId : profile.Name;
		    listView.ItemsSource = userRepositoryList.Items;
		    detailLayoutView.IsVisible = true;
                    if(userRepositoryList.Items.Count>0)
                    {
                       listView.IsVisible = true;
                       lblNoRepoMessage.IsVisible = false;
                    }
                    else
                    {
                       listView.IsVisible = false;
		       lblNoRepoMessage.IsVisible = true;
                    }
		}
		else
		{
                    errorMessage.IsVisible = true;
		    errorMessage.Text = profile.Message;
		    detailLayoutView.IsVisible = false;
		}		
		loader.IsRunning = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            detailLayoutView.IsVisible = false;
            loader.IsRunning = false;
        }
    }
}

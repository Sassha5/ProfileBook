using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ProfileBook.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            profilesList.ItemsSource = App.ProfilesDatabase.GetItems().Where(x => x.UserId == App.currentUser.Id);
            base.OnAppearing();
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Application.Current.MainPage.DisplayAlert("hi", "hello", "cancel");
            //Profile selectedProfile = (Profile)e.SelectedItem;
            //ProfilePage ProfilePage = new ProfilePage();
            //ProfilePage.BindingContext = selectedProfile;
            //await Navigation.PushAsync(ProfilePage);
        }
    }
}

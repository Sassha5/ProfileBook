using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        public ICommand CreateProfile => new Command<string>(async (url) =>
        {
            //await Application.Current.MainPage.DisplayAlert("hi", "hello", "cancel");
            //model.Authorize(Login, Password)
            await NavigationService.NavigateAsync("CreateProfile");
        });

        //private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    //Profile selectedProfile = (Profile)e.SelectedItem;
        //    //ProfilePage ProfilePage = new ProfilePage();
        //    //ProfilePage.BindingContext = selectedProfile;
        //    //await Navigation.PushAsync(ProfilePage);
        //}
    }
}

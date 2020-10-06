using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class CreateProfileViewModel : ViewModelBase
    {
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public CreateProfileViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "New Profile Page";
        }

        public ICommand Add => new Command<string>(async (url) =>
        {
            App.database.SaveItem(new Profile
            {
                Nickname = this.Nickname,
                Name = this.Name,
                Description = this.Description
            });
            await NavigationService.NavigateAsync("MainPage");
        });
    }
}

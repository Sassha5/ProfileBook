using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.ProfileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class CreateProfileViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;

        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public CreateProfileViewModel(INavigationService navigationService,
            IProfileService profileService)
            : base(navigationService)
        {
            _profileService = profileService;
            Title = "New Profile";
        }

        public ICommand Add => new Command(async () =>
        {
            _profileService.SaveProfile(new Profile
            {
                Nickname = this.Nickname,
                Name = this.Name,
                Description = this.Description,
                UserId = App.currentUser.Id
            });
            await NavigationService.NavigateAsync("/NavigationPage/MainPage");
        });
    }
}

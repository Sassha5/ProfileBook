using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using ProfileBook.Models;
using System;
using System.IO;

namespace ProfileBook
{
    public partial class App
    {
        public const string DATABASE_NAME = "profiles.db";
        public static ProfilesRepository database;
        public static ProfilesRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new ProfilesRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/SignIn");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SignUpViewModel>();
        }
    }
}

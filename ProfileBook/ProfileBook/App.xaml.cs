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
using System.Data;

namespace ProfileBook
{
    public partial class App
    {
        public const string DATABASE_NAME = "users.db";
        public const string PROFILES_DATABASE_NAME = "profiles.db";
        public static string DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static UsersRepository database;
        private static ProfilesRepository profilesDatabase;
        public static User currentUser;
        public static Client client = new Client();
        public static UsersRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new UsersRepository(Path.Combine(DATABASE_PATH, DATABASE_NAME));
                }
                return database;
            }
        }
        public static ProfilesRepository ProfilesDatabase
        {
            get
            {
                if (profilesDatabase == null)
                {
                    profilesDatabase = new ProfilesRepository(Path.Combine(DATABASE_PATH, PROFILES_DATABASE_NAME));
                }
                return profilesDatabase;
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
            containerRegistry.RegisterForNavigation<CreateProfile, CreateProfileViewModel>();
            containerRegistry.RegisterForNavigation<Settings, SettingsViewModel>();
        }
    }
}

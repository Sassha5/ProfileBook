using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Registration;
using ProfileBook.Services.Repository;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Settings;
using Plugin.Settings;
using ProfileBook.Models;
using Acr.UserDialogs;

namespace ProfileBook
{
    public partial class App
    {
        private ISettingsManager _settingsManager;
        private ISettingsManager SettingsManager =>
            _settingsManager ??= Container.Resolve<ISettingsManager>();
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            Device.SetFlags(new string[] { "AppTheme_Experimental", "RadioButton_Experimental" });

            InitializeComponent();

            Application.Current.UserAppTheme = (OSAppTheme)SettingsManager.Theme; //setting theme to previous session theme

            string path = nameof(SignIn);
            if (SettingsManager.AuthorizedUserID != Constants.NoAuthorizedUser)
            {
                path = nameof(MainPage);
            }
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{path}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateProfile, CreateProfileViewModel>();
            containerRegistry.RegisterForNavigation<Settings, SettingsViewModel>();

            containerRegistry.RegisterInstance(CrossSettings.Current);
            containerRegistry.RegisterInstance(UserDialogs.Instance);

            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepository<User>>(Container.Resolve<Repository<User>>());
            containerRegistry.RegisterInstance<IRepository<Profile>>(Container.Resolve<Repository<Profile>>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IRegistrationService>(Container.Resolve<RegistrationService>());
        }
    }
}

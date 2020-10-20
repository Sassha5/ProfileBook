using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Localization;
using ProfileBook.Resources;
using ProfileBook.Services.Settings;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProfileBook.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigatedAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected ISettingsManager SettingsManager { get; private set; }
        public LocalizedResources Resources
        {
            get;
            private set;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService, ISettingsManager settingsManager)
        {
            SettingsManager = settingsManager;
            NavigationService = navigationService;
            Resources = new LocalizedResources(typeof(AppResource), SettingsManager.Language);
        }

        public virtual void Initialize(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        public virtual void Destroy() { }

        public new void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}

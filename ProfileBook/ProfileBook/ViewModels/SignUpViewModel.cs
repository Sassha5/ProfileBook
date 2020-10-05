using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        public string Login { get; set; }
        public string FirstPassword { get; set; }
        public string SecondPassword { get; set; }


        public SignUpViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }
    }
}

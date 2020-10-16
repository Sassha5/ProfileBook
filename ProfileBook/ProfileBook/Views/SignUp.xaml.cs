using Xamarin.Forms;

namespace ProfileBook.Views
{
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
            signUpButton.IsEnabled = false;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            signUpButton.IsEnabled = loginInput.Text != null && 
                                     passwordFirstInput.Text != null && 
                                     passwordSecondInput.Text != null;
        }
    }
}

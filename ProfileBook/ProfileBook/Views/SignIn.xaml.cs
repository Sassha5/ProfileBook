using Xamarin.Forms;

namespace ProfileBook.Views
{
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent();
            signInButton.IsEnabled = false;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            signInButton.IsEnabled = loginInput.Text != null && passwordInput.Text != null;
        }
    }
}

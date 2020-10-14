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
            if (loginInput.Text != null && passwordInput.Text != null)
            {
                if (loginInput.Text.Length > 0 && passwordInput.Text.Length > 0)
                {
                    signInButton.IsEnabled = true;
                }
                else signInButton.IsEnabled = false;
            }
            else signInButton.IsEnabled = false;
        }
    }
}

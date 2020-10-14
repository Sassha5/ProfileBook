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
            if (loginInput.Text != null && passwordFirstInput.Text != null && passwordSecondInput.Text != null)
            {
                if (loginInput.Text.Length > 0 && passwordFirstInput.Text.Length > 0 && passwordSecondInput.Text.Length > 0)
                {
                    signUpButton.IsEnabled = true;
                }
                else signUpButton.IsEnabled = false;
            }
            else signUpButton.IsEnabled = false;
        }
    }
}

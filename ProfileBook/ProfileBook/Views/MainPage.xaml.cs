using ProfileBook.Models;
using System;
using Xamarin.Forms;

namespace ProfileBook.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            profilesList.ItemsSource = App.Database.GetItems();
            base.OnAppearing();
        }
    }
}

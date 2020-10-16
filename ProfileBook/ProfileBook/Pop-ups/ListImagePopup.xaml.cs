using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.Pop_ups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListImagePopup : PopupPage
    {
        public ListImagePopup(string imagePath)
        {
            InitializeComponent();
            ImageView.Source = imagePath;
        }

    }
}
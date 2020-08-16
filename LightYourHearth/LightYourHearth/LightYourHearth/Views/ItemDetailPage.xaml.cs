using System.ComponentModel;
using Xamarin.Forms;
using LightYourHearth.ViewModels;

namespace LightYourHearth.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
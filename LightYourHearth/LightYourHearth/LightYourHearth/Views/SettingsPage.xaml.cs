using LightYourHearth.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LightYourHearth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel vm;

        public SettingsPage()
        {
            InitializeComponent();
            vm = new SettingsViewModel();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.ExecuteLoadItemsCommand();
        }
    }
}
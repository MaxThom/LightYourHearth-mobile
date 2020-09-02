using DLToolkit.Forms.Controls;

using LightYourHearth.ViewModels;

using Xamarin.Forms;

namespace LightYourHearth.Views
{
    public partial class AnimationPage : ContentPage
    {
        private AnimationViewModel vm;

        public AnimationPage()
        {
            InitializeComponent();
            FlowListView.Init();
            vm = new AnimationViewModel();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = vm.ConnectToLastDeviceAsync();
        }
    }
}
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
            vm = new AnimationViewModel();
            BindingContext = vm;
        }
    }
}
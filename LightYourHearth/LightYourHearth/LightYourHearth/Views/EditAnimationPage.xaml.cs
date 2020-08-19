using LightYourHearth.ViewModels;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LightYourHearth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("Animation", "animation")]
    public partial class EditAnimationPage : ContentPage
    {
        private string animation;

        public string Animation
        {
            get => animation;
            set
            {
                animation = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                vm.Animation = animation;
            }
        }

        private EditAnimationViewModel vm;

        public EditAnimationPage()
        {
            InitializeComponent();
            vm = new EditAnimationViewModel();
            BindingContext = vm;
        }
    }
}
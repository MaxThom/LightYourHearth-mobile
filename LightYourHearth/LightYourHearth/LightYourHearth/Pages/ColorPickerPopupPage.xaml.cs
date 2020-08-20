using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LightYourHearth.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPickerPopupPage : PopupPage
    {
        private Action<Color> _callback;

        public ColorPickerPopupPage(Action<Color> callback)
        {
            InitializeComponent();
            _callback = callback;
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            _callback.Invoke(ColorWheel.SelectedColor);
        }
    }
}
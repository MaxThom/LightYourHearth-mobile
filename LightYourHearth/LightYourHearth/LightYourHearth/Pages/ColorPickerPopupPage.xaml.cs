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

        public ColorPickerPopupPage(Action<Color> callback, Color initialColor)
        {
            InitializeComponent();
            _callback = callback;
            ColorWheel.SelectedColor = new Color(initialColor.R, initialColor.G, initialColor.B, initialColor.A);
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();

            _callback.Invoke(ColorWheel.SelectedColor);
        }
    }
}
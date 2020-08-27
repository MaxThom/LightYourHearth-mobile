using ImageCircle.Forms.Plugin.Abstractions;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LightYourHearth.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPickerPopupPage : PopupPage
    {
        private Action<Color> _callback;

        private List<string> preselectedColors = new List<string>()
        {
            "#FF000000",
            "#00FFFFFF",
            "#FF0000FF",
            "#FFFF0000",
            "#FF00FF00",
            "#FFFFA500",
            "#FFFFFF00",
            "#FF00FFFF",
            "#FFFF00FF",
            "#FF4B0082"
        };

        public ColorPickerPopupPage(Action<Color> callback, Color initialColor)
        {
            InitializeComponent();
            _callback = callback;
            ColorWheel.SelectedColor = new Color(initialColor.R, initialColor.G, initialColor.B, initialColor.A);

            var tapGest = new TapGestureRecognizer();
            tapGest.Tapped += CircleButton_Pressed;

            foreach (var clr in preselectedColors)
            {
                var circle = new CircleImage()
                {
                    HeightRequest = 50,
                    WidthRequest = 50,
                    Aspect = Aspect.AspectFill,
                    BorderColor = Color.Black,
                    BorderThickness = 1,
                    Margin = new Thickness(5),
                    FillColor = Color.FromHex(clr)
                };
                circle.GestureRecognizers.Add(tapGest);
                ColorPickerLayout.Children.Add(circle);
            }
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();

            _callback.Invoke(ColorWheel.SelectedColor);
        }

        public void CircleButton_Pressed(object sender, EventArgs e)
        {
            var picker = (CircleImage)sender;
            ColorWheel.SelectedColor = picker.FillColor;
        }
    }
}
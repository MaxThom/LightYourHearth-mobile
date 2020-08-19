using LightYourHearth.Views;

using Xamarin.Forms;

namespace LightYourHearth
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(BluetoothConfigurationPage), typeof(BluetoothConfigurationPage));
            Routing.RegisterRoute(nameof(LedConfigurationPage), typeof(LedConfigurationPage));
            Routing.RegisterRoute(nameof(EditAnimationPage), typeof(EditAnimationPage));
        }
    }
}
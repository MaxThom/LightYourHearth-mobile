using LightYourHearth.Services;

using Xamarin.Forms;

namespace LightYourHearth
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<SettingsService>();
            DependencyService.Register<IBluetoothComm, BluetoothSPDComm>();
            DependencyService.Register<ServerService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
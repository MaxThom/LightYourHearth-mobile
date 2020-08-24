using LightYourHearth.Models;

using System.Collections.Generic;

using Xamarin.Forms;

namespace LightYourHearth.Services
{
    public class SettingsService
    {
        private IBluetoothComm _bluetoothComm => DependencyService.Get<IBluetoothComm>();

        public BluetoothConfiguration BluetoothConfiguration { get; set; }
        public LedConfiguration LedConfiguration { get; set; }

        public SettingsService()
        {
            _bluetoothComm.OnBluetoothConnected += _bluetoothComm_OnBluetoothConnected;

            LoadConfigurations();
        }

        private void _bluetoothComm_OnBluetoothConnected(object sender, System.EventArgs e)
        {
            BluetoothConfiguration.DeviceName = _bluetoothComm.SelectedDevice.Name;
            BluetoothConfiguration.DeviceMacAddress = _bluetoothComm.SelectedDevice.Address;
        }

        private void LoadConfigurations()
        {
            // Load all settings from local files
            BluetoothConfiguration = new BluetoothConfiguration()
            {
                Id = "0"
            };
            LedConfiguration = new LedConfiguration()
            {
                Id = "1",
                LedType = "WS2812B",
                LedPixelCount = 64
            };
        }

        public List<ConfigurationItem> GetAllSettings()
        {
            var settings = new List<ConfigurationItem>();
            settings.Add(BluetoothConfiguration);
            settings.Add(LedConfiguration);
            return settings;
        }
    }
}
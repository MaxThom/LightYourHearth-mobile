using LightYourHearth.Models;

using System.Collections.Generic;

namespace LightYourHearth.Services
{
    public class SettingsService
    {
        public BluetoothConfiguration BluetoothConfiguration { get; set; }
        public LedConfiguration LedConfiguration { get; set; }

        public SettingsService()
        {
            LoadConfigurations();
        }

        private void LoadConfigurations()
        {
            // Load all settings from local files
            BluetoothConfiguration = new BluetoothConfiguration()
            {
                Id = "0",
                DeviceName = "raspberrypie",
                DeviceMacAddress = "02:XE:34:BD"
            };
            LedConfiguration = new LedConfiguration()
            {
                Id = "1",
                LedType = "WS2812B",
                LedPixelCount = 144
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
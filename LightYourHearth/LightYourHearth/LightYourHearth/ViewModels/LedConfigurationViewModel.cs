using LightYourHearth.Services;

using System.Collections.Generic;

using Xamarin.Forms;

namespace LightYourHearth.ViewModels
{
    public class LedConfigurationViewModel : BaseViewModel
    {
        private IBluetoothComm _bluetoothComm => DependencyService.Get<IBluetoothComm>();
        private SettingsService _settingsService => DependencyService.Get<SettingsService>();

        private string _selectedLedStrip = "SK6812";

        public string SelectedLedStrip
        {
            get => _selectedLedStrip;
            set
            {
                SetProperty(ref _selectedLedStrip, value);
                OnLedStripSelection(value);
            }
        }

        public List<string> LedStripsOption { get; } = new List<string>()
        {
            "SK6812",
            "WS2812B",
            "WS2811"
        };

        private int _selectedLedCount = 300;

        public int SelectedLedCount
        {
            get => _selectedLedCount;
            set
            {
                SetProperty(ref _selectedLedCount, value);
                OnLedCountSelection(value);
            }
        }

        public List<int> LedCountOption { get; } = new List<int>();

        public LedConfigurationViewModel()
        {
            Title = "Led Configuration";

            _selectedLedCount = _settingsService.LedConfiguration.LedPixelCount;
            _selectedLedStrip = _settingsService.LedConfiguration.LedType;

            for (int i = 1; i <= 1000; i++)
                LedCountOption.Add(i);
        }

        private void OnLedStripSelection(string value)
        {
            _settingsService.LedConfiguration.SaveLedType(value, _settingsService.BluetoothConfiguration.DeviceName);
            _bluetoothComm.SendMessageAsync($"Led_Settings:{_settingsService.LedConfiguration.ToConfigurationString()}");
        }

        private void OnLedCountSelection(int value)
        {
            _settingsService.LedConfiguration.SaveLedCount(value, _settingsService.BluetoothConfiguration.DeviceName);
            _bluetoothComm.SendMessageAsync($"Led_Settings:{_settingsService.LedConfiguration.ToConfigurationString()}");
        }
    }
}
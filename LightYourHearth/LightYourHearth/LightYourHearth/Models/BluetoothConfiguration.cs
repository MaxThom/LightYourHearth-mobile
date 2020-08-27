using Xamarin.Essentials;

namespace LightYourHearth.Models
{
    public class BluetoothConfiguration : ConfigurationItem
    {
        public override string Text { get => "Bluetooth"; }
        public override string Description { get => $"{DeviceName} - {DeviceMacAddress}"; }

        private string deviceName;
        public string DeviceName { get => deviceName; set { deviceName = value; SaveToLocalStorage(); } }
        private string deviceMacAddress;
        public string DeviceMacAddress { get => deviceMacAddress; set { deviceMacAddress = value; SaveToLocalStorage(); } }

        public BluetoothConfiguration()
        {
            LoadFromLocalStorage();
        }

        public override void LoadFromLocalStorage()
        {
            deviceName = Preferences.Get(nameof(DeviceName), "");
            deviceMacAddress = Preferences.Get(nameof(DeviceMacAddress), "");
        }

        public override void SaveToLocalStorage()
        {
            Preferences.Set(nameof(DeviceName), DeviceName);
            Preferences.Set(nameof(DeviceMacAddress), DeviceMacAddress);
        }
    }
}
using System;

using Xamarin.Essentials;

namespace LightYourHearth.Models
{
    public class BluetoothConfiguration : ConfigurationItem
    {
        //private SimpleStorage _storage = SimpleStorage.EditGroup(nameof(BluetoothConfiguration));

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
            //deviceName = _storage.Get(nameof(DeviceName)) ?? "";
            //deviceMacAddress = _storage.Get(nameof(DeviceMacAddress)) ?? "";
            deviceName = Preferences.Get(nameof(DeviceName), "");
            deviceMacAddress = Preferences.Get(nameof(DeviceMacAddress), "0");
        }

        public override void SaveToLocalStorage()
        {
            //_storage.Put(nameof(DeviceName), DeviceName);
            //_storage.Put(nameof(DeviceMacAddress), DeviceName);
            Preferences.Set(nameof(DeviceName), DeviceName);
            Preferences.Set(nameof(DeviceMacAddress), DeviceMacAddress);
        }
    }
}
namespace LightYourHearth.Models
{
    public class BluetoothConfiguration : ConfigurationItem
    {
        public override string Text { get => "Bluetooth"; }
        public override string Description { get => $"{DeviceName} - {DeviceMacAddress}"; }

        public string DeviceName { get; set; }
        public string DeviceMacAddress { get; set; }
    }
}
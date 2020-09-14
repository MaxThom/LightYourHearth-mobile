using Xamarin.Essentials;

namespace LightYourHearth.Models
{
    public class LedConfiguration : ConfigurationItem
    {
        public override string Text { get => "Led"; }
        public override string Description { get => $"{LedType} - {LedPixelCount} pixels"; }

        private string ledType { get; set; }
        public string LedType { get => ledType; set => ledType = value; }
        private int ledPixelCount { get; set; }
        public int LedPixelCount { get => ledPixelCount; set => ledPixelCount = value; }

        public string ToConfigurationString() => $"led_type={LedType},led_pixel_count={LedPixelCount}";

        public LedConfiguration()
        {
        }

        public void SaveLedType(string ledType, string bluetoothName)
        {
            LedType = ledType;
            Preferences.Set($"{bluetoothName}_{nameof(LedType)}", LedType);
            Preferences.Set($"{bluetoothName}_{nameof(LedPixelCount)}", LedPixelCount);
        }

        public void SaveLedCount(int ledCount, string bluetoothName)
        {
            LedPixelCount = ledCount;
            Preferences.Set($"{bluetoothName}_{nameof(LedType)}", LedType);
            Preferences.Set($"{bluetoothName}_{nameof(LedPixelCount)}", LedPixelCount);
        }

        public void LoadFromLocalStorage(string bluetoothName)
        {
            ledType = Preferences.Get($"{bluetoothName}_{nameof(LedType)}", "");
            ledPixelCount = Preferences.Get($"{bluetoothName}_{nameof(LedPixelCount)}", 0);
        }

        public override void LoadFromLocalStorage()
        {
            ledType = Preferences.Get(nameof(LedType), "");
            ledPixelCount = Preferences.Get(nameof(LedPixelCount), 0);
        }

        public override void SaveToLocalStorage()
        {
            Preferences.Set(nameof(LedType), LedType);
            Preferences.Set(nameof(LedPixelCount), LedPixelCount);
        }
    }
}
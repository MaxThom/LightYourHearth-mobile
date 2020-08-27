using Xamarin.Essentials;

namespace LightYourHearth.Models
{
    public class LedConfiguration : ConfigurationItem
    {
        public override string Text { get => "Led"; }
        public override string Description { get => $"{LedType} - {LedPixelCount} pixels"; }

        private string ledType { get; set; }
        public string LedType { get => ledType; set { ledType = value; SaveToLocalStorage(); } }
        private int ledPixelCount { get; set; }
        public int LedPixelCount { get => ledPixelCount; set { ledPixelCount = value; SaveToLocalStorage(); } }

        public string ToConfigurationString() => $"led_type={LedType},led_pixel_count={LedPixelCount}";

        public LedConfiguration()
        {
            LoadFromLocalStorage();
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
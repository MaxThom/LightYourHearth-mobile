using Android.App;

using Xamarin.Essentials;

namespace LightYourHearth.Models
{
    public class LedConfiguration : ConfigurationItem
    {
        //private SimpleStorage _storage = SimpleStorage.EditGroup(nameof(LedConfiguration));

        public override string Text { get => "Led"; }
        public override string Description { get => $"{LedType} - {LedPixelCount} pixels"; }

        public string ledType { get; set; }
        public string LedType { get => ledType; set { ledType = value; SaveToLocalStorage(); } }
        public int ledPixelCount { get; set; }
        public int LedPixelCount { get => LedPixelCount; set { LedPixelCount = value; SaveToLocalStorage(); } }

        public string ToConfigurationString() => $"led_type={LedType},led_pixel_count={LedPixelCount}";

        public LedConfiguration()
        {
            LoadFromLocalStorage();
        }

        public override void LoadFromLocalStorage()
        {
            //ledType = _storage.Get(nameof(LedType)) ?? "";
            //ledPixelCount = int.Parse(_storage.Get(nameof(LedPixelCount)) ?? "0");
            ledType = Preferences.Get(nameof(LedType), "0");
            ledPixelCount = Preferences.Get(nameof(LedPixelCount), 0);
        }

        public override void SaveToLocalStorage()
        {
            //_storage.Put(nameof(LedType), LedType);
            //_storage.Put(nameof(LedPixelCount), LedPixelCount);
            Preferences.Set(nameof(LedType), LedType);
            Preferences.Set(nameof(LedPixelCount), LedPixelCount);
        }
    }
}
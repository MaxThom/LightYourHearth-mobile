namespace LightYourHearth.Models
{
    public class LedConfiguration : ConfigurationItem
    {
        public override string Text { get => "Led"; }
        public override string Description { get => $"{LedType} - {LedPixelCount} pixels"; }

        public string LedType { get; set; }
        public int LedPixelCount { get; set; }
    }
}
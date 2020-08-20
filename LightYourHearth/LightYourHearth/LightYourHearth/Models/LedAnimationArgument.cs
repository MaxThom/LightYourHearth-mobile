using System.Linq;

namespace LightYourHearth.Models
{
    public class LedAnimationArgument
    {
        public string Name { get; set; }
        public LedAnimationArgumentType Type { get; set; }
        public string DefaultValue { get; set; }
        public string Value { get; set; } = string.Empty;

        public string MaxValue { get; set; }
        public string MinValue { get; set; }

        public string GetDisplayName() => Name.First().ToString().ToUpper() + Name.Substring(1).Replace("_", " ");
    }
}
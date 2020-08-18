using System.Collections.Generic;
using System.Linq;

namespace LightYourHearth.Models
{
    public class LedAnimation
    {
        public string Name { get; set; }

        public List<LedAnimationArgument> Arguments { get; set; }

        public string DisplayName
        {
            get
            {
                var name = Name.Substring(4).Replace("_", " ").ToLower();
                name = name.First().ToString().ToUpper() + name.Substring(1);
                return name;
            }
        }
    }
}
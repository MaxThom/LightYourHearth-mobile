﻿using System.Collections.Generic;
using System.Linq;

namespace LightYourHearth.Models
{
    public class LedAnimation
    {
        public string Name { get; set; }

        public List<LedAnimationArgument> Arguments { get; set; } = new List<LedAnimationArgument>();

        public string DisplayName
        {
            get
            {
                var name = Name.Substring(4).Replace("_", " ").ToLower();
                name = name.First().ToString().ToUpper() + name.Substring(1);
                return name;
            }
        }

        public bool HasArguments { get => Arguments != null && Arguments.Any(); }

        public override string ToString()
        {
            var argStr = string.Empty;
            foreach (var arg in Arguments)
            {
                argStr += $"\n\t{arg.Name} - ";
                argStr += arg.Value == string.Empty ? arg.DefaultValue : arg.Value;
            }
            return $"{Name}:{argStr}";
        }

        public string ToCommandString()
        {
            return $"{Name}:{string.Join(",", Arguments.Select(x => x.ToCommandString()))}";
        }
    }
}
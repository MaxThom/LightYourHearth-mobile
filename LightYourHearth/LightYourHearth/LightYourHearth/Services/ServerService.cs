using LightYourHearth.Models;

using System.Collections.Generic;

using Xamarin.Forms;

namespace LightYourHearth.Services
{
    public class ServerService
    {
        private IBluetoothComm _bluetoothComm => DependencyService.Get<IBluetoothComm>();
        private SettingsService _settingsService => DependencyService.Get<SettingsService>();

        public List<LedAnimation> AnimationCapabilities { get; set; } = new List<LedAnimation>();

        public ServerService()
        {
            _bluetoothComm.OnMessageReceived += _bluetoothComm_OnMessageReceived;
            AnimationCapabilities.AddRange(FetchLedAnimationsCapabilities());
        }

        private void _bluetoothComm_OnMessageReceived(object sender, string e)
        {
        }

        private List<LedAnimation> FetchLedAnimationsCapabilities() =>
            new List<LedAnimation>()
            {
                new LedAnimation()
                {
                    Name = "Led_Off",
                    Arguments = null
                },
                new LedAnimation()
                {
                    Name = "Led_Rainbow_Color",
                    Arguments = new List<LedAnimationArgument>()
                    {
                        new LedAnimationArgument()
                        {
                            Name = "wait",
                            Type = "double",
                            DefaultValue = "0.05"
                        }
                    }
                },
                new LedAnimation()
                {
                    Name = "Led_Rainbow_Cycle",
                    Arguments = new List<LedAnimationArgument>()
                    {
                        new LedAnimationArgument()
                        {
                            Name = "wait",
                            Type = "double",
                            DefaultValue = "0.005"
                        }
                    }
                },
                new LedAnimation()
                {
                    Name = "Led_Rainbow_Cycle_Successive",
                    Arguments = new List<LedAnimationArgument>()
                    {
                        new LedAnimationArgument()
                        {
                            Name = "wait",
                            Type = "double",
                            DefaultValue = "0.1"
                        }
                    }
                },
                new LedAnimation()
                {
                    Name = "Led_Brightness_Decrease",
                    Arguments = new List<LedAnimationArgument>()
                    {
                        new LedAnimationArgument()
                        {
                            Name = "wait",
                            Type = "double",
                            DefaultValue = "0.01"
                        },
                        new LedAnimationArgument()
                        {
                            Name = "step",
                            Type = "int",
                            DefaultValue = "1"
                        }
                    }
                },
                new LedAnimation()
                {
                    Name = "Led_Blink_Color",
                    Arguments = new List<LedAnimationArgument>()
                    {
                        new LedAnimationArgument()
                        {
                            Name = "wait",
                            Type = "double",
                            DefaultValue = "0.5"
                        },
                        new LedAnimationArgument()
                        {
                            Name = "blink_times",
                            Type = "int",
                            DefaultValue = "5"
                        },
                        new LedAnimationArgument()
                        {
                            Name = "color",
                            Type = "Color",
                            DefaultValue = "(255, 255, 255)"
                        }
                    }
                },
                new LedAnimation()
                {
                    Name = "Led_Appear_From_Back",
                    Arguments = new List<LedAnimationArgument>()
                    {
                        new LedAnimationArgument()
                        {
                            Name = "color",
                            Type = "Color",
                            DefaultValue = "(255, 255, 255)"
                        }
                    }
                }
            };
    }
}
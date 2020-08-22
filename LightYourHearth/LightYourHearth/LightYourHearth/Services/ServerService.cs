using LightYourHearth.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LightYourHearth.Services
{
    public class ServerService
    {
        private IBluetoothComm _bluetoothComm => DependencyService.Get<IBluetoothComm>();
        private SettingsService _settingsService => DependencyService.Get<SettingsService>();

        public List<LedAnimation> AnimationCapabilities { get; set; } = new List<LedAnimation>();

        public event EventHandler<LedAnimation> OnNewAnimationDiscovered;

        public ServerService()
        {
            _bluetoothComm.OnBluetoothConnected += _bluetoothComm_OnBluetoothConnected;
            _bluetoothComm.OnBluetoothDisconnected += _bluetoothComm_OnBluetoothDisconnected;
            _bluetoothComm.OnMessageReceived += _bluetoothComm_OnMessageReceived;
            //AnimationCapabilities.AddRange(FetchLedAnimationsCapabilities());
        }

        private void _bluetoothComm_OnBluetoothDisconnected(object sender, EventArgs e)
        {
            AnimationCapabilities?.Clear();
        }

        private async void _bluetoothComm_OnBluetoothConnected(object sender, EventArgs e)
        {
            // 1. Send led settings
            _bluetoothComm.SendMessageAsync($"Led_Settings:{_settingsService.LedConfiguration.ToConfigurationString()}");
            await Task.Delay(100);
            // 2. Ask for animation capabilities
            _bluetoothComm.SendMessageAsync($"Led_Animation_Capabilities");
        }

        private void _bluetoothComm_OnMessageReceived(object sender, string e)
        {
            string pattern = @"'([\w]*)':([\w' #.]*)";
            Regex rg = new Regex(pattern);

            var cap = e.Split(new char[] { ':' }, 3);
            if (cap[0].Equals("Cap"))
            {
                var name = cap[1];
                var args = cap[2];

                var newAnim = new LedAnimation
                {
                    Name = name
                };
                LedAnimationArgument newArg = null;
                foreach (Match match in rg.Matches(args))
                {
                    Console.WriteLine(match);

                    if (string.IsNullOrWhiteSpace(match.Groups[2].Value))
                    {
                        if (newArg != null)
                            newAnim.Arguments.Add(newArg);
                        newArg = new LedAnimationArgument()
                        {
                            Name = match.Groups[1].Value
                        };
                    }
                    else
                    {
                        var value = match.Groups[2].Value.Replace("\'", "").Trim();
                        switch (match.Groups[1].Value)
                        {
                            case "default_value":
                                newArg.DefaultValue = value;
                                break;

                            case "max_value":
                                newArg.MaxValue = value;
                                break;

                            case "min_value":
                                newArg.MinValue = value;
                                break;

                            case "type":
                                newArg.Type = (LedAnimationArgumentType)Enum.Parse(typeof(LedAnimationArgumentType), value, true);
                                break;
                        }
                    }
                }
                if (newArg != null)
                    newAnim.Arguments.Add(newArg);
                AnimationCapabilities.Add(newAnim);
                OnNewAnimationDiscovered?.Invoke(this, AnimationCapabilities.Last());
            }
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
                            Type = LedAnimationArgumentType.Double,
                            DefaultValue = "0.05",
                            MinValue="0",
                            MaxValue="1"
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
                            Type = LedAnimationArgumentType.Double,
                            DefaultValue = "0.005",
                            MinValue="0",
                            MaxValue="1"
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
                            Type = LedAnimationArgumentType.Double,
                            DefaultValue = "0.1",
                            MinValue="0",
                            MaxValue="1"
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
                            Type = LedAnimationArgumentType.Double,
                            DefaultValue = "0.01",
                            MinValue="0",
                            MaxValue="1"
                        },
                        new LedAnimationArgument()
                        {
                            Name = "step",
                            Type = LedAnimationArgumentType.Int,
                            DefaultValue = "1",
                            MinValue="0",
                            MaxValue="10"
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
                            Type = LedAnimationArgumentType.Double,
                            DefaultValue = "0.5",
                            MinValue="0",
                            MaxValue="1"
                        },
                        new LedAnimationArgument()
                        {
                            Name = "blink_times",
                            Type = LedAnimationArgumentType.Int,
                            DefaultValue = "5",
                            MinValue="0",
                            MaxValue="100"
                        },
                        new LedAnimationArgument()
                        {
                            Name = "color",
                            Type = LedAnimationArgumentType.Color,
                            DefaultValue = "#1E90FF"
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
                            Type = LedAnimationArgumentType.Color,
                            DefaultValue = "#1E90FF"
                        }
                    }
                }
            };
    }
}
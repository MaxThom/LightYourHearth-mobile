﻿using LightYourHearth.Models;

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
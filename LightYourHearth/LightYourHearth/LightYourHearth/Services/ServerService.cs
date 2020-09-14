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
        }

        private void _bluetoothComm_OnBluetoothDisconnected(object sender, EventArgs e)
        {
            AnimationCapabilities?.Clear();
        }

        private async void _bluetoothComm_OnBluetoothConnected(object sender, string deviceName)
        {
            // 1. Send led settings
            _settingsService.LedConfiguration.LoadFromLocalStorage(deviceName);
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
                            newAnim.AddAndLoadArgument(newArg);
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
                    newAnim.AddAndLoadArgument(newArg);
                AnimationCapabilities.Add(newAnim);
                OnNewAnimationDiscovered?.Invoke(this, AnimationCapabilities.Last());
            }
        }
    }
}
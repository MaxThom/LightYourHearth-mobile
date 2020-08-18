using LightYourHearth.Models;
using LightYourHearth.Services;

using Plugin.Toast;
using Plugin.Toast.Abstractions;

using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace LightYourHearth.ViewModels
{
    public class AnimationViewModel : BaseViewModel
    {
        private IBluetoothComm _bluetoothComm => DependencyService.Get<IBluetoothComm>();
        private SettingsService _settingsService => DependencyService.Get<SettingsService>();
        private ServerService _serverService => DependencyService.Get<ServerService>();

        public ObservableCollection<LedAnimation> AnimationList { get; }

        public Command<LedAnimation> AnimationCommand { get; }

        public AnimationViewModel()
        {
            Title = "Animation";
            AnimationList = new ObservableCollection<LedAnimation>();
            AnimationCommand = new Command<LedAnimation>(OnLedAnimationTap);

            _serverService.AnimationCapabilities.ForEach(x => AnimationList.Add(x));
        }

        private void OnLedAnimationTap(LedAnimation animation)
        {
            if (animation == null)
                return;

            Console.WriteLine(animation.Name);
            if (_bluetoothComm.IsDeviceListening)
            {
                _bluetoothComm.SendMessageAsync(animation.Name);
                CrossToastPopUp.Current.ShowToastMessage(animation.DisplayName, ToastLength.Short);
            }
            else
                CrossToastPopUp.Current.ShowToastMessage("Device not connected.", ToastLength.Short);

        }
    }
}
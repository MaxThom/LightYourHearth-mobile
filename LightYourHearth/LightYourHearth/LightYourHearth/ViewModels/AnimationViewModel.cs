using LightYourHearth.Models;
using LightYourHearth.Services;
using LightYourHearth.Views;

using Plugin.Toast;
using Plugin.Toast.Abstractions;

using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;

namespace LightYourHearth.ViewModels
{
    public class AnimationViewModel : BaseViewModel
    {
        private IBluetoothComm _bluetoothComm => DependencyService.Get<IBluetoothComm>();
        private SettingsService _settingsService => DependencyService.Get<SettingsService>();
        private ServerService _serverService => DependencyService.Get<ServerService>();

        public ObservableCollection<LedAnimation> AnimationList { get; } = new ObservableCollection<LedAnimation>();

        public Command<LedAnimation> AnimationCommand { get; }
        public Command<LedAnimation> AnimationEditCommand { get; }

        private int _brightness = 127;

        public int Brightness
        {
            get => _brightness;
            set
            {
                SetProperty(ref _brightness, value);
                OnBrightnessValueChanged(value);
            }
        }

        public AnimationViewModel()
        {
            Title = "Animation";
            AnimationCommand = new Command<LedAnimation>(OnLedAnimationTap);
            AnimationEditCommand = new Command<LedAnimation>(OnLedAnimationEditTap, (x) => x.HasArguments);

            //_serverService.AnimationCapabilities.ForEach(x => AnimationList.Add(x));

            _bluetoothComm.OnBluetoothDisconnected += _bluetoothComm_OnBluetoothDisconnected;
            _serverService.OnNewAnimationDiscovered += _serverService_OnNewAnimationDiscovered;

            var device = _bluetoothComm.GetPairedDevices().Where(x => x.Address.Equals(_settingsService.BluetoothConfiguration.DeviceMacAddress)).FirstOrDefault();
            if (device != null)
            {
                Device.BeginInvokeOnMainThread(() => CrossToastPopUp.Current.ShowToastMessage($"Connecting to {device.Name}...", ToastLength.Long));
                _bluetoothComm.CreateBluetoothConnectionAsync(device);
            }
        }

        private void _bluetoothComm_OnBluetoothDisconnected(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => AnimationList.Clear());
        }

        private void _serverService_OnNewAnimationDiscovered(object sender, LedAnimation e)
        {
            Device.BeginInvokeOnMainThread(() => AnimationList.Add(e));
        }

        private void OnLedAnimationTap(LedAnimation animation)
        {
            if (animation == null)
                return;

            Console.WriteLine($"VM:{animation}");
            if (_bluetoothComm.IsDeviceListening)
            {
                var ledAnimation = _serverService.AnimationCapabilities.Where(x => x.Name.Equals(animation.Name)).FirstOrDefault();
                _bluetoothComm.SendMessageAsync(animation.ToCommandString());
                CrossToastPopUp.Current.ShowToastMessage(animation.DisplayName, ToastLength.Short);
            }
            else
                CrossToastPopUp.Current.ShowToastMessage("Device not connected.", ToastLength.Short);
        }

        public async void OnLedAnimationEditTap(LedAnimation animation)
        {
            await Shell.Current.GoToAsync($"{nameof(EditAnimationPage)}?animation={animation.Name}");
        }

        private void OnBrightnessValueChanged(int value)
        {
            Console.WriteLine("Brightness : " + value);
            if (_bluetoothComm.IsDeviceListening)
            {
                _bluetoothComm.SendMessageAsync($"Led_Set_Brightness:brightness={value}");
            }
        }
    }
}
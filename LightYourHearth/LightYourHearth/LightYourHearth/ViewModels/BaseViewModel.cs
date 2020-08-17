﻿using LightYourHearth.Services;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LightYourHearth.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private IBluetoothComm _bluetoothComm => DependencyService.Get<IBluetoothComm>();
        private SettingsService _settingsService => DependencyService.Get<SettingsService>();
        public Command BluetoothConnectionIconCommand { get; private set; }

        public BaseViewModel()
        {
            BluetoothConnectionIcon = _bluetoothComm.SelectedDevice != null ? "plug_in.png" : "plug_out.png";
            _bluetoothComm.OnBluetoothConnected += (object sender, EventArgs e) => BluetoothConnectionIcon = "plug_in.png";
            _bluetoothComm.OnBluetoothDisconnected += (object sender, EventArgs e) => BluetoothConnectionIcon = "plug_out.png";
            BluetoothConnectionIconCommand = new Command(async () => await ConnectToLastDeviceAsync());
        }

        #region Properties

        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string bluetoothConnectionIcon = "plug_out.png";

        public string BluetoothConnectionIcon
        {
            get { return bluetoothConnectionIcon; }
            set { SetProperty(ref bluetoothConnectionIcon, value); }
        }

        #endregion Properties

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged

        private async Task ConnectToLastDeviceAsync()
        {
            if (BluetoothConnectionIcon.Equals("plug_out.png"))
            {
                var device = _bluetoothComm.GetPairedDevices().Where(x => x.Address.Equals(_settingsService.BluetoothConfiguration.DeviceMacAddress)).FirstOrDefault();
                if (device != null)
                    await _bluetoothComm.CreateBluetoothConnectionAsync(device);
            }
            else if (BluetoothConnectionIcon.Equals("plug_in.png"))
            {
                await _bluetoothComm.CloseBluetoothConnectionAsync();
            }
        }

        public void Dispose()
        {
            _bluetoothComm.OnBluetoothConnected -= (object sender, EventArgs e) => BluetoothConnectionIcon = "plug_in.png";
            _bluetoothComm.OnBluetoothDisconnected -= (object sender, EventArgs e) => BluetoothConnectionIcon = "plug_out.png";
        }
    }
}
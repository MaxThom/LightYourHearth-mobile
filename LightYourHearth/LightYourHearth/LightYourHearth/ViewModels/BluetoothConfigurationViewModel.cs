using Android.Bluetooth;

using LightYourHearth.Services;

using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace LightYourHearth.ViewModels
{
    public class BluetoothConfigurationViewModel : BaseViewModel
    {
        private IBluetoothComm _bluetoothComm => DependencyService.Get<IBluetoothComm>();

        public ObservableCollection<BluetoothDeviceDisplay> BluetoothDevices { get; }
        public Command LoadItemsCommand { get; }
        public Command<BluetoothDeviceDisplay> ItemTapped { get; }
        public BluetoothDeviceDisplay selectedDevice;

        public BluetoothConfigurationViewModel()
        {
            Title = "Bluetooth Configuration";
            BluetoothDevices = new ObservableCollection<BluetoothDeviceDisplay>();

            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());
            ItemTapped = new Command<BluetoothDeviceDisplay>(OnDeviceSelectedAsync);

            _bluetoothComm.OnBluetoothConnected += (object sender, EventArgs e) => ExecuteLoadItemsCommand();
            _bluetoothComm.OnBluetoothDisconnected += (object sender, EventArgs e) => ExecuteLoadItemsCommand();

            ExecuteLoadItemsCommand();
        }

        public BluetoothDeviceDisplay SelectedDevice
        {
            get => selectedDevice;
            set
            {
                SetProperty(ref selectedDevice, value);
                OnDeviceSelectedAsync(value);
            }
        }

        private async void OnDeviceSelectedAsync(BluetoothDeviceDisplay item)
        {
            if (item == null)
                return;

            Console.WriteLine($"{item.BluetoothDevice.Name}__{item.BluetoothDevice.Address}__{item.BluetoothDevice.BondState}");

            await _bluetoothComm.CreateBluetoothConnectionAsync(item.BluetoothDevice);
            ExecuteLoadItemsCommand();
        }

        private void ExecuteLoadItemsCommand()
        {
            try
            {
                IsBusy = true;
                BluetoothDevices.Clear();
                foreach (var device in _bluetoothComm.GetPairedDevices())
                    BluetoothDevices.Add(new BluetoothDeviceDisplay()
                    {
                        BluetoothDevice = device,
                        IsConnected = _bluetoothComm.SelectedDevice != null && _bluetoothComm.SelectedDevice.Address.Equals(device.Address)
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

    public class BluetoothDeviceDisplay
    {
        public bool IsConnected { get; set; }

        public BluetoothDevice BluetoothDevice { get; set; }
    }
}
using Android.Bluetooth;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightYourHearth.Services
{
    public interface IBluetoothComm
    {
        bool IsDeviceListening { get; }
        BluetoothDevice SelectedDevice { get; set; }

        event EventHandler<string> OnMessageReceived;

        Task<bool> CreateBluetoothConnectionAsync(BluetoothDevice device);

        List<BluetoothDevice> GetPairedDevices();

        void SendMessageAsync(string message);

        Task StopBluetoothConnectionAsync();

        void print();
    }
}
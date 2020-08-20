using Android.Bluetooth;
using Android.Runtime;

using Java.Util;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightYourHearth.Services
{
    public class BluetoothSPDComm : IBluetoothComm
    {
        // https://stackoverflow.com/questions/4032391/android-bluetooth-where-can-i-get-uuid
        // This uuid target SPD (Serial Port Profile) Bluetooth Profile
        private const string TARGET_UUID = "00001101-0000-1000-8000-00805F9B34FB";

        private BluetoothSocket socket = null;
        private OutputStreamInvoker outStream = null;
        private InputStreamInvoker inStream = null;
        private Task bluetoothListen = null;

        public BluetoothDevice SelectedDevice { get; set; }
        public bool IsDeviceListening { get; private set; }

        public event EventHandler<string> OnMessageReceived;

        public event EventHandler OnBluetoothConnected;

        public event EventHandler OnBluetoothDisconnected;

        public BluetoothSPDComm()
        {
            test++;
        }

        private static int test = 0;

        public void print()
        {
            Console.WriteLine(test);
        }

        public List<BluetoothDevice> GetPairedDevices()
        {
            if (BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled)
                return BluetoothAdapter.DefaultAdapter.BondedDevices.ToList();

            return new List<BluetoothDevice>();
        }

        public async Task<bool> CreateBluetoothConnectionAsync(BluetoothDevice device)
        {
            try
            {
                socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString(TARGET_UUID));
                await socket.ConnectAsync();

                if (socket != null && socket.IsConnected)
                {
                    SelectedDevice = device;
                    Console.WriteLine("Connection successful!");
                    OnBluetoothConnected?.Invoke(this, null);

                    inStream = (InputStreamInvoker)socket.InputStream;
                    outStream = (OutputStreamInvoker)socket.OutputStream;
                    bluetoothListen = new Task(() => ListenAsync());
                    bluetoothListen.Start();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.WriteLine("Connection failed!");
            SelectedDevice = null;
            IsDeviceListening = false;
            socket.Close();
            OnBluetoothDisconnected?.Invoke(this, null);

            return false;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        public async Task CloseBluetoothConnectionAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            IsDeviceListening = false;
            bluetoothListen.Wait();
            bluetoothListen?.Dispose();
            inStream?.Close();
            outStream?.Close();
            socket?.Close();
            SelectedDevice = null;
            Console.WriteLine("Bluetooth connection closed!");
            OnBluetoothDisconnected?.Invoke(this, null);
        }

        public async void SendMessageAsync(string message)
        {
            try
            {
                uint messageLength = (uint)message.Length;
                byte[] buffer = Encoding.UTF8.GetBytes(message);

                await outStream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private async void ListenAsync()
        {
            IsDeviceListening = true;
            byte[] textBuffer;
            Console.WriteLine("Listening has been started.");

            while (IsDeviceListening)
            {
                try
                {
                    textBuffer = new byte[1024];
                    await inStream.ReadAsync(textBuffer, 0, (int)1024);

                    string s = Encoding.UTF8.GetString(textBuffer);
                    Console.WriteLine("Received: " + s);
                    OnMessageReceived?.Invoke(this, s);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    IsDeviceListening = false;
                    break;
                }
            }
            Console.WriteLine("Listening has ended.");
        }
    }
}
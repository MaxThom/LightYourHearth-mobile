using LightYourHearth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LightYourHearth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BluetoothConfigurationPage : ContentPage
    {
        BluetoothConfigurationViewModel vm = new BluetoothConfigurationViewModel();

        public BluetoothConfigurationPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
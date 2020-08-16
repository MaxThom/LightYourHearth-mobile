using LightYourHearth.Models;
using LightYourHearth.Services;
using LightYourHearth.Views;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LightYourHearth.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private IBluetoothComm _bluetoothComm => DependencyService.Get<IBluetoothComm>();
        private SettingsService _settingsService => DependencyService.Get<SettingsService>();
        private ConfigurationItem _selectedItem;

        public ObservableCollection<ConfigurationItem> ConfigurationItems { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<ConfigurationItem> ItemTapped { get; }

        public SettingsViewModel()
        {
            Title = "Settings";
            ConfigurationItems = new ObservableCollection<ConfigurationItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<ConfigurationItem>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);

            ExecuteLoadItemsCommand();
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                ConfigurationItems.Clear();
                foreach (var config in _settingsService.GetAllSettings())
                    ConfigurationItems.Add(config);
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public ConfigurationItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnItemSelected(ConfigurationItem item)
        {
            if (item == null)
                return;

            Console.WriteLine($"{item.Id}__{item.Text}__{item.Description}");

            // This will push the page onto the navigation stack
            switch (item.Id)
            {
                case "0":
                    await Shell.Current.GoToAsync($"{nameof(BluetoothConfigurationPage)}");
                    break;

                case "1":
                    await Shell.Current.GoToAsync($"{nameof(LedConfigurationPage)}");
                    break;
            }
        }
    }
}
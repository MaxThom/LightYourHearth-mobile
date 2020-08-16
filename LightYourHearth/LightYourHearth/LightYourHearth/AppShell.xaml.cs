using System;
using System.Collections.Generic;

using LightYourHearth.ViewModels;
using LightYourHearth.Views;

using Xamarin.Forms;

namespace LightYourHearth
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}

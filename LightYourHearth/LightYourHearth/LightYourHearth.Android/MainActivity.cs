﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

using ImageCircle.Forms.Plugin.Droid;

using Rg.Plugins.Popup;

namespace LightYourHearth.Droid
{
    [Activity(Label = "LightYourHearth", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Popup.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            //SimpleStorage.SetContext(ApplicationContext);

            LoadApplication(new App());
            //Window.SetStatusBarColor(Android.Graphics.Color.Rgb(234, 128, 252));
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(182, 79, 200));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
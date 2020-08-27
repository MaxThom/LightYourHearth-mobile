using DLToolkit.Forms.Controls;

using ImageCircle.Forms.Plugin.Abstractions;

using LightYourHearth.ViewModels;

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LightYourHearth.Views
{
    public partial class AnimationPage : ContentPage
    {
        private AnimationViewModel vm;

        public AnimationPage()
        {
            InitializeComponent();
            FlowListView.Init();
            vm = new AnimationViewModel();
            BindingContext = vm;
        }
    }
}
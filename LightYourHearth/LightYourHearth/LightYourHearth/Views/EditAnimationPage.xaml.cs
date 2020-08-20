using ColorPicker;

using LightYourHearth.Models;
using LightYourHearth.ViewModels;

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LightYourHearth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("Animation", "animation")]
    public partial class EditAnimationPage : ContentPage
    {
        private string animation;

        public string Animation
        {
            get => animation;
            set
            {
                animation = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                ledAnimation = vm.GetAnimation(Animation);
                if (ledAnimation?.Arguments != null && ledAnimation.Arguments.Any())
                    GenerateLayout();
            }
        }

        private EditAnimationViewModel vm;
        private LedAnimation ledAnimation;

        public EditAnimationPage()
        {
            InitializeComponent();
            vm = new EditAnimationViewModel();
            BindingContext = vm;
        }

        private void GenerateLayout()
        {
            foreach (var arg in ledAnimation.Arguments)
            {
                switch (arg.Type)
                {
                    case LedAnimationArgumentType.Color:
                        AddColorParameter(arg);
                        break;

                    case LedAnimationArgumentType.Double:
                        AddSliderParameter(arg);
                        break;

                    case LedAnimationArgumentType.Int:
                        AddSliderParameter(arg);
                        break;

                    case LedAnimationArgumentType.String:
                        break;
                }
            }
        }

        private void AddSliderParameter(LedAnimationArgument arg)
        {
            var displayLabel = new Label()
            {
                Text = arg.Value == string.Empty ? $"{arg.DefaultValue}" : $"{arg.Value}",
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = new Thickness(10, 1, 0, 0)
            };
            var slider = new Slider()
            {
                Minimum = double.Parse(arg.MinValue),
                Maximum = double.Parse(arg.MaxValue),
                Value = arg.Value == string.Empty ? double.Parse(arg.DefaultValue, CultureInfo.InvariantCulture) : double.Parse(arg.Value, CultureInfo.InvariantCulture),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ThumbColor = Color.DodgerBlue,
                MinimumTrackColor = Color.DodgerBlue,
                MaximumTrackColor = Color.Gray,
            };
            slider.ValueChanged += (sender, args) =>
            {
                var newValue = arg.Type == LedAnimationArgumentType.Int ? ((int)args.NewValue).ToString() : args.NewValue.ToString();
                displayLabel.Text = newValue;
                vm.UpdateAnimationArgument(arg.Name, newValue);
            };

            ParamLayout.Children.Add(
                new StackLayout()
                {
                    Children =
                    {
                        new StackLayout()
                        {
                            Orientation=StackOrientation.Horizontal,
                            Padding= new Thickness(5, 0),
                            Children =
                            {
                                new Label()
                                {
                                    Text=$"{arg.GetDisplayName()}",
                                    TextColor=Color.Black,
                                    FontAttributes=FontAttributes.Bold,
                                    VerticalOptions=LayoutOptions.Start,
                                    HorizontalOptions=LayoutOptions.Start,
                                },
                                displayLabel
                            }
                        },
                        slider
                    }
                }
            );
            ParamLayout.Children.Add(new BoxView()
            {
                BackgroundColor = Color.Black,
                HeightRequest = 1
            });
        }

        private void AddColorParameter(LedAnimationArgument arg)
        {
            var displayLabel = new Label()
            {
                Text = arg.Value == string.Empty ? $"{arg.DefaultValue}" : $"{arg.Value}",
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                HeightRequest = 50,
                BackgroundColor = Color.FromHex(arg.Value == string.Empty ? arg.DefaultValue : arg.Value)
            };
            var colorPicker = new ColorCircle()
            {
                Padding = new Thickness(5, -10, 5, 5),
                HeightRequest = 250
            };
            colorPicker.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                Console.WriteLine(e.PropertyName);
                if (e.PropertyName.Equals("SelectedColor"))
                {
                    displayLabel.BackgroundColor = colorPicker.SelectedColor;
                    displayLabel.Text = colorPicker.SelectedColor.ToHex();
                    vm.UpdateAnimationArgument(arg.Name, colorPicker.SelectedColor.ToHex());
                }
            };

            TopLayout.Children.Insert(0, new StackLayout()
            {
                Children =
                {
                    new Label()
                    {
                        Text="Color",
                        TextColor=Color.Black,
                        FontAttributes=FontAttributes.Bold,
                        VerticalOptions=LayoutOptions.CenterAndExpand,
                        HorizontalOptions=LayoutOptions.StartAndExpand,
                        Padding= new Thickness(5, 0, 0, 0),
                    },
                    displayLabel,
                    colorPicker,
                    new BoxView()
                    {
                        BackgroundColor = Color.Black,
                        HeightRequest = 1
                    }
                }
            });
        }
    }
}
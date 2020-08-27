using LightYourHearth.Models;
using LightYourHearth.Pages;
using LightYourHearth.ViewModels;

using Rg.Plugins.Popup.Services;

using System;
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
            foreach (var arg in ledAnimation.Arguments.OrderBy(x => x.Type.ToString()).ThenBy(y => y.Name))
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

                    case LedAnimationArgumentType.Boolean:
                        AddSwitchParameter(arg);
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
                Maximum = double.Parse(arg.MaxValue, CultureInfo.InvariantCulture),
                Minimum = double.Parse(arg.MinValue, CultureInfo.InvariantCulture),
                Value = arg.Value == string.Empty ? double.Parse(arg.DefaultValue, CultureInfo.InvariantCulture) : double.Parse(arg.Value, CultureInfo.InvariantCulture),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ThumbColor = Color.DodgerBlue,
                MinimumTrackColor = Color.DodgerBlue,
                MaximumTrackColor = Color.Gray,
            };
            slider.ValueChanged += (sender, args) =>
            {
                var newValue = arg.Type == LedAnimationArgumentType.Int ? ((int)args.NewValue).ToString(CultureInfo.InvariantCulture) : args.NewValue.ToString(CultureInfo.InvariantCulture);
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
                BackgroundColor = Color.SlateGray,
                HeightRequest = 1.5
            });
        }

        private void AddColorParameter(LedAnimationArgument arg)
        {
            var colorBtn = new Button()
            {
                Text = arg.Value == string.Empty ? $"{arg.DefaultValue}" : $"{arg.Value}",
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex(arg.Value == string.Empty ? arg.DefaultValue : arg.Value)
            };

            var initialColor = Color.FromHex(arg.Value == string.Empty ? $"{arg.DefaultValue}" : $"{arg.Value}");
            colorBtn.Clicked += async (object sender, EventArgs e) =>
            {
                var page = new ColorPickerPopupPage((color) =>
                {
                    Console.WriteLine($"{color.R}-{color.G}-{color.B}-{color.A}");
                    colorBtn.BackgroundColor = color;
                    colorBtn.Text = color.ToHex();
                    vm.UpdateAnimationArgument(arg.Name, color.ToHex());
                }, Color.FromHex(arg.Value == string.Empty ? $"{arg.DefaultValue}" : $"{arg.Value}"));

                await PopupNavigation.Instance.PushAsync(page);
            };

            ParamLayout.Children.Add(new StackLayout()
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
                    colorBtn
                }
            });
            ParamLayout.Children.Add(new BoxView()
            {
                BackgroundColor = Color.SlateGray,
                HeightRequest = 1.5
            });
        }

        private void AddSwitchParameter(LedAnimationArgument arg)
        {
            var switchUi = new Switch()
            {
                IsToggled = arg.Value == string.Empty ? bool.Parse(arg.DefaultValue) : bool.Parse(arg.Value),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ThumbColor = Color.DodgerBlue,
                OnColor = Color.FromHex("#7cbbf2"),
                BackgroundColor = Color.White
            };
            switchUi.Toggled += (sender, args) =>
            {
                vm.UpdateAnimationArgument(arg.Name, switchUi.IsToggled.ToString());
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
                                switchUi
                            }
                        }
                    }
                }
            );
            ParamLayout.Children.Add(new BoxView()
            {
                BackgroundColor = Color.SlateGray,
                HeightRequest = 1.5
            });
        }
    }
}
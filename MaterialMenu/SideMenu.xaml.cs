//The MIT License(MIT)

//Copyright(c) 2015 Alberto Rodriguez

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialMenu
{
    public partial class SideMenu
    {
        private bool _isShown;

        public SideMenu()
        {
            InitializeComponent();
            Theme = SideMenuTheme.Default;
            ClosingType = ClosingType.Auto;
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
        "State",
        typeof(MenuState),
        typeof(SideMenu));

        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register(
        "Theme",
        typeof(SideMenuTheme),
        typeof(SideMenu));

        public static readonly DependencyProperty MenuWidthProperty = DependencyProperty.Register(
        "MenuWidth",
        typeof(double),
        typeof(SideMenu));

        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register(
        "Menu",
        typeof(ScrollViewer),
        typeof(SideMenu));

        public static readonly DependencyProperty ShadowBackgroundProperty = DependencyProperty.Register(
        "ShadowBackground",
        typeof(Brush),
        typeof(SideMenu));

        public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register(
        "ButtonBackground",
        typeof(Brush),
        typeof(SideMenu));

        public static readonly DependencyProperty ButtonHoverProperty = DependencyProperty.Register(
        "ButtonHover",
        typeof(Brush),
        typeof(SideMenu));

        public ClosingType ClosingType { get; set; }

        public Brush ButtonBackground
        {
            get { return (Brush)GetValue(ButtonBackgroundProperty); }
            set
            {
                SetValue(ButtonBackgroundProperty, value);
                Resources["ButtonBackground"] = value;
            }
        }

        public Brush ButtonHover
        {
            get { return (Brush)GetValue(ButtonHoverProperty); }
            set
            {
                SetValue(ButtonHoverProperty, value);
                Resources["ButtonHover"] = value;
            }
        }

        public Brush ShadowBackground
        {
            get { return (Brush)GetValue(ShadowBackgroundProperty); }
            set
            {
                SetValue(ShadowBackgroundProperty, value);
                Resources["Shadow"] = value ?? new SolidColorBrush { Color = Colors.Black, Opacity = .2 };
            }
        }

        public ScrollViewer Menu
        {
            get { return (ScrollViewer) GetValue(MenuProperty); }
            set { SetValue(MenuProperty, value); }
        }

        public double MenuWidth
        {
            get { return (double) GetValue(MenuWidthProperty); }
            set
            {
                SetValue(MenuWidthProperty, value);
            }
        }

        public MenuState State
        {
            get { return (MenuState)GetValue(StateProperty); }
            set
            {
                SetValue(StateProperty, value);
                if (value == MenuState.Visible)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            }
        }

        public SideMenuTheme Theme
        {
            get { return (SideMenuTheme) GetValue(ThemeProperty); }
            set
            {
                if (value == SideMenuTheme.None) return;
                SetValue(ThemeProperty, value);
                SolidColorBrush buttonBackground;
                SolidColorBrush buttonHoverBackground;
                SolidColorBrush background;
                switch (value)
                {
                    case SideMenuTheme.Default:
                        background = new SolidColorBrush { Color = Color.FromArgb(205, 20, 20, 20) };
                        buttonBackground = new SolidColorBrush {Color = Color.FromArgb(50,30,30,30)};
                        buttonHoverBackground = new SolidColorBrush {Color = Color.FromArgb(50, 70,70,70)};
                        break;
                    case SideMenuTheme.Primary:
                        background = new SolidColorBrush { Color = Color.FromArgb(205, 24, 57, 85) };
                        buttonBackground = new SolidColorBrush { Color = Color.FromArgb(50, 35, 85, 126) };
                        buttonHoverBackground = new SolidColorBrush { Color = Color.FromArgb(50, 45, 110, 163) };
                        break;
                    case SideMenuTheme.Success:
                        background = new SolidColorBrush { Color = Color.FromArgb(205, 55, 109, 55) };
                        buttonBackground = new SolidColorBrush { Color = Color.FromArgb(50, 65, 129, 65) };
                        buttonHoverBackground = new SolidColorBrush { Color = Color.FromArgb(50, 87, 172, 87) };
                        break;
                    case SideMenuTheme.Warning:
                        background = new SolidColorBrush { Color = Color.FromArgb(205, 150, 108, 49) };
                        buttonBackground = new SolidColorBrush { Color = Color.FromArgb(50, 179, 129, 58) };
                        buttonHoverBackground = new SolidColorBrush { Color = Color.FromArgb(50, 216, 155, 70) };
                        break;
                    case SideMenuTheme.Danger:
                        background = new SolidColorBrush { Color = Color.FromArgb(205, 135, 52, 49) };
                        buttonBackground = new SolidColorBrush {Color = Color.FromArgb(50, 179, 69, 65)};
                        buttonHoverBackground = new SolidColorBrush { Color = Color.FromArgb(50, 238, 92, 86) };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                ButtonBackground = buttonBackground;
                ButtonHover = buttonHoverBackground;
                if (Menu != null) Menu.Background = background;
            }
        }

        public void Toggle()
        {
            if (_isShown)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public void Show()
        {
            var animation = new DoubleAnimation
            {
                From = -MenuWidth*.85, To = 0, Duration = TimeSpan.FromMilliseconds(100)
            };
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            _isShown = true;
            var p = Parent as Panel;
            (FindName("ShadowColumn") as ColumnDefinition).Width = new GridLength(10000);
        }

        public void Hide()
        {
            var animation = new DoubleAnimation
            {
                To = -MenuWidth,
                Duration = TimeSpan.FromMilliseconds(100)
            };
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            _isShown = false;
            (FindName("ShadowColumn") as ColumnDefinition).Width = new GridLength(0);
        }

        public override void OnApplyTemplate()
        {
            Panel.SetZIndex(this, int.MaxValue);
            RenderTransform = new TranslateTransform(-MenuWidth, 0);
            (FindName("MenuColumn") as ColumnDefinition).Width = new GridLength(MenuWidth);

            //this is a little hack to fire propertu changes.
            //wpf so complex, it could be much simple...
            State = State;
            Theme = Theme;
            ShadowBackground = ShadowBackground;
            ButtonBackground = ButtonBackground;
            ButtonHover = ButtonHover;
        }

        private void ShadowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ClosingType == ClosingType.Auto) Hide();
        }
    }
}
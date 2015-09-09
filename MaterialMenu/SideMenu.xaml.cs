using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialMenu
{
    /// <summary>
    /// Interaction logic for SideMenu.xaml
    /// </summary>
    public partial class SideMenu
    {
        private bool _isShown;

        public SideMenu()
        {
            InitializeComponent();
            RenderTransform = new TranslateTransform(-MenuWidth,0);
            Theme = SideMenuTheme.Primary;
            BackgroundOpacity = .9;
            ClosingType = ClosingType.Auto;
            ShadowBackground = new SolidColorBrush {Color = Colors.Black, Opacity = .2};
        }

        public static readonly DependencyProperty ShownProperty = DependencyProperty.Register(
        "Shown",
        typeof(bool),
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

        public static readonly DependencyProperty BackgroundOpacityProperty = DependencyProperty.Register(
        "BackgroundOpacity",
        typeof(double),
        typeof(SideMenu));

        public static readonly DependencyProperty ShadowBackgroundProperty = DependencyProperty.Register(
        "ShadowBackground",
        typeof(SolidColorBrush),
        typeof(SideMenu));

        public ClosingType ClosingType { get; set; }

        public SolidColorBrush ShadowBackground
        {
            get { return (SolidColorBrush)GetValue(ShadowBackgroundProperty); }
            set
            {
                SetValue(ShadowBackgroundProperty, value);
                Resources["Shadow"] = value;
            }
        }

        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        public ScrollViewer Menu
        {
            get { return (ScrollViewer) GetValue(MenuProperty); }
            set { SetValue(MenuProperty, value); }
        }

        public double MenuWidth
        {
            get { return (double) GetValue(MenuWidthProperty); }
            set { SetValue(MenuWidthProperty, value); }
        }

        public bool Shown
        {
            get { return (bool)GetValue(ShownProperty); }
            set
            {
                SetValue(ShownProperty, value);
                if (value)
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
                SetValue(ThemeProperty, value);
                var o = BackgroundOpacity;
                switch (value)
                {
                    case SideMenuTheme.Default:
                        if (Menu != null)
                            Menu.Background = new SolidColorBrush {Color = Color.FromRgb(30, 30, 30), Opacity = o};
                        Resources["ButtonHover"] = Color.FromRgb(70, 70, 70);
                        Resources["ButtonBackground"] = Colors.Transparent;
                        break;
                    case SideMenuTheme.Primary:
                        if (Menu != null)
                            Menu.Background = new SolidColorBrush {Color = Color.FromRgb(35, 85, 126), Opacity = o };
                        Resources["ButtonHover"] = Color.FromRgb(45, 110, 163);
                        Resources["ButtonBackground"] = Colors.Transparent;
                        break;
                    case SideMenuTheme.Success:
                        if (Menu != null)
                            Menu.Background = new SolidColorBrush {Color = Color.FromRgb(65, 129, 65), Opacity = o };
                        Resources["ButtonHover"] = Color.FromRgb(87, 172, 87);
                        Resources["ButtonBackground"] = Colors.Transparent;
                        break;
                    case SideMenuTheme.Warning:
                        if (Menu != null)
                            Menu.Background = new SolidColorBrush {Color = Color.FromRgb(179, 129, 58), Opacity = o };
                        Resources["ButtonHover"] = Color.FromRgb(216, 155, 70);
                        Resources["ButtonBackground"] = Colors.Transparent;
                        break;
                    case SideMenuTheme.Danger:
                        if (Menu != null)
                           Menu.Background = new SolidColorBrush {Color = Color.FromRgb(179, 69, 65), Opacity = o };
                        Resources["ButtonHover"] = Color.FromRgb(238, 92, 86);
                        Resources["ButtonBackground"] = Colors.Transparent;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
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
            (FindName("ShadowColumn") as ColumnDefinition).Width = new GridLength(p.ActualWidth + MenuWidth);
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
            var p = Parent as Panel;
            (FindName("ShadowColumn") as ColumnDefinition).Width = new GridLength(0);
        }

        public override void OnApplyTemplate()
        {
            Panel.SetZIndex(this, int.MaxValue);
            Theme = Theme;
        }

        private void SideMenu_OnLoaded(object sender, RoutedEventArgs e)
        {
            //Not sure how to start with menu closed... so this is a litle hack
            //that looks ugly because you can see menu hiding whn form starts.
            Shown = Shown;
        }

        private void ShadowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ClosingType == ClosingType.Auto) Hide();
        }
    }
}
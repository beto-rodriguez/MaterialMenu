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
            Theme = SideMenuTheme.Default;
            ClosingType = ClosingType.Auto;
            ShadowBackground = new SolidColorBrush {Color = Colors.Black, Opacity = .2};
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
                Resources["ButtonHover"] = buttonBackground;
                Resources["ButtonBackground"] = buttonHoverBackground;
                if(Menu != null) Menu.Background = background;
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
            State = State;
            Theme = Theme;
        }

        private void ShadowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ClosingType == ClosingType.Auto) Hide();
        }
    }
}
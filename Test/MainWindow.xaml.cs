using System.Windows;
using System.Windows.Input;
using MaterialMenu;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Menu.Toggle();
        }

        private void Custom_OnClick(object sender, RoutedEventArgs e)
        {
            CustomMenu.Toggle();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("You Clicked Packing!");
        }

        private void DefaultClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Default;
        }

        private void PrimaryClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Primary;
        }

        private void SuccessClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Success;
        }

        private void WarningClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Warning;
        }

        private void DangerClick(object sender, RoutedEventArgs e)
        {
            Menu.Theme = SideMenuTheme.Danger;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Menu.Hide();
        }

        private void ToggleClosingTypeClick(object sender, RoutedEventArgs e)
        {
            Menu.ClosingType = Menu.ClosingType == ClosingType.Auto 
                ? ClosingType.Manual 
                : ClosingType.Auto;
        }

        private SideMenu MapMenuToTheme(SideMenuTheme theme)
        {
            //this should not be necesray but colors are not changing correctly
            //when changing theme porperty... maybe its needed to implement INotifyPropertyChanged
            return new SideMenu
            {
                MenuWidth = Menu.MenuWidth,
                Theme = theme,
                Menu = Menu.Menu
            };
        }
    }
}

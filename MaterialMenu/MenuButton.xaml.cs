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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaterialMenu
{
    /// <summary>
    /// Interaction logic for MenuButton.xaml
    /// </summary>
    public partial class MenuButton 
    {
        private bool _areChildrenVisible;
        private Brush _originalBackgound;

        public MenuButton()
        {
            InitializeComponent();
            Children = new List<MenuButton>();
            Theme = UiTheme.Light;
            AnimationSpeed = TimeSpan.FromMilliseconds(150);
        }

        public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(
        "HoverBackground",
        typeof(SolidColorBrush),
        typeof(MenuButton));

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
        "Image",
        typeof(ImageSource),
        typeof(MenuButton));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        "Text",
        typeof(string),
        typeof(MenuButton));

        public static readonly DependencyProperty AnimationSpeedProperty = DependencyProperty.Register(
        "AnimationSpeed",
        typeof(TimeSpan),
        typeof(MenuButton));

        public static readonly DependencyProperty UiThemeProperty = DependencyProperty.Register(
        "UiTheme",
        typeof(UiTheme),
        typeof(MenuButton));

        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register(
        "Children",
        typeof(List<MenuButton>),
        typeof(MenuButton));

        public MenuButton ParentButton { get; private set; }

        public TimeSpan AnimationSpeed
        {
            get { return (TimeSpan)GetValue(AnimationSpeedProperty); }
            set { SetValue(AnimationSpeedProperty, value); }
        } 

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public SolidColorBrush HoverBackground
        {
            get { return (SolidColorBrush) GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        public ImageSource Image
        {
            get { return (ImageSource) GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public UiTheme Theme
        {
            get { return (UiTheme) GetValue(UiThemeProperty); }
            set
            {
                SetValue(UiThemeProperty, value);
                BitmapImage bm;
                switch (Theme)
                {
                    case UiTheme.Light:
                        bm = new BitmapImage(new Uri(@"Images/Light24.png", UriKind.Relative));
                        break;
                    case UiTheme.Dark:
                        bm = new BitmapImage(new Uri(@"Images/Dark24.png", UriKind.Relative));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var i = FindName("Chevron") as Image;
                i.Source = bm;

                var tb = FindName("Txt") as TextBlock;
                tb.Foreground = Theme == UiTheme.Light
                    ? Brushes.WhiteSmoke
                    : new SolidColorBrush { Color = Color.FromRgb(30, 30, 30) };

            }
        }

        public List<MenuButton> Children
        {
            get { return (List<MenuButton>) GetValue(ChildrenProperty); }
            set
            {
                SetValue(ChildrenProperty, value);
                foreach (var child in Children)
                {
                    child.ParentButton = this;
                }
                DataContext = Children;
                var c = FindName("Chevron") as Image;
                c.Visibility = Children == null || Children.Count == 0 ? Visibility.Hidden : Visibility.Visible;
            }
        }

        private double ExpandedHeight => Children.Count * ActualHeight + 5;

        private void Expand(double units)
        {
            var c = FindName("Chld") as ItemsControl;
            var animation = new DoubleAnimation
            {
                From = c.ActualHeight,
                To = c.ActualHeight + units,
                Duration = AnimationSpeed
            };
            c.BeginAnimation(HeightProperty, animation);
        }

        private void Reduce(double units)
        {
            var c = FindName("Chld") as ItemsControl;
            var animation = new DoubleAnimation
            {
                From = c.ActualHeight,
                To = c.ActualHeight - units,
                Duration = AnimationSpeed
            };
            c.BeginAnimation(HeightProperty, animation);
        }

        private void Open()
        {
            var parent = FindName("ChevronRotate") as RotateTransform;
            var rotateAnimation = new DoubleAnimation
            {
                To = 90,
                Duration = AnimationSpeed
            };
            parent.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

            Expand(ExpandedHeight);
            _areChildrenVisible = true;
        }

        private void Close()
        {
            var parent = FindName("ChevronRotate") as RotateTransform;
            var rotateAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = AnimationSpeed
            };
            parent.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

            foreach (var child in Children) child.Close();
            if (!_areChildrenVisible) return;
            var c = FindName("Chld") as ItemsControl;
            var animation = new DoubleAnimation
            {
                From = c.ActualHeight,
                To = 0,
                Duration = AnimationSpeed
            };
            c.BeginAnimation(HeightProperty, animation);
            _areChildrenVisible = false;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Children.Count == 0) return;
            if (_areChildrenVisible)
            {
                var parent = ParentButton;
                while (parent != null)
                {
                    var c = FindName("Chld") as ItemsControl;
                    parent.Reduce(c.ActualHeight);
                    parent = parent.ParentButton;
                }
                Close();
            }
            else
            {
                var parent = ParentButton;
                while (parent != null)
                {
                    parent.Expand(ExpandedHeight);
                    parent = parent.ParentButton;
                }
                Open();
            }
        }

        private void MenuButtonBase_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var g = FindName("Grid") as Grid;
            var animation = new ColorAnimation
            {
                To = HoverBackground.Color,
                Duration = AnimationSpeed
            };
            var sb = new Storyboard();
            sb.Children.Add(animation);
            Storyboard.SetTarget(animation, g);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Background.Color"));
            sb.Begin();
        }

        private void MenuButtonBase_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var g = FindName("Grid") as Grid;
            var animation = new ColorAnimation
            {
                To = (Background as SolidColorBrush).Color,
                Duration = AnimationSpeed
            };
            var sb = new Storyboard();
            sb.Children.Add(animation);
            Storyboard.SetTarget(animation, g);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Background.Color"));
            sb.Begin();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var tb = FindName("Txt") as TextBlock;
            tb.Text = Text;

            var i = FindName("Img") as Image;
            i.Source = Image;

            var ic = FindName("Chld") as ItemsControl;
            ic.DataContext = Children;
            ic.GetBindingExpression(ItemsControl.ItemsSourceProperty).UpdateTarget();
            foreach (var child in Children)
            {
                child.ParentButton = this;
            }

            var c = FindName("Chevron") as Image;
            c.Visibility = Children == null || Children.Count == 0 ? Visibility.Hidden : Visibility.Visible;

            var g = FindName("Grid") as Grid;
            g.Background = Background;
        }

        protected override void OnInitialized(EventArgs e)
        {
            InitializeComponent();
            base.OnInitialized(e);
        }
    }
}
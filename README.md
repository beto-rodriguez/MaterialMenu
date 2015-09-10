#Wpf Material Menu

A good looking animated side menu, with just one line of code. It already includes preloaded themes just changing `Theme`. property.

<p align="center">
  <img src="https://dl.dropboxusercontent.com/u/40165535/quxl0.gif" />
</p>

#Instalation

1. Install from **[NuGet](https://www.nuget.org/packages/MaterialMenu/)** `Install-Package MaterialMenu`
2. Add namespace `xmlns:materialMenu="clr-namespace:MaterialMenu;assembly=MaterialMenu"`
3. Now you can add this example to your Xaml

```xml
<materialMenu:SideMenu HorizontalAlignment="Left" x:Name="Menu"
                               MenuWidth="300"
                               Theme="Default"
                               State="Hidden">
            <materialMenu:SideMenu.Menu>
                <ScrollViewer VerticalScrollBarVisibility="Hidden">
                    <StackPanel Orientation="Vertical">
                        <Border Background="#337AB5">
                            <Grid Margin="10">
                                <TextBox Height="150" BorderThickness="0" Background="Transparent"
                                    VerticalContentAlignment="Bottom" FontFamily="Calibri" FontSize="18"
                                    Foreground="WhiteSmoke" FontWeight="Bold">Welcome</TextBox>
                            </Grid>
                        </Border>
                        <materialMenu:MenuButton Text="Administration"></materialMenu:MenuButton>
                        <materialMenu:MenuButton Text="Packing"></materialMenu:MenuButton>
                        <materialMenu:MenuButton Text="Logistics"></materialMenu:MenuButton>
                    </StackPanel>
                </ScrollViewer>
            </materialMenu:SideMenu.Menu>
        </materialMenu:SideMenu>
```

<h1>Controls</h1>

<hr/>

<h3>A. <a href="https://github.com/beto-rodriguez/MaterialMenu/blob/master/MaterialMenu/SideMenu.xaml.cs">SideMenu</a></h3>

<h4>Inheritance Hierarchy</h4>

[User Control](https://msdn.microsoft.com/en-us/library/system.windows.controls.usercontrol(v=vs.110).aspx)

<h4>Properties</h4>

| Name  | Description | Type |
| ------------- | ------------- | ------------- |
| `Theme`  | Gets or sets menu color set  | `SideMenuTheme` (`enum` Default, Primary Success, Warning, Danger) |
| `State`  | Gets or sets menu visibility | `MenuState` (`enum` Visible, Hidden) |
| `MenuWidth` | Gets or sets menu column width | `double` |
| `Menu` | Gets or sets menu XAML | [`ScrollViewer`](https://msdn.microsoft.com/es-es/library/system.windows.controls.scrollviewer(v=vs.110).aspx) |
| `ShadowBackground` | Gets or sets menu shadow color | [`SolidColorBrush`](https://msdn.microsoft.com/en-us/library/system.windows.media.solidcolorbrush(v=vs.110).aspx) |
| `ClosingType` | Gets or sets how menu is closed | `ClosingType` (`enum` Auto, Manual) |

<h4>Methods</h4>

| Name  | Description | Returned Type |
| ------------- | ------------- | ------------- |
| `Show()`  | Shows menu  | `void` |
| `Hide()`  | Hides menu  | `void` |
| `Toggle()`  | Toggles menu  | `void` |

<hr/>

<h3>B. <a href="https://github.com/beto-rodriguez/MaterialMenu/blob/master/MaterialMenu/MenuButton.xaml.cs">MenuButton</a></h3>

<h4>Inheritance Hierarchy</h4>

[User Control](https://msdn.microsoft.com/en-us/library/system.windows.controls.usercontrol(v=vs.110).aspx)

<h4>Properties</h4>

| Name  | Description | Type |
| ------------- | ------------- | ------------- |
| `HoverBackground`  | Gets or sets button brush on hover  | [`SolidColorBrush`](https://msdn.microsoft.com/en-us/library/system.windows.media.solidcolorbrush(v=vs.110).aspx) |
| `Image`  | Gets or sets button image | [`ImageSource`](https://msdn.microsoft.com/en-us/library/system.windows.media.imagesource(v=vs.110).aspx) |
| `Text` | Gets or sets button text | `string` |
| `AnimationSpeed` | Gets or sets color animation speen on hover | [`TimeSpan`](https://msdn.microsoft.com/en-us/library/system.timespan(v=vs.110).aspx) |
| `Children` | Gets or sets menu children elements, they are shown only when users clicks on parent button. | `List<MenuButton>` |
| `Parent` | Gets parent menu button | [`MenuButton`](https://github.com/beto-rodriguez/MaterialMenu/blob/master/MaterialMenu/MenuButton.xaml.cs) |

<h4>Methods</h4>

none.

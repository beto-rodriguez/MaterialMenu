#Wpf Material Menu

An easy to use side menu. 
Support for nested elements

![nested elements](https://dl.dropboxusercontent.com/u/40165535/qrzx4.gif)

preloaded themes

![nested elements](https://dl.dropboxusercontent.com/u/40165535/qs012.gif)

#Instalation

1. Install from **[NuGet](https://www.nuget.org/packages/MaterialMenu/)** `Install-Package MaterialMenu`
2. Add namespace `xmlns:materialMenu="clr-namespace:MaterialMenu;assembly=MaterialMenu"`
3. Now you can add this example to your Xaml
 

 ```xml
<materialMenu:SideMenu HorizontalAlignment="Left" x:Name="Menu" 
                               MenuWidth="400" 
                               Shown="False"
                               Theme="Primary"
                               BackgroundOpacity=".9">
            <materialMenu:SideMenu.Menu>
                <ScrollViewer VerticalScrollBarVisibility="Hidden">
                    <StackPanel Orientation="Vertical">
                        <TextBlock Foreground="WhiteSmoke" FontFamily="Calibri" FontSize="24" MinHeight="150" FontWeight="UltraBold">
                            <TextBlock.Background>
                                <ImageBrush ImageSource="colors.jpg" Stretch="UniformToFill"></ImageBrush>
                            </TextBlock.Background>
                            <TextBlock.Text>Welcome</TextBlock.Text>
                        </TextBlock>
                        <materialMenu:MenuButton Text="Administration"></materialMenu:MenuButton>
                        <materialMenu:MenuButton Text="Packing"></materialMenu:MenuButton>
                        <materialMenu:MenuButton Text="Logistics"></materialMenu:MenuButton>
                        <materialMenu:MenuButton Text="Org"></materialMenu:MenuButton>
                        <materialMenu:MenuButton Text="SaveChanges"></materialMenu:MenuButton>
                        <materialMenu:MenuButton Text="Help"></materialMenu:MenuButton>
                        <materialMenu:MenuButton Text="Close Menu"></materialMenu:MenuButton>
                    </StackPanel>
                </ScrollViewer>
            </materialMenu:SideMenu.Menu>
        </materialMenu:SideMenu> 
 ```

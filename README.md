# Notice
This repository is archived after release of [DockLayout](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/layouts/docklayout) there is no need to use this layout so rather use [MAUI Community Toolkit](https://github.com/CommunityToolkit/Maui)

## Universal Layouts
This projects provides 2 new types of layouts UniversalHorizontalStackLayout and UniversalVerticalStackLayout. This project is for those who sometimes want dynamically add and set
layoutoptions to child of layouts. Because Horizontal/Vertical layout doesn't support settings of
HorizontalOptions/VerticalOptions in their respective orientation we need to use grid or absolute layout, but these layout's don't automatically adjust location of children if there is already one
in it's place where UniversalLayouts does.

### Showcase
![Example Image](https://github.com/zZHorizonZz/Horizon.UniversalLayout/blob/master/example.png)

### Example Application
You can find example application here [Example App](https://github.com/zZHorizonZz/Horizon.UniversalLayout/tree/master/Example)

### Example Code
```xaml
<?xml version="1.0" encoding="utf-8"?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:layout="clr-namespace:Horizon.UniversalLayout.Layout;assembly=Horizon.UniversalLayout"
             x:Class="Example.MainPage">

    <AbsoluteLayout>
        <layout:UniversalVerticalStackLayout BackgroundColor="Black" Spacing="8" WidthRequest="32"
                                             AbsoluteLayout.LayoutBounds="0, 0, 1, 0.5"
                                             AbsoluteLayout.LayoutFlags="All">
            <BoxView Color="Red" WidthRequest="32" HeightRequest="32" VerticalOptions="Start"></BoxView>
            <BoxView Color="DarkRed" WidthRequest="32" HeightRequest="32" VerticalOptions="Start"></BoxView>
            <BoxView Color="Green" WidthRequest="32" HeightRequest="32" VerticalOptions="Center"></BoxView>
            <BoxView Color="Olive" WidthRequest="32" HeightRequest="32" VerticalOptions="Center"></BoxView>
            <BoxView Color="Blue" WidthRequest="32" HeightRequest="32" VerticalOptions="End"></BoxView>
            <BoxView Color="CadetBlue" WidthRequest="32" HeightRequest="32" VerticalOptions="End"></BoxView>
        </layout:UniversalVerticalStackLayout>
        <layout:UniversalHorizontalStackLayout BackgroundColor="Black" Spacing="8" HeightRequest="32"
                                               AbsoluteLayout.LayoutBounds="0, 0.75, 1, 0.5"
                                               AbsoluteLayout.LayoutFlags="All">
            <BoxView Color="Red" WidthRequest="32" HeightRequest="32" HorizontalOptions="Start"></BoxView>
            <BoxView Color="DarkRed" WidthRequest="32" HeightRequest="32" HorizontalOptions="Start"></BoxView>
            <BoxView Color="Green" WidthRequest="32" HeightRequest="32" HorizontalOptions="Center"></BoxView>
            <BoxView Color="Olive" WidthRequest="32" HeightRequest="32" HorizontalOptions="Center"></BoxView>
            <BoxView Color="Blue" WidthRequest="32" HeightRequest="32" HorizontalOptions="End"></BoxView>
            <BoxView Color="CadetBlue" WidthRequest="32" HeightRequest="32" HorizontalOptions="End"></BoxView>
        </layout:UniversalHorizontalStackLayout>
    </AbsoluteLayout>
</ContentPage>
```

### License
This project is under Apache 2.0 license which can be found [here](https://github.com/zZHorizonZz/Horizon.UniversalLayout/blob/master/LICENSE)

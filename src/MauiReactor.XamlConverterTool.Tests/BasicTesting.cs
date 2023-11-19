using System.Xml.Linq;

namespace MauiReactor.XamlConverterTool.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertBasicXaml()
        {
            var xaml = """
                <?xml version="1.0" encoding="utf-8" ?>
                <ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                             x:Class="MauiApp16.MainPage">

                    <ScrollView>
                        <VerticalStackLayout
                            Padding="30,0"
                            Spacing="25">
                            <Image
                                Source="dotnet_bot.png"
                                HeightRequest="185"
                                Aspect="AspectFit"
                                SemanticProperties.Description="dot net bot in a race car number eight" />

                            <Label
                                Text="Hello, World!"
                                Style="{StaticResource Headline}"
                                SemanticProperties.HeadingLevel="Level1" />

                            <Label
                                Text="Welcome to &#10;.NET Multi-platform App UI"
                                Style="{StaticResource SubHeadline}"
                                SemanticProperties.HeadingLevel="Level2"
                                SemanticProperties.Description="Welcome to dot net Multi platform App U I" />

                            <Button
                                x:Name="CounterBtn"
                                Text="Click me" 
                                SemanticProperties.Hint="Counts the number of times you click"
                                Clicked="OnCounterClicked"
                                HorizontalOptions="Fill" />
                        </VerticalStackLayout>
                    </ScrollView>

                </ContentPage>
                """;

            var xamlDocument = XDocument.Parse(xaml);

            var codeBuilder = new MauiReactorCodeBuilder(xamlDocument);

            var generatedCode = codeBuilder.GenerateCode();

            Assert.Pass();
        }


        [Test]
        public void ConvertLevel1PageXaml()
        {
            var xaml = """
                <?xml version="1.0" encoding="utf-8" ?>
                <ContentPage
                    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                    xmlns:local="clr-namespace:MauiApp16"
                    x:Class="MauiApp16.NewsListPage"
                    BackgroundColor="{ DynamicResource BackgroundColor }"
                    Title="Popular"
                    x:Name="root"
                    IconImageSource="diamond.png">

                    <!-- Latest News List -->
                    <CollectionView
                        Grid.Row="1"
                        Margin="{ OnPlatform
                            iOS='0,0,0,60',
                            Android='0'
                        }"
                        ItemsSource="{ Binding List }"
                        VerticalScrollBarVisibility="Never">
                        <CollectionView.Header>
                            <!-- Top Section Container -->
                            <Grid
                                Padding="{ OnIdiom
                                    Tablet='100,0',
                                    Phone='0'
                                }"
                                RowDefinitions="Auto,Auto,Auto"
                                RowSpacing="10">
                                <Grid.Resources>
                                    <ResourceDictionary>
                                        <!-- Style for Indicator View -->
                                        <Style x:Key="IndicatorViewStyle" TargetType="IndicatorView">
                                            <Setter Property="IndicatorColor" Value="#373837" />
                                            <Setter Property="SelectedIndicatorColor" Value="#FFFFFFFF" />
                                            <Setter Property="IndicatorTemplate">
                                                <Setter.Value>
                                                    <DataTemplate>
                                                        <BoxView
                                                            Margin="2,0"
                                                            HeightRequest="3"
                                                            WidthRequest="26"
                                                            CornerRadius="1.5"
                                                        />
                                                    </DataTemplate>
                                                </Setter.Value>
                                            </Setter>
                                        </Style>
                                    </ResourceDictionary>
                                </Grid.Resources>


                                <!-- Carousel View -->
                                <CarouselView
                                    Grid.RowSpan="2"
                                    Margin="0,24,0,0"
                                    ItemsSource="{ Binding Featured }"
                                    HeightRequest="350"
                                    IndicatorView="indicatorView"
                                    HorizontalScrollBarVisibility="Never"
                                    IsBounceEnabled="False">
                                    <CarouselView.ItemTemplate>
                                        <DataTemplate>
                                            <local:NewsCarouselItemTemplate
                                                ToggleFavoriteCommand="{ Binding BindingContext.ToggleFavoriteCommand, Source={x:Reference root} }"
                                                GoToArticleCommand="{ Binding BindingContext.GoToArticleCommand, Source={x:Reference root} }"
                                            />
                                        </DataTemplate>
                                    </CarouselView.ItemTemplate>
                                </CarouselView>

                                <!-- Indicator View -->
                                <IndicatorView
                                    Grid.Row="1"
                                    x:Name="indicatorView"
                                    HorizontalOptions="Center"
                                    VerticalOptions="End"
                                    Margin="{ OnPlatform
                                        Android='0,0,0,15',
                                        iOS='-60,0,0,15'
                                    }"
                                    Style="{ DynamicResource IndicatorViewStyle }"
                                />

                                <!-- Title -->
                                <Label
                                    Margin="24,20"
                                    Grid.Row="2"
                                    Text="Latest News"
                                    FontSize="{ StaticResource TitleFontSize }"
                                    HorizontalOptions="Start"
                                    VerticalOptions="Center"
                                />

                            </Grid>
                        </CollectionView.Header>
                        <CollectionView.ItemTemplate>
                            <DataTemplate>
                                <Grid Padding="24,0">
                                    <local:NewsListItemTemplate ToggleFavoriteCommand="{ Binding BindingContext.ToggleFavoriteCommand, Source={x:Reference root} }" />
                                </Grid>
                            </DataTemplate>
                        </CollectionView.ItemTemplate>
                    </CollectionView>
                </ContentPage>
                """;

            var xamlDocument = XDocument.Parse(xaml);

            var codeBuilder = new MauiReactorCodeBuilder(xamlDocument);

            var generatedCode = codeBuilder.GenerateCode();

            Assert.Pass();
        }


        [Test]
        public void ConvertLevel2PageXaml()
        {
            var xaml = """
                <?xml version="1.0" encoding="utf-8" ?>
                <ContentView
                    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                    xmlns:local="clr-namespace:MauiApp16"
                    x:Class="MauiApp16.NewsListItemTemplate"
                    x:Name="root">

                    <ContentView.GestureRecognizers>
                        <TapGestureRecognizer Tapped="OnArticleTapped" />
                    </ContentView.GestureRecognizers>

                    <Grid
                        ColumnDefinitions="Auto,1.5*,*"
                        RowDefinitions="Auto,*,Auto,Auto"
                        ColumnSpacing="16"
                        RowSpacing="4"
                        Padding="{ OnIdiom
                            Phone='0,10',
                            Tablet='100,10'
                        }"
                        VerticalOptions="Center">

                        <!-- Rounded Corners Border -->
                        <Border
                            Grid.RowSpan="3"
                            StrokeShape="RoundRectangle 20"
                            HeightRequest="96"
                            WidthRequest="80"
                            HorizontalOptions="Center"
                            VerticalOptions="Center"
                            Margin="0,-10"
                            StrokeThickness="0">
                            <!--
                                Grid required, issue: https://github.com/dotnet/maui/issues/9952
                            -->
                            <Grid>
                                <!-- Avatar Image -->
                                <Image
                                    Source="{ Binding ListImage }"
                                    Aspect="AspectFill"
                                    HeightRequest="96"
                                    WidthRequest="80"
                                />
                            </Grid>
                        </Border>

                        <!-- Category -->
                        <Label
                            Grid.Row="0"
                            Grid.Column="1"
                            Grid.ColumnSpan="2"
                            Text="{ Binding Section }"
                            FontSize="12"
                            TextColor="{ DynamicResource PrimaryColor }"
                            VerticalOptions="End"
                        />

                        <!-- Title -->
                        <Label
                            Grid.Row="1"
                            Grid.Column="1"
                            Text="{ Binding Title }"
                            FontSize="16"
                            FontAttributes="Bold"
                            HorizontalOptions="Start"
                            HeightRequest="{ OnPlatform Android=45 }"
                            LineBreakMode="TailTruncation"
                            MaxLines="2"
                        />

                        <!-- Container -->
                        <HorizontalStackLayout
                            Grid.Row="2"
                            Grid.Column="1"
                            HorizontalOptions="Start"
                            Spacing="4">
                            <!-- Date Published -->
                            <Label
                                Text="{ Binding When }"
                                FontSize="{ StaticResource SmallFontSize }"
                                TextColor="{ DynamicResource TextSecondaryColor }"
                                HorizontalOptions="Start"
                                VerticalOptions="Center"
                            />
                            <!-- Separator -->
                            <Label
                                Text=" • "
                                FontSize="{ StaticResource SmallFontSize }"
                                TextColor="{ DynamicResource TextSecondaryColor }"
                                HorizontalOptions="Start"
                                VerticalOptions="Center"
                            />
                            <!-- Reading Time -->
                            <Label
                                Text="{ Binding Length }"
                                FontSize="{ StaticResource SmallFontSize }"
                                TextColor="{ DynamicResource TextSecondaryColor }"
                                HorizontalOptions="Start"
                                VerticalOptions="Center"
                            />
                        </HorizontalStackLayout>

                        <!-- Bookmark Icon -->
                        <AbsoluteLayout
                            x:Name="bookmark"
                            Grid.Row="2"
                            Grid.Column="2"
                            HorizontalOptions="End"
                            IsVisible="{ Binding IsPro, Converter={StaticResource NegateBooleanConverter} }">
                            <AbsoluteLayout.GestureRecognizers>
                                <TapGestureRecognizer
                                    Command="{ Binding ToggleFavoriteCommand, Source={x:Reference root} }"
                                    CommandParameter="{ Binding . }"
                                />
                            </AbsoluteLayout.GestureRecognizers>

                            <Label
                                Text="{ x:Static local:MaterialCommunityIconsFont.BookmarkOutline }"
                                FontSize="22"
                                HorizontalOptions="End"
                                FontFamily="{ StaticResource NewsIconsFontFamily }"
                                TextColor="{ DynamicResource TextTertiaryColor }">
                                <Label.Triggers>
                                    <DataTrigger
                                        Binding="{ Binding IsFavorite }"
                                        TargetType="Label"
                                        Value="True">
                                        <Setter Property="Opacity" Value="0" />
                                    </DataTrigger>
                                </Label.Triggers>
                            </Label>
                            <Label
                                Opacity="0"
                                Text="{ x:Static local:MaterialCommunityIconsFont.Bookmark }"
                                FontSize="22"
                                HorizontalOptions="End"
                                FontFamily="{ StaticResource NewsIconsFontFamily }"
                                TextColor="{ DynamicResource PrimaryColor }">
                                <Label.Triggers>
                                    <DataTrigger
                                        Binding="{ Binding IsFavorite }"
                                        TargetType="Label"
                                        Value="True">
                                        <Setter Property="Opacity" Value="1" />
                                    </DataTrigger>
                                </Label.Triggers>
                            </Label>
                        </AbsoluteLayout>

                        <Border
                            Grid.Row="2"
                            Grid.Column="2"
                            HorizontalOptions="End"
                            StrokeShape="RoundRectangle 8"
                            IsVisible="{ Binding IsPro }"
                            Style="{ DynamicResource BorderTagStyle }">
                            <Label
                                Text="Pro"
                                FontSize="{ StaticResource SmallFontSize }"
                                TextColor="{ DynamicResource TagItemTextColor }"
                                VerticalOptions="Center"
                            />
                        </Border>

                        <!-- SEPARATOR -->
                        <BoxView
                            IsVisible="{ Binding IsRelatedStory, Converter={StaticResource NegateBooleanConverter}, Source={x:Reference root} }"
                            Grid.Row="3"
                            Grid.ColumnSpan="2"
                            VerticalOptions="End"
                            Style="{ DynamicResource Horizontal1ptLineStyle }"
                            Margin="0,12,0,0"
                        />
                    </Grid>
                </ContentView>

                
                """;

            var xamlDocument = XDocument.Parse(xaml);

            var codeBuilder = new MauiReactorCodeBuilder(xamlDocument);

            var generatedCode = codeBuilder.GenerateCode();

            Assert.Pass();
        }

    }
}
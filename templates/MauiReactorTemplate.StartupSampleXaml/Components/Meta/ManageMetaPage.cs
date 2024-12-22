using MauiReactorTemplate.StartupSampleXaml.Data;
using MauiReactorTemplate.StartupSampleXaml.Framework;
using MauiReactorTemplate.StartupSampleXaml.Models;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;
using System.Text.RegularExpressions;

namespace MauiReactorTemplate.StartupSampleXaml.Components.Meta;

class ManageMetaPageState
{
    public List<Category> Categories { get; set; } = [];

    public List<Tag> Tags { get; set; } = [];
}


partial class ManageMetaPage : Component<ManageMetaPageState>
{
    [Inject]
    CategoryRepository _categoryRepository;

    [Inject]
    TagRepository _tagRepository;

    [Inject]
    SeedDataService _seedDataService;

    public override VisualNode Render()
    {
        return ContentPage("Manage Meta Page",

            ToolbarItem("Reset App")
                .OnClicked(Reset),

            RenderBody()
        )
        .OnAppearing(LoadData);
    }

    VisualNode RenderBody()
    {
        return VScrollView(
            VStack(
                Label("Categories")
                    .Style(ResourceHelper.GetResource<Style>("Title2")),

                VStack(
                    State.Categories.Select(RenderCategoryItem)
                    )                    
                    .Spacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing")),

                Grid("*", "*,Auto",
                    Button("Save")
                        .OnClicked(SaveCategories)
                        .HeightRequest(DeviceInfo.Current.Idiom == DeviceIdiom.Desktop ? 60 : 44),

                    Button()
                        .ImageSource(ResourceHelper.GetResource<ImageSource>("IconAdd"))
                        .OnClicked(AddCategory)
                        .GridColumn(1)
                )
                .Margin(0,10)
                .ColumnSpacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing")),

                Label("Tags")
                    .Style(ResourceHelper.GetResource<Style>("Title2")),

                VStack(
                    State.Tags.Select(RenderTagItem)
                    )
                    .Spacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing")),


                Grid("*", "*,Auto",
                    Button("Save")
                        .OnClicked(SaveTags)
                        .HeightRequest(DeviceInfo.Current.Idiom == DeviceIdiom.Desktop ? 60 : 44),

                    Button()
                        .ImageSource(ResourceHelper.GetResource<ImageSource>("IconAdd"))
                        .OnClicked(AddTag)
                        .GridColumn(1)
                )
                .Margin(0, 10)
                .ColumnSpacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"))

            )
            .Spacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"))
            .Padding(ResourceHelper.GetResource<OnIdiom<Thickness>>("LayoutPadding"))
        );

        /*
             <ScrollView>
        <VerticalStackLayout Spacing="{StaticResource LayoutSpacing}" Padding="{StaticResource LayoutPadding}">
            <Label Text="Categories" Style="{StaticResource Title2}"/>
            <VerticalStackLayout Spacing="{StaticResource LayoutSpacing}"
                BindableLayout.ItemsSource="{Binding Categories}">
                <BindableLayout.ItemTemplate>
                    <DataTemplate x:DataType="models:Category">
                        <Grid ColumnSpacing="{StaticResource LayoutSpacing}" ColumnDefinitions="4*,3*,30,Auto">
                            <Entry Text="{Binding Title}" Grid.Column="0"/>
                            <Entry Text="{Binding Color}" Grid.Column="1" x:Name="ColorEntry">
                                <Entry.Behaviors>
                                    <toolkit:TextValidationBehavior 
                                        InvalidStyle="{StaticResource InvalidEntryStyle}"
                                        Flags="ValidateOnUnfocusing"
                                        RegexPattern="^#(?:[0-9a-fA-F]{3}){1,2}$" />
                                </Entry.Behaviors>
                            </Entry>
                            <BoxView HeightRequest="30" WidthRequest="30" VerticalOptions="Center" 
                                Color="{Binding Text, Source={x:Reference ColorEntry}, x:DataType=Entry}" Grid.Column="2"/>
                            <Button 
                                ImageSource="{StaticResource IconDelete}"
                                Background="Transparent"
                                Command="{Binding DeleteCategoryCommand, Source={RelativeSource AncestorType={x:Type pageModels:ManageMetaPageModel}}, x:DataType=pageModels:ManageMetaPageModel}" CommandParameter="{Binding .}"
                                Grid.Column="3"/>
                        </Grid>
                    </DataTemplate>
                </BindableLayout.ItemTemplate>
            </VerticalStackLayout>

            <Grid ColumnSpacing="{StaticResource LayoutSpacing}" ColumnDefinitions="*,Auto" Margin="0,10">
                <Button Text="Save" Command="{Binding SaveCategoriesCommand}" HeightRequest="{OnIdiom 44,Desktop=60}" Grid.Column="0"/>
                <Button ImageSource="{StaticResource IconAdd}" Command="{Binding AddCategoryCommand}" Grid.Column="1"/>
            </Grid>

            <Label Text="Tags" Style="{StaticResource Title2}"/>
            <VerticalStackLayout Spacing="{StaticResource LayoutSpacing}"
                BindableLayout.ItemsSource="{Binding Tags}">
                <BindableLayout.ItemTemplate>
                    <DataTemplate x:DataType="models:Tag">
                        <Grid ColumnSpacing="{StaticResource LayoutSpacing}" ColumnDefinitions="4*,3*,30,Auto">
                            <Entry Text="{Binding Title}" Grid.Column="0"/>
                            <Entry Text="{Binding Color}" Grid.Column="1" x:Name="ColorEntry">
                                <Entry.Behaviors>
                                    <toolkit:TextValidationBehavior 
                                        InvalidStyle="{StaticResource InvalidEntryStyle}"
                                        Flags="ValidateOnUnfocusing"
                                        RegexPattern="^#(?:[0-9a-fA-F]{3}){1,2}$" />
                                </Entry.Behaviors>
                            </Entry>
                            <BoxView HeightRequest="30" WidthRequest="30" VerticalOptions="Center" 
                                Color="{Binding Text, Source={x:Reference ColorEntry}, x:DataType=Entry}" Grid.Column="2"/>
        
                            <Button 
                                ImageSource="{StaticResource IconDelete}"
                                Background="Transparent"
                                Command="{Binding DeleteTagCommand, Source={RelativeSource AncestorType={x:Type pageModels:ManageMetaPageModel}}, x:DataType=pageModels:ManageMetaPageModel}" CommandParameter="{Binding .}"
                                Grid.Column="3"/>
                        </Grid>
                    </DataTemplate>
                </BindableLayout.ItemTemplate>
            </VerticalStackLayout>

            <Grid ColumnSpacing="{StaticResource LayoutSpacing}" ColumnDefinitions="*,Auto" Margin="0,10">
                <Button Text="Save" Command="{Binding SaveTagsCommand}" HeightRequest="{OnIdiom 44,Desktop=60}" Grid.Column="0"/>
                <Button ImageSource="{StaticResource IconAdd}" Command="{Binding AddTagCommand}" Grid.Column="1"/>
            </Grid>
        </VerticalStackLayout>
    </ScrollView>
         */
    }

    VisualNode RenderCategoryItem(Category category)
    {
        /*
        <Grid ColumnSpacing="{StaticResource LayoutSpacing}" ColumnDefinitions="4*,3*,30,Auto">
            <Entry Text="{Binding Title}" Grid.Column="0"/>
            <Entry Text="{Binding Color}" Grid.Column="1" x:Name="ColorEntry">
                <Entry.Behaviors>
                    <toolkit:TextValidationBehavior 
                        InvalidStyle="{StaticResource InvalidEntryStyle}"
                        Flags="ValidateOnUnfocusing"
                        RegexPattern="^#(?:[0-9a-fA-F]{3}){1,2}$" />
                </Entry.Behaviors>
            </Entry>
            <BoxView HeightRequest="30" WidthRequest="30" VerticalOptions="Center" 
                Color="{Binding Text, Source={x:Reference ColorEntry}, x:DataType=Entry}" Grid.Column="2"/>
            <Button 
                ImageSource="{StaticResource IconDelete}"
                Background="Transparent"
                Command="{Binding DeleteCategoryCommand, Source={RelativeSource AncestorType={x:Type pageModels:ManageMetaPageModel}}, x:DataType=pageModels:ManageMetaPageModel}" CommandParameter="{Binding .}"
                Grid.Column="3"/>
        </Grid>
        */
        return Grid("*", "4*,3*,30,Auto",
            Entry()
                .Text(category.Title)
                .OnTextChanged(newText => category.Title = newText),

            ColorEntryWithValidation(category.Color, newColor => category.Color = newColor),

            BoxView()
                .HeightRequest(30)
                .WidthRequest(30)
                .VCenter()
                .Color(Color.Parse(category.Color))
                .GridColumn(2),

            Button()
                .ImageSource(ResourceHelper.GetResource<ImageSource>("IconDelete"))
                .Background(Colors.Transparent)
                .OnClicked(() => DeleteCategory(category))
                .GridColumn(3)
        )
        .ColumnSpacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"));
    }

    VisualNode RenderTagItem(Tag tag)
    {
        return Grid("*", "4*,3*,30,Auto",
            Entry()
                .Text(tag.Title)
                .OnTextChanged(newText => tag.Title = newText),

            ColorEntryWithValidation(tag.Color, newColor => tag.Color = newColor),

            BoxView()
                .HeightRequest(30)
                .WidthRequest(30)
                .VCenter()
                .Color(Color.Parse(tag.Color))
                .GridColumn(2),

            Button()
                .ImageSource(ResourceHelper.GetResource<ImageSource>("IconDelete"))
                .Background(Colors.Transparent)
                .OnClicked(() => DeleteTag(tag))
                .GridColumn(3)
        )
        .ColumnSpacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"));
    }

    static VisualNode ColorEntryWithValidation(string color, Action<string> onColorChanged)
    {
        Regex colorRegex = ColorValidationRegex();

        return Render<(string Color, bool Valid)>(state =>
            Entry()
                .Text(state.Value.Color)
                .OnTextChanged(newText =>
                {
                    if (colorRegex.IsMatch(newText))
                    {
                        onColorChanged(newText);
                        state.Set(_=> (newText, true));
                    }
                    else
                    {
                        state.Set(_ => (newText, true));
                    }
                })
                .When(!state.Value.Valid, _=>_.TextColor(Colors.Red))
                .GridColumn(1)

        , (color, true));
    }

    async Task LoadData()
    {
        var categoriesList = await _categoryRepository.ListAsync();
        var tagsList = await _tagRepository.ListAsync();

        SetState(s => 
        {
            s.Categories = categoriesList;
            s.Tags = tagsList;
        });
    }

    async Task DeleteCategory(Category category)
    {
        await _categoryRepository.DeleteItemAsync(category);
        await AppUtils.DisplayToastAsync("Category deleted");

        SetState(s => s.Categories.Remove(category));
    }

    async Task AddCategory()
    {
        var category = new Category();
        await _categoryRepository.SaveItemAsync(category);
        SetState(s => s.Categories.Add(category));
        await AppUtils.DisplayToastAsync("Category added");
    }

    async Task SaveCategories()
    {
        foreach (var category in State.Categories)
        {
            await _categoryRepository.SaveItemAsync(category);
        }

        await AppUtils.DisplayToastAsync("Categories saved");
    }

    async Task SaveTags()
    {
        foreach (var tag in State.Tags)
        {
            await _tagRepository.SaveItemAsync(tag);
        }

        await AppUtils.DisplayToastAsync("Tags saved");
    }

    async Task DeleteTag(Tag tag)
    {
        await _tagRepository.DeleteItemAsync(tag);
        SetState(s => s.Tags.Remove(tag));
        await AppUtils.DisplayToastAsync("Tag deleted");
    }

    async Task AddTag()
    {
        var tag = new Tag();
        await _tagRepository.SaveItemAsync(tag);

        SetState(s => s.Tags.Add(tag));

        await AppUtils.DisplayToastAsync("Tag added");
    }

    async Task Reset()
    {
        Preferences.Default.Remove("is_seeded");
        await _seedDataService.LoadSeedDataAsync();
        Preferences.Default.Set("is_seeded", true);
        await MauiControls.Shell.Current.GoToAsync("//main");
    }


    [GeneratedRegex("^#(?:[0-9a-fA-F]{3}){1,2}$")]
    private static partial Regex ColorValidationRegex();
}

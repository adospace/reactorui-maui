using Fonts;
using MauiReactorTemplate.StartupSampleXaml.Components.Main;
using MauiReactorTemplate.StartupSampleXaml.Components.Tasks;
using MauiReactorTemplate.StartupSampleXaml.Data;
using MauiReactorTemplate.StartupSampleXaml.Framework;
using MauiReactorTemplate.StartupSampleXaml.Models;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactorTemplate.StartupSampleXaml.Components.Projects;
class ProjectDetailPageState
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<Category> Categories { get; set; } = [];
    public List<Tag> Tags { get; set; } = [];
    public HashSet<Tag> SelectedTags { get; set; } = [];
    public HashSet<Tag> ProjectTags { get; set; } = [];
    public List<ProjectTask> ProjectTasks { get; set; } = [];
    public int CategoryID { get; set; }
    public string? Icon { get; set; }

}

class ProjectDetailPageProps
{
    public int? ProjectID { get; set; }
}

partial class ProjectDetailPage : Component<ProjectDetailPageState, ProjectDetailPageProps>
{
    [Inject]
    ProjectRepository _projectRepository;

    [Inject]
    CategoryRepository _categoryRepository;

    [Inject]
    TagRepository _tagRepository;

    [Inject]
    TaskRepository _taskRepository;

    public override VisualNode Render()
    {
        return ContentPage("Project",

            Props.ProjectID.HasValue ?
            ToolbarItem("Delete")
                .OnClicked(Delete)
                .Order(ToolbarItemOrder.Primary)
                .Priority(0)
                .IconImageSource(ResourceHelper.GetResource<ImageSource>("IconDelete")) : null,

            Grid(
                VScrollView(
                    VStack(
                        new SfTextInputLayout(
                            Entry()
                                .Text(State.Name)
                                .OnTextChanged(newText => State.Name = newText)
                        )
                        .Hint("Name"),
                        new SfTextInputLayout(
                            Entry()
                                .Text(State.Description)
                                .OnTextChanged(newText => State.Description = newText)
                        )
                        .Hint("Description"),
                        new SfTextInputLayout(
                            Picker()
                                .ItemsSource(State.Categories.Select(_=>_.Title).ToList())
                                .SelectedIndex(State.Categories.FindIndex(_=>_.ID == State.CategoryID))
                                .OnSelectedIndexChanged(newIndex => State.CategoryID = State.Categories[newIndex].ID)
                        )
                        .Hint("Category"),

                        Label("Icon")
                            .Style(ResourceHelper.GetResource<Style>("Title2")),

                        HScrollView(
                            HStack(spacing: ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"),
                                RenderIcons().ToArray()
                                )
                            ),

                        //Alternative with CollectionView
                        //CollectionView()
                        //    .ItemsSource([
                        //        FluentUI.ribbon_24_regular,
                        //        FluentUI.ribbon_star_24_regular,
                        //        FluentUI.trophy_24_regular,
                        //        FluentUI.badge_24_regular,
                        //        FluentUI.book_24_regular,
                        //        FluentUI.people_24_regular,
                        //        FluentUI.bot_24_regular
                        //    ], RenderIcon)
                        //    .SelectedItem(State.Icon)
                        //    .OnSelectionChanged(item => State.Icon = (string?)item.CurrentSelection.FirstOrDefault())
                        //    .ItemsLayout(
                        //        HorizontalLinearItemsLayout()
                        //            .ItemSpacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"))
                        //            )
                        //    .HeightRequest(44)
                        //    .Margin(0,0,0,15)
                        //    .SelectedItem(State.Icon)


                        Label("Tags")
                            .Style(ResourceHelper.GetResource<Style>("Title2")),

                        HScrollView(
                            HStack(spacing: ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"),
                                State.Tags
                                    .Select(RenderTag)
                                )
                            ),

                        Button("Save")
                            .HeightRequest(DeviceInfo.Current.Idiom == DeviceIdiom.Desktop ? 60 : 44)
                            .OnClicked(Save),

                        Grid(
                            Label("Tasks")
                                .Style(ResourceHelper.GetResource<Style>("Title2"))
                                .VCenter(),

                            ImageButton()
                                .Source(ResourceHelper.GetResource<ImageSource>("IconClean"))
                                .HEnd()
                                .VCenter()
                                .Aspect(Aspect.Center)
                                .WidthRequest(44)
                                .WidthRequest(44)
                                .IsVisible(State.ProjectTasks.Any(t => t.IsCompleted))
                                .OnClicked(CleanTasks)
                        )
                        .HeightRequest(44),

                        VStack(
                            State.ProjectTasks.Select(RenderTask)
                        )
                        .Spacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"))

                    )
                    .Spacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"))
                    .Padding(ResourceHelper.GetResource<OnIdiom<Thickness>>("LayoutPadding"))
                )                
            )            
        )
        .OnAppearing(Refresh);
    }

    IEnumerable<VisualNode> RenderIcons()
    {
        string[] icons = 
        [
            FluentUI.ribbon_24_regular,
            FluentUI.ribbon_star_24_regular,
            FluentUI.trophy_24_regular,
            FluentUI.badge_24_regular,
            FluentUI.book_24_regular,
            FluentUI.people_24_regular,
            FluentUI.bot_24_regular
        ];

        foreach (var icon in icons)
        {
            yield return RenderIcon(icon);
        }
    }

    VisualNode RenderIcon(string icon)
    {
        return Grid("Auto,4", "*",
            Label(icon)
                .FontFamily(FluentUI.FontFamily)
                .FontSize(24)
                .Center()
                .TextColor(Theme.IsLightTheme ?
                    ResourceHelper.GetResource<Color>("DarkOnLightBackground") :
                    ResourceHelper.GetResource<Color>("LightOnDarkBackground")),

            BoxView()
                .HeightRequest(4)
                .GridRow(1)
                .Color(ResourceHelper.GetResource<Color>("Primary"))
                .HFill()
                .IsVisible(State.Icon == icon)
        )
        .OnTapped(()=>SetState(s => s.Icon = icon))
        .RowSpacing(ResourceHelper.GetResource<double>("size60"));
    }

    VisualNode RenderTag(Tag tag)
    {
        return Border(
            Label(tag.Title)
                .TextColor(Theme.IsLightTheme ?
                    ResourceHelper.GetResource<Color>("DarkOnLightBackground") :
                    ResourceHelper.GetResource<Color>("LightOnDarkBackground"))
                .FontSize(DeviceInfo.Current.Idiom == DeviceIdiom.Desktop ? 18 : 16)
                .VerticalTextAlignment(TextAlignment.Center)
        )
        .HeightRequest(44)
        .StrokeThickness(0)
        .Background(Theme.IsLightTheme ?
            ResourceHelper.GetResource<Color>("LightSecondaryBackground") :
            ResourceHelper.GetResource<Color>("DarkSecondaryBackground"))
        .When(State.SelectedTags.Contains(tag), _ => _.Background(Color.FromArgb(tag.Color)))
        .Padding(18, 0, 18, 8).OnAndroid(_=>_.Padding(18, 0, 18, 0))
        .OnTapped(() => SetState(s =>
        {
            if (!s.SelectedTags.Remove(tag))
            {
                s.SelectedTags.Add(tag);
            }
        }));
    }

    TaskView RenderTask(ProjectTask task)
    {
        return new TaskView()
            .Task(task)
            .OnTaskCompletionChanged(Invalidate);
    }

    async Task Refresh()
    {
        var categories = await _categoryRepository.ListAsync();
        var tags = await _tagRepository.ListAsync();

        if (Props.ProjectID != null)
        {
            var project = await _projectRepository.GetAsync(Props.ProjectID.Value) ?? throw new InvalidOperationException();
            var projectTags = await _tagRepository.ListAsync(Props.ProjectID.Value);
            var projectTasks = await _taskRepository.ListAsync(Props.ProjectID.Value);

            SetState(s =>
            {
                s.Name = project.Name;
                s.Description = project.Description;
                s.Categories = categories;
                s.SelectedTags = [.. projectTags];
                s.ProjectTags = [.. projectTags];
                s.CategoryID = project.CategoryID;
                s.Icon = project.Icon;
                s.Tags = tags;
                s.ProjectTasks = projectTasks;
            });
        }
        else
        {
            SetState(s =>
            {
                s.Categories = categories;
                s.Tags = tags;
            });
        }
    }

    async Task Save()
    {
        var projectToSave = new Project
        {
            ID = Props.ProjectID.GetValueOrDefault(),
            Name = State.Name,
            Description = State.Description,
            CategoryID = State.CategoryID,
            Icon = State.Icon ?? FluentUI.ribbon_24_regular
        };

        await _projectRepository.SaveItemAsync(projectToSave);

        foreach (var tag in State.ProjectTags)
        {
            if (!State.SelectedTags.Contains(tag))
            {
                await _tagRepository.DeleteItemAsync(tag, projectToSave.ID);
            }
        }

        foreach (var tag in State.SelectedTags)
        {
            if (!State.ProjectTags.Contains(tag))
            {
                await _tagRepository.SaveItemAsync(tag, projectToSave.ID);
            }
        }

        await Navigation.PopAsync();

        await AppUtils.DisplayToastAsync("Project Saved!");
    }

    async Task Delete()
    {
        if (Props.ProjectID.HasValue)
        {
            await _projectRepository.DeleteItemAsync(Props.ProjectID.Value);
        }

        await Navigation.PopAsync();

        await AppUtils.DisplayToastAsync("Project deleted");
    }

    async Task CleanTasks()
    {
        var completedTasks = State.ProjectTasks.Where(t => t.IsCompleted).ToArray();
        foreach (var task in completedTasks)
        {
            await _taskRepository.DeleteItemAsync(task);
        }

        SetState(s => s.ProjectTasks.RemoveAll(t => t.IsCompleted));

        await AppUtils.DisplayToastAsync("All cleaned up!");
    }
}

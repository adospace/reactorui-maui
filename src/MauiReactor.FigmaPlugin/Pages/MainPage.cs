using FigmaSharp;
using FigmaSharp.Models;
using MauiReactor;
using MauiReactor.Canvas;
using MauiReactor.FigmaPlugin.Pages.Components;
using MauiReactor.FigmaPlugin.Resources.Styles;
using MauiReactor.FigmaPlugin.Services.UI;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MauiReactor.FigmaPlugin.Pages;

class MainPageState
{
    public FigmaDocument? Document { get; set; }

    public bool IsBusy { get; set; }

    public List<RecentProject> RecentProjects { get; set; } = new();
}

class MainPage : Component<MainPageState>
{
    protected override void OnMounted()
    {
        GetRecentProjects();

        base.OnMounted();
    }

    async void GetRecentProjects()
    {
        SetState(s => s.IsBusy = true);

        var recentProjectsJson = await SecureStorage.Default.GetAsync("recent_projects");

        if (!string.IsNullOrWhiteSpace(recentProjectsJson))
        {
            State.RecentProjects = JsonConvert.DeserializeObject<List<RecentProject>>(recentProjectsJson) ?? throw new InvalidOperationException();
        }

        SetState(s => s.IsBusy = false);
    }

    public override VisualNode Render()
    {
        return new Shell
        {
            new ContentPage
            {
                new MenuBarItem("File")
                {
                    new MenuFlyoutSubItem("Project")
                    {
                        new MenuFlyoutItem("Open...")
                            .OnClicked(OnOpenProject),

                        State.RecentProjects.Count > 0 ? new MenuFlyoutSeparator() : null,

                        State.RecentProjects.Select(_=> new  MenuFlyoutItem(_.Name).OnClicked(()=>OnOpenProject(_)))

                    },
                    new MenuFlyoutSeparator(),
                    new MenuFlyoutItem("Exit")
                        .OnClicked(OnExitApplication),
                },

                RenderBody()
            }
        }
        .WindowTitle("MauiReactor Figma Tool");
    }

    private VisualNode RenderBody()
    {
        return State.IsBusy ?
            new ActivityIndicator()
                .IsRunning(true)
            :
            new ResizableContainer
            {
                new TreeView()
                    .Document(State.Document),

                new Editor()
                    .FontFamily("CascadiaCodeRegular")
            }
            .Orientation(StackOrientation.Horizontal);
    }

    private async void OnOpenProject()
    {
        if (ContainerPage == null)
        {
            return;
        }

        SetState(s => s.IsBusy = true);

        string token = await SecureStorage.Default.GetAsync("figma_token");

        token ??= await ContainerPage.DisplayPromptAsync("Figma PAT", "Please enter Figma personal access token");

        if (string.IsNullOrWhiteSpace(token))
        {
            return;
        }

        await SecureStorage.Default.SetAsync("figma_token", token);

        string fileId = await ContainerPage.DisplayPromptAsync("Figma Prject Id", "Please enter the Figma project Id");
        
        if (string.IsNullOrWhiteSpace(fileId))
        {
            return;
        }

        var api = new FigmaApi(token);
        var fileResponse = await api.GetFileAsync(new FigmaFileQuery(fileId));

        var recentProject = new RecentProject(fileResponse.name, fileId);

        State.RecentProjects.RemoveAll(_ => _.Name != fileResponse.name);

        State.RecentProjects.Insert(0, new RecentProject(fileResponse.name, fileId));        
        
        await SecureStorage.Default.SetAsync("recent_projects", JsonConvert.SerializeObject(State.RecentProjects));

        SetState(s =>
        {
            s.Document = fileResponse.document;
            s.IsBusy = false;
        });
    }

    private async void OnOpenProject(RecentProject recentProject)
    {
        if (ContainerPage == null)
        {
            return;
        }

        SetState(s => s.IsBusy = true);

        string token = await SecureStorage.Default.GetAsync("figma_token");

        token ??= await ContainerPage.DisplayPromptAsync("Figma PAT", "Please enter Figma personal access token");

        if (string.IsNullOrWhiteSpace(token))
        {
            return;
        }

        await SecureStorage.Default.SetAsync("figma_token", token);

        var api = new FigmaApi(token);
        var fileResponse = await api.GetFileAsync(new FigmaFileQuery(recentProject.Id));

        SetState(s =>
        {
            s.Document = fileResponse.document;
            s.IsBusy = false;
        });
    }

    private void OnExitApplication()
    {
        Application.Current?.Quit();        
    }
}


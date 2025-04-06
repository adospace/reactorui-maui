using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

public class TestBug308State
{
    public bool IsAudioPlaying { get; set; }
}

public class TestBug308 : Component<TestBug308State>
{
    public override VisualNode Render()
    {
        return ContentPage(
            Button()
                .ImageSource(State.IsAudioPlaying ? "tab_home.png" : "tab_favorites.png")
                .Center()
                .OnClicked(()=>SetState(s => s.IsAudioPlaying = !s.IsAudioPlaying))
            );
    }
}

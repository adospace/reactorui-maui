using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiIcons.Core;
using MauiIcons.SegoeFluent;

namespace MauiReactor.TestApp.Pages;

public class TestBug308State
{
    public bool IsAudioPlaying { get; set; }
}

public class TestBug308 : Component<TestBug308State>
{
    static MauiControls.ImageSource? _pause;
    static MauiControls.ImageSource? _play;

    public override VisualNode Render()
    {
        _pause ??= SegoeFluentIcons.Pause.ToImageSource(iconColor: Colors.White);
        _play ??= SegoeFluentIcons.Play.ToImageSource(iconColor: Colors.White);

        return ContentPage(
            Button()
                .ImageSource(State.IsAudioPlaying
                                ? _pause
                                : _play)
                .Center()
                .OnClicked(()=>SetState(s => s.IsAudioPlaying = !s.IsAudioPlaying))
            );
    }
}

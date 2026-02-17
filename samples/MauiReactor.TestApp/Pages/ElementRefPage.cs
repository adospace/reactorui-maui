using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class ElementRefPage : Component
{
    private MauiControls.Entry? _entryRef;

    public override VisualNode Render()
        => ContentPage("Element Reference",
            VStack(spacing: 10,
                Entry(entryRef => _entryRef = entryRef)
                    .Text("Hi!")
                    .VCenter()
                    .HCenter(),

                Button("Focus Entry")
                    .BackgroundColor(Colors.Green)
                    .HCenter()
                    .OnClicked(()=> _entryRef?.Focus())
            )
        );    
}

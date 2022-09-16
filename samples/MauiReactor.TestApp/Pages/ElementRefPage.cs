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
    {
        return new ContentPage
        {
            new VStack(spacing: 10)
            {
                new Entry(entryRef => _entryRef = entryRef)
                    .Text("Hi!")
                    .VCenter()
                    .HCenter(),

                new Button("Focus Entry")
                    .OnClicked(()=> _entryRef?.Focus())
            }
        }
        .Title("Element Reference");
    }
}

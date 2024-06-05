using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class FormattedTextTestPage : Component
{
    public override VisualNode Render()
        => ContentPage("Formatted Text",
            VStack(
                Label()
                    .FormattedText(()=>
                    {
                        //of course FomattedText here, being static, can be created as a static variable and passed to Label().FormattedText(myStaticFormattedText)
                        FormattedString formattedString = new();
                        formattedString.Spans.Add(new Span { Text = "Red bold, ", TextColor = Colors.Red, FontAttributes = FontAttributes.Bold });

                        Span span = new() { Text = "default, " };
                        span.GestureRecognizers.Add(new MauiControls.TapGestureRecognizer { Command = new Command(async () => await ContainerPage!.DisplayAlert("Tapped", "This is a tapped Span.", "OK")) });
                        formattedString.Spans.Add(span);
                        formattedString.Spans.Add(new Span { Text = "italic small.", FontAttributes = FontAttributes.Italic, FontSize = 14 });

                        return formattedString;
                    })
            )
            .Spacing(10)
            .Center()
        );
}

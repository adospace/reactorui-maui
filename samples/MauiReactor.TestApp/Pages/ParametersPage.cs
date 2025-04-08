using MauiReactor.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class CustomParameter
{
    public int Numeric { get; set; }
}

partial class ParametersPage : Component
{
    [Param]
    IParameter<CustomParameter> _customParameter;


    public override VisualNode Render()
    {
        return new ContentPage("Parameters Sample")
        {
            new VStack(spacing: 10)
            {
                new Button("Increment from parent", () => _customParameter.Set(_=>_.Numeric += 1))
                .AutomationId("Increment_Button"),
                new Label(_customParameter.Value.Numeric)
                .AutomationId("Increment_Label"),

                new Button("Open child page", ()=> Navigation?.PushAsync<ParameterChildComponent>())
            }
            .VCenter()
            .HCenter()
        };
    }
}

partial class ParameterChildComponent : Component
{
    [Param]
    IParameter<CustomParameter> _customParameter;

    public override VisualNode Render()
    {
        return new ContentPage
        {
            new VStack(spacing: 10)
            {
                new Button("Increment from child", ()=> _customParameter.Set(_ => _.Numeric+=1)),

                new Label(_customParameter.Value.Numeric),
            }
        };
    }
}

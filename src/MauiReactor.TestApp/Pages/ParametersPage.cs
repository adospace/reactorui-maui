using MauiReactor.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages
{
    public class CustomParameter
    {
        public int Numeric { get; set; }
    }

    internal class ParametersPage : Component
    {
        private readonly IParameter<CustomParameter> _customParameter;

        public ParametersPage()
        {
            _customParameter = CreateParameter<CustomParameter>();
        }

        public override VisualNode Render()
        {
            return new ContentPage("Parameters Sample")
            {
                new VStack(spacing: 10)
                {
                    new Button("Increment from parent", () => _customParameter.Set(_=>_.Numeric += 1   )),
                    new Label(_customParameter.Value.Numeric),

                    new ParameterChildComponent()
                }
                .VCenter()
                .HCenter()
            };
        }
    }


    partial class ParameterChildComponent : Component
    {
        public override VisualNode Render()
        {
            var customParameter = GetParameter<CustomParameter>();

            return new VStack(spacing: 10)
            {
                new Button("Increment from child", ()=> customParameter.Set(_=>_.Numeric++)),

                new Label(customParameter.Value.Numeric),
            };
        }
    }

}

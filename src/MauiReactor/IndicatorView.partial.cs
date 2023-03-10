using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

partial interface IIndicatorView
{
    PropertyValue<IEnumerable?>? ItemsSource { get; set; }

    PropertyValue<DataTemplate?>? IndicatorTemplate { get; set; }

    VisualStateGroupList VisualStateGroups { get; set; }
}

partial class IndicatorView<T>
{
    PropertyValue<IEnumerable?>? IIndicatorView.ItemsSource { get; set; }

    PropertyValue<DataTemplate?>? IIndicatorView.IndicatorTemplate { get; set; }

    VisualStateGroupList IIndicatorView.VisualStateGroups { get; set; } = new VisualStateGroupList();

    partial void OnBeginUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIIndicatorView = (IIndicatorView)this;

        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.IndicatorView.ItemsSourceProperty, thisAsIIndicatorView.ItemsSource);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.IndicatorView.IndicatorTemplateProperty, thisAsIIndicatorView.IndicatorTemplate);
    }
}

public static partial class IndicatorViewExtensions
{
    public static T ItemsSource<T>(this T indicatorView, IEnumerable? itemsSource) where T : IIndicatorView
    {
        indicatorView.ItemsSource = new PropertyValue<IEnumerable?>(itemsSource);
        return indicatorView;
    }

    public static T IndicatorTemplate<T>(this T indicatorView, Func<VisualNode>? template) where T : IIndicatorView
    {
        indicatorView.IndicatorTemplate = new PropertyValue<DataTemplate?>(() =>
        {
            if (template == null)
            {
                return null;
            }

            return new DataTemplate(()=>
            {
                var root = template.Invoke();
                var itemTemplateHost = new TemplateHost<VisualElement>(root);
                VisualStateManager.SetVisualStateGroups(itemTemplateHost.NativeElement, indicatorView.VisualStateGroups.Clone());
                return itemTemplateHost.NativeElement;
            });
        });
        return indicatorView;
    }

    public static T VisualState<T>(this T indicatorView, string groupName, string stateName, BindableProperty property, object value) where T : IIndicatorView
    {
        var group = indicatorView.VisualStateGroups.FirstOrDefault(_ => _.Name == groupName);

        if (group == null)
        {
            indicatorView.VisualStateGroups.Add(group = new VisualStateGroup()
            {
                Name = groupName
            });
        }

        var state = group.States.FirstOrDefault(_ => _.Name == stateName);
        if (state == null)
        {
            group.States.Add(state = new VisualState { Name = stateName });
        }

        state.Setters.Add(new Setter() { Property = property, Value = value });

        return indicatorView;
    }
}


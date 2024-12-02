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

    VisualStateGroupList IndicatorVisualStateGroups { get; set; }
}

partial class IndicatorView<T>
{
    PropertyValue<IEnumerable?>? IIndicatorView.ItemsSource { get; set; }

    PropertyValue<DataTemplate?>? IIndicatorView.IndicatorTemplate { get; set; }

    VisualStateGroupList IIndicatorView.IndicatorVisualStateGroups { get; set; } = [];

    protected override void OnUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIIndicatorView = (IIndicatorView)this;

        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.IndicatorView.ItemsSourceProperty, thisAsIIndicatorView.ItemsSource);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.IndicatorView.IndicatorTemplateProperty, thisAsIIndicatorView.IndicatorTemplate);
        base.OnUpdate();
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
                var itemTemplateHost = new TemplateHost(root);
                var nativeElement = (VisualElement?)itemTemplateHost.NativeElement ?? throw new InvalidOperationException();
                //VisualStateManager.SetVisualStateGroups(nativeElement, indicatorView.VisualStateGroups.Clone());
                indicatorView.IndicatorVisualStateGroups.SetToVisualElement(nativeElement);
                return itemTemplateHost.NativeElement;
            });
        });
        return indicatorView;
    }

    public static T IndicatorVisualState<T>(this T indicatorView, string groupName, string stateName, BindableProperty? property = null, object? value = null, string? targetName = null) where T : IIndicatorView
    {
        indicatorView.IndicatorVisualStateGroups ??= [];

        indicatorView.IndicatorVisualStateGroups.Set(groupName, stateName, property, value, targetName);

        //indicatorView.IndicatorVisualStateGroups.TryGetValue(groupName, out var group);

        //if (group == null)
        //{
        //    indicatorView.IndicatorVisualStateGroups.Add(groupName, group = []);
        //}

        //group.TryGetValue(stateName, out var state);
        //if (state == null)
        //{
        //    group.Add(stateName, state = []);
        //}

        //state.Add(new VisualStatePropertySetter(property, value, targetName));

        return indicatorView;
    }

    public static T IndicatorVisualState<T>(this T indicatorView, VisualStateGroupList visualState) where T : IIndicatorView
    {
        indicatorView.IndicatorVisualStateGroups = visualState;

        return indicatorView;
    }

    //public static T VisualState<T>(this T indicatorView, string groupName, string stateName, BindableProperty property, object value) where T : IIndicatorView
    //{
    //    var group = indicatorView.VisualStateGroups.FirstOrDefault(_ => _.Name == groupName);

    //    if (group == null)
    //    {
    //        indicatorView.VisualStateGroups.Add(group = new VisualStateGroup()
    //        {
    //            Name = groupName
    //        });
    //    }

    //    var state = group.States.FirstOrDefault(_ => _.Name == stateName);
    //    if (state == null)
    //    {
    //        group.States.Add(state = new VisualState { Name = stateName });
    //    }

    //    state.Setters.Add(new Setter() { Property = property, Value = value });

    //    return indicatorView;
    //}
}


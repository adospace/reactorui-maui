using System;
using System.Collections;
using Microsoft.Maui.Controls;

namespace MauiReactor.TestApp.Controls.Native;

/// <summary>
/// Minimal custom control with ItemsSource and ItemTemplate properties,
/// used to test scaffold generation with <c>implementItemTemplate: true</c>.
/// </summary>
public class TestItemsControl : Microsoft.Maui.Controls.View
{
    /// <summary>
    /// When true, setting <see cref="ItemTemplate"/> throws to simulate controls
    /// that require MAUI handlers for template processing (e.g., PanCardView.CardsView).
    /// This reproduces the crash path where <c>OnUpdate</c> fails before
    /// <c>base.OnUpdate()</c> can apply queued properties like AutomationId.
    /// </summary>
    public static bool SimulateHandlerDependency { get; set; }

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(TestItemsControl)
    );

    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
        nameof(ItemTemplate),
        typeof(DataTemplate),
        typeof(TestItemsControl),
        propertyChanged: OnItemTemplateChanged
    );

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public DataTemplate? ItemTemplate
    {
        get => (DataTemplate?)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    private static void OnItemTemplateChanged(
        BindableObject bindable,
        object? oldValue,
        object? newValue
    )
    {
        if (SimulateHandlerDependency && newValue != null)
        {
            throw new InvalidOperationException(
                "TestItemsControl: simulated handler-dependent failure. "
                    + "Real controls like CardsView fail here because they require "
                    + "MAUI handlers for internal template processing."
            );
        }
    }
}

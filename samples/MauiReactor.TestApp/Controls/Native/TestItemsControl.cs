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
    /// <summary>The message of the exception <see cref="SimulateHandlerDependency"/> causes.</summary>
    public const string SimulatedFailureMessage =
        "TestItemsControl: simulated handler-dependent failure.";

    /// <summary>
    /// When true, setting a non-null <see cref="ItemTemplate"/> throws, as controls that need a
    /// MAUI handler for template processing (e.g., PanCardView.CardsView) do when none is attached.
    /// </summary>
    public bool SimulateHandlerDependency { get; set; }

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
        if (((TestItemsControl)bindable).SimulateHandlerDependency && newValue != null)
        {
            throw new InvalidOperationException(SimulatedFailureMessage);
        }
    }
}

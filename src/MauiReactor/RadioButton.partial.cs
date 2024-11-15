using MauiReactor.Internals;

namespace MauiReactor;

public partial interface IRadioButton
{
    PropertyValue<string?>? ContentString { get; set; }
}

public partial class RadioButton<T> : IRadioButton
{
    PropertyValue<string?>? IRadioButton.ContentString { get; set; }


    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is View view)
        {
            NativeControl.Content = view;
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is View)
        {
            NativeControl.Content = null;
        }

        base.OnRemoveChild(widget, childControl);
    }


    partial void OnEndUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIRadioButton = (IRadioButton)this;

        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.RadioButton.ContentProperty, thisAsIRadioButton.ContentString);
    }
}

public partial class RadioButton : RadioButton<Microsoft.Maui.Controls.RadioButton>
{
    public RadioButton(string? content)
    {
        this.Content(content);
    }

    public RadioButton(string? content, Action<Microsoft.Maui.Controls.RadioButton?> componentRefAction) : base(componentRefAction)
    {
        this.Content(content);
    }
}

public static partial class RadioButtonExtensions
{
    public static T Content<T>(this T radioButton, string? value)
        where T : IRadioButton
    {
        radioButton.ContentString = new PropertyValue<string?>(value);
        return radioButton;
    }

    public static T OnCheckedChanged<T>(this T radioButton, Action? checkedAction = null, Action? uncheckedAction = null)
        where T : IRadioButton
    {
        radioButton.CheckedChangedActionWithArgs = new Action<object?, CheckedChangedEventArgs>((sender, args) =>
        {
            if (args.Value)
            {
                checkedAction?.Invoke();
            }
            else
            {
                uncheckedAction?.Invoke();
            }
        });

        return radioButton;
    }
}

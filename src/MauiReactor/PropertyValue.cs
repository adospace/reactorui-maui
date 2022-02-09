using MauiReactor.Internals;

namespace MauiReactor
{
    public interface IPropertyValue
    {
        bool SetDefault { get; }

        object? GetValue();

        Action GetValueAction(BindableObject dependencyObject, BindableProperty dependencyProperty);

        bool HasValueFunction { get; }
    }

    public class PropertyValue<T> : IPropertyValue
    {
        private PropertyValue()
        {
            SetDefault = true;
        }
        public PropertyValue(T value)
        {
            Value = value;
        }

        public PropertyValue(Func<T> valueAction)
        {
            ValueFunc = valueAction ?? throw new ArgumentNullException(nameof(valueAction));

            Value = valueAction();
        }

        public static IPropertyValue Default { get; } = new PropertyValue<T>();

        public T? Value { get; }

        public Func<T>? ValueFunc { get; }

        public bool SetDefault { get; }

        public bool HasValueFunction => ValueFunc != null;

        public object? GetValue() => Value;

        public override string ToString()
        {
            return $"{{{(Value == null ? "null" : Value.ToString())}}}";
        }

        public Action GetValueAction(BindableObject dependencyObject, BindableProperty dependencyProperty)
        {
            Validate.EnsureNotNull(ValueFunc);

            return () =>
            {
                var newValue = ValueFunc();
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"{dependencyObject.GetType()} set property {dependencyProperty.PropertyName} to {newValue}");
#endif
                dependencyObject.SetValue(dependencyProperty, newValue);
            };
        }
    }

}

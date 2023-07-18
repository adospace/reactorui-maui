using MauiReactor.Internals;
using System.Diagnostics.CodeAnalysis;

namespace MauiReactor.Parameters
{
    public interface IParameter
    {
        string Name { get; }

        object GetValue();
    }

    public interface IParameter<T> : IParameter where T : new()
    {
        T Value { get; }

        void Set(Action<T> setAction, bool invalidateComponent = true);
    }

    internal class Parameter<T> : IParameter<T> where T : new()
    {
        private readonly HashSet<WeakReference<Component>> _parameterReferences = new();

        public Parameter(string name)
        {
            Name = name;
        }

        public string Name { get; }

        private readonly T _value = new();

        public T Value => _value ?? throw new InvalidOperationException();

        object IParameter.GetValue() => Value!;

        public void Set(Action<T> setAction, bool invalidateComponent = true)
        {
            setAction(_value);

            if (invalidateComponent)
            {
                foreach (var componentOfReferencedParameter in _parameterReferences.ToList())
                {
                    if (componentOfReferencedParameter.TryGetTarget(out var componentOfReferencedParameterValue))
                    {
                        componentOfReferencedParameterValue.InvalidateComponent();
                    }
                    else
                    {
                        _parameterReferences.Remove(componentOfReferencedParameter);
                    }
                }
            }
        }

        public void RegisterReference(Component reference)
        {
            foreach (var componentOfReferencedParameter in _parameterReferences.ToList())
            {
                if (componentOfReferencedParameter.TryGetTarget(out var componentOfReferencedParameterValue))
                {
                    if (reference == componentOfReferencedParameterValue)
                    {
                        return;
                    }
                }
                else
                {
                    _parameterReferences.Remove(componentOfReferencedParameter);
                }
            }

            _parameterReferences.Add(new WeakReference<Component>(reference));
        }
    }


    public sealed class ParameterContext
    {
        private static readonly Dictionary<string, IParameter> _parameters = new();

        public Component Component { get; }

        internal ParameterContext(Component component)
        {
            Component = component;
        }

        public IParameter<T> Create<T>(string? name = null) where T : new()
        {
            name ??= typeof(T).FullName ?? throw new InvalidOperationException();
            _parameters.TryGetValue(name, out var parameter);

            if (parameter == null)
            {
                var newParameterT = new Parameter<T>(name);
                _parameters[name] = newParameterT;
                newParameterT.RegisterReference(Component);
                return newParameterT;
            }
            else
            {
                if (parameter is IParameter<T> parameterT)
                {
                    ((Parameter<T>)parameterT).RegisterReference(Component);
                    return parameterT;
                }

                var newParameterT = new Parameter<T>(name);

                _parameters[newParameterT.Name] = newParameterT;

                CopyObjectExtensions.CopyProperties(parameter.GetValue(), newParameterT.Value!);

                newParameterT.RegisterReference(Component);

                return newParameterT;
            }
        }

        public IParameter<T>? Get<T>(string? name = null) where T : new()
        {
            name ??= typeof(T).FullName ?? throw new InvalidOperationException();

            if (!_parameters.TryGetValue(name, out var parameter))
            {
                return null;
            }

            if (parameter is IParameter<T> parameterT)
            {
                ((Parameter<T>)parameterT).RegisterReference(Component);
                return parameterT;
            }

            var newParameterT = new Parameter<T>(name);

            _parameters[newParameterT.Name] = newParameterT;

            CopyObjectExtensions.CopyProperties(parameter.GetValue(), newParameterT.Value!);

            newParameterT.RegisterReference(Component);

            return newParameterT;
        }

        //internal IParameter<T> Register<T>(IParameterWithReferences<T> parameterWithReferences, string? name = null) where T : new()
        //{
        //    name ??= typeof(T).FullName ?? throw new InvalidOperationException();
        //    var parameter = new ParameterReference<T>(this, parameterWithReferences);
        //    _parameters[name] = parameter;
        //    return parameter;
        //}
    }
}

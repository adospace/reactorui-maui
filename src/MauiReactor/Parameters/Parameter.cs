﻿using MauiReactor.Internals;
using Microsoft.Extensions.Logging;
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
        private readonly HashSet<WeakReference<Component>> _parameterReferences = [];

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

    public sealed class ParameterContext : IDisposable
    {
        private static readonly AsyncLocal<Dictionary<string, IParameter>?> _testingParameters = new();
        private static readonly Dictionary<string, IParameter> _sharedParameters = new();

        private static Dictionary<string, IParameter> Parameters =>
            _testingParameters.Value ?? _sharedParameters;

        public Component? Component { get; }

        /// <summary>
        /// this construction is for use with testing so that parameters can be isolated
        /// </summary>
        public ParameterContext()
        {
            _testingParameters.Value = new Dictionary<string, IParameter>();
        }

        internal ParameterContext(Component component)
        {
            Component = component;
        }

        public IParameter<T> Create<T>(string? name = null) where T : new()
        {
            if (Component == null)
            {
                throw new InvalidOperationException("ParameterContext must be created with a Component");
            }

            name ??= typeof(T).FullName ?? throw new InvalidOperationException();
            Parameters.TryGetValue(name, out var parameter);

            if (parameter == null)
            {
                var newParameterT = new Parameter<T>(name);
                Parameters[name] = newParameterT;
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
                Parameters[newParameterT.Name] = newParameterT;

                if (MauiReactorFeatures.HotReloadIsEnabled)
                {
                    CopyObjectExtensions.CopyProperties(parameter.GetValue(), newParameterT.Value!);
                }
                else
                {
                    var logger = ServiceCollectionProvider.ServiceProvider?.GetService<ILogger<ParameterContext>>();
                    logger?.LogWarning("Unable to forward component Props from type {Props}", 
                        parameter.GetValue().GetType().FullName);
                }

                newParameterT.RegisterReference(Component);
                return newParameterT; 

            }
        }

        public IParameter<T>? Get<T>(string? name = null) where T : new()
        {
            if (Component == null)
            {
                throw new InvalidOperationException("ParameterContext must be created with a Component");
            }

            name ??= typeof(T).FullName ?? throw new InvalidOperationException();

            if (!Parameters.TryGetValue(name, out var parameter))
            {
                return null;
            }

            if (parameter is IParameter<T> parameterT)
            {
                ((Parameter<T>)parameterT).RegisterReference(Component);
                return parameterT;
            }

            var newParameterT = new Parameter<T>(name);

            Parameters[newParameterT.Name] = newParameterT;

            if (MauiReactorFeatures.HotReloadIsEnabled)
            {
                CopyObjectExtensions.CopyProperties(parameter.GetValue(), newParameterT.Value!);
            }
            else
            {
                var logger = ServiceCollectionProvider.ServiceProvider?.GetService<ILogger<ParameterContext>>();
                logger?.LogWarning("Unable to forward component Props from type {Props}", 
                    parameter.GetValue().GetType().FullName);
            }            

            newParameterT.RegisterReference(Component);

            return newParameterT;
        }

        public void Dispose()
        {
            _testingParameters.Value = null;
        }
    }
}

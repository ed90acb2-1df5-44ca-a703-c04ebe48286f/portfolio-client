using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Portfolio.Core.DependencyInjection
{
    public class Container : IContainer
    {
        private readonly Dictionary<Type, object> _instances = new();
        private readonly Dictionary<Type, Blueprint> _blueprints = new();
        private readonly Dictionary<Type, object[]> _parametersCache = new();
        private readonly Stack<Type> _dependencyResolverStack = new();

        public void Singleton<TImplementation>(TImplementation implementation) where TImplementation : class
        {
            _instances.Add(typeof(TImplementation), implementation);
        }

        public void Singleton<TImplementation>() where TImplementation : class
        {
            Register<TImplementation, TImplementation>(Lifetime.Singleton);
        }

        public void Singleton<TContract, TImplementation>(TImplementation implementation) where TImplementation : class, TContract
        {
            _instances.Add(typeof(TContract), implementation);
        }

        public void Singleton<TContract, TImplementation>() where TImplementation : TContract
        {
            Register<TContract, TImplementation>(Lifetime.Singleton);
        }

        public void Transient<TContract, TImplementation>() where TImplementation : TContract
        {
            Register<TContract, TImplementation>(Lifetime.Transient);
        }

        public TContract Resolve<TContract>() where TContract : class
        {
            _dependencyResolverStack.Clear();

            var type = _blueprints[typeof(TContract)].Type;

            return (TContract) Resolve(type);
        }

        public T Instantiate<T>()
        {
            _dependencyResolverStack.Clear();

            var type = typeof(T);

            return (T) Instantiate(type, GetConstructorParameters(type));
        }

        private static ParameterInfo[] GetConstructorParameters(Type type)
        {
            return type.GetConstructors().Single().GetParameters();
        }

        private void Register<TContract, TImplementation>(Lifetime lifetime)
        {
            var implementationType = typeof(TImplementation);
            var implementationParameters = GetConstructorParameters(implementationType);

            _blueprints.Add(typeof(TContract), new Blueprint(lifetime, implementationType, implementationParameters));
        }

        private object Instantiate(Type type, IReadOnlyList<ParameterInfo> parameters)
        {
            if (!_parametersCache.TryGetValue(type, out var args))
            {
                args = new object[parameters.Count];
                _parametersCache.Add(type, args);
            }

            for (var i = 0; i < parameters.Count; i++)
            {
                args[i] = Resolve(parameters[i].ParameterType);
            }

            var instance = Activator.CreateInstance(type, args);

            return instance;
        }

        private object Resolve(Type type)
        {
            if (_instances.TryGetValue(type, out var instance))
            {
                return instance;
            }

            if (_dependencyResolverStack.Contains(type))
            {
                throw new Exception($"Circular dependency detected: {_dependencyResolverStack.Peek()} -> {string.Join(" -> ", _dependencyResolverStack.Reverse())}");
            }

            _dependencyResolverStack.Push(type);

            var blueprint = _blueprints[type];

            instance = Instantiate(type, blueprint.Parameters);

            if (blueprint.Lifetime == Lifetime.Singleton)
            {
                _instances.Add(type, instance);
            }

            _dependencyResolverStack.Pop();

            return instance;
        }

        private enum Lifetime
        {
            Singleton,
            Transient,
        }

        private readonly struct Blueprint
        {
            public readonly Lifetime Lifetime;
            public readonly Type Type;
            public readonly ParameterInfo[] Parameters;

            public Blueprint(Lifetime lifetime, Type type, ParameterInfo[] parameters)
            {
                Lifetime = lifetime;
                Type = type;
                Parameters = parameters;
            }
        }
    }
}

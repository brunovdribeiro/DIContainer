using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DIContainer.BasicDI.Exceptions;

namespace DIContainer.BasicDI
{
    public sealed class Container
    {
        private readonly List<TypeInformation> _myTypes;
        private readonly List<Type> _usedType;

        public Container()
        {
            _myTypes = new List<TypeInformation>();
            _usedType = new List<Type>();
        }

        public void RegisterSingleton<TImplementation>()
        {
            Register<TImplementation>(LifeCicle.Singleton);
        }

        public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
        {
            Register<TInterface, TImplementation>(LifeCicle.Singleton);
        }

        public void RegisterTransient<TImplementation>()
        {
            Register<TImplementation>(LifeCicle.Transient);
        }

        public void RegisterTransient<TInterface, TImplementation>() where TImplementation : TInterface
        {
            Register<TInterface, TImplementation>(LifeCicle.Transient);
        }

        private void Register<TImplementation>(LifeCicle lifeCicle)
        {
            var objType = typeof(TImplementation);

            CheckIfAlreadyRegistered(objType);

            if (IsAbstractionOrInterface(objType))
                throw new AbstractionOrInterfaceException();

            _myTypes.Add(new TypeInformation(objType, lifeCicle));
        }

        private void Register<TInterface, TImplementation>(LifeCicle lifeCicle) where TImplementation : TInterface
        {
            var interfaceType = typeof(TInterface);

            CheckIfAlreadyRegistered(interfaceType);

            _myTypes.Add(new TypeInformation(interfaceType, typeof(TImplementation), lifeCicle));
        }


        public int Registrations()
        {
            return _myTypes.Count;
        }

        public T GetInstance<T>()
        {
            return (T) GetInstance(typeof(T));
        }

        object GetInstance(Type type)
        {
            var typeInformation = GetType(type);

            if (typeInformation == null)
                throw new UnregisteredDependencyException();

            CheckCircularReference(type);

            var constructor = typeInformation.ImplementationType.GetConstructors().FirstOrDefault();
            var parameters = constructor.GetParameters().Select(x => GetInstance(x.ParameterType)).ToArray();

            return typeInformation.GetInstance(parameters);
        }

        void CheckIfAlreadyRegistered(Type type)
        {
            var typeToInitiate = GetType(type);

            if (typeToInitiate != null)
                throw new AlreadyRegisteredException();
        }

        TypeInformation GetType(Type type)
        {
            if (type.IsAbstract || type.IsInterface)
                return _myTypes.FirstOrDefault(
                    x => x.InterfaceType != null && x.InterfaceType.FullName == type.FullName);

            return _myTypes.FirstOrDefault(x =>
                x.ImplementationType.FullName == type.FullName && x.InterfaceType == null);
        }

        bool IsAbstractionOrInterface(Type type)
        {
            return type.IsAbstract || type.IsInterface;
        }

        void CheckCircularReference(Type type)
        {
            if (_usedType.Any(x => x == type))
                throw new CircularDependencyException();

            _usedType.Add(type);

            var constructor = type.GetConstructors().FirstOrDefault();
            if (constructor is not null)
            {
                var parameters = constructor.GetParameters();

                foreach (var parameter in parameters)
                {
                    CheckCircularReference(parameter.ParameterType);
                }
            }

            _usedType.Remove(type);
        }
    }
}
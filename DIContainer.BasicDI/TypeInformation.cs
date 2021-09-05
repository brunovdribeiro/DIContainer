using System;

namespace DIContainer.BasicDI
{
    public class TypeInformation
    {
        public TypeInformation(Type implementationType, LifeCicle lifeCicle)
        {
            ImplementationType = implementationType;
            LifeCicle = lifeCicle;
            InterfaceType = null;
            Instance = null;
        }

        public TypeInformation(Type interfaceType, Type implementationType, LifeCicle lifeCicle)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementationType;
            LifeCicle = lifeCicle;
            Instance = null;
        }

        public Type InterfaceType { get; }
        public Type ImplementationType { get; }
        public LifeCicle LifeCicle { get; }
        public object Instance { get; private set; }

        public void SetInstance(object obj)
        {
            Instance = obj;
        }

        public bool HaveToInitiate()
        {
            return Instance == null || LifeCicle == LifeCicle.Transient;
        }

        public object GetInstance(object[] parameters)
        {
            if (HaveToInitiate())
                SetInstance(Activator.CreateInstance(ImplementationType, parameters));

            return Instance;
        }
    }
}
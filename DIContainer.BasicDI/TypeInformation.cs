using System;

namespace DIContainer.BasicDI
{
    public class TypeInformation
    {
        public Type InterfaceType { get; private set; }
        public Type ImplementationType { get; private set; }
        public LifeCicle LifeCicle { get; private set; }
        public object Instance { get; private set; }

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
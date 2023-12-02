namespace Portfolio.Core.DependencyInjection
{
    public interface IContainer
    {
        void Singleton<TImplementation>(TImplementation implementation) where TImplementation : class;

        void Singleton<TImplementation>() where TImplementation : class;

        void Singleton<TContract, TImplementation>(TImplementation implementation) where TImplementation : class, TContract;

        void Singleton<TContract, TImplementation>() where TImplementation : TContract;

        void Transient<TContract, TImplementation>() where TImplementation : TContract;

        TContract Resolve<TContract>() where TContract : class;

        T Instantiate<T>();
    }
}

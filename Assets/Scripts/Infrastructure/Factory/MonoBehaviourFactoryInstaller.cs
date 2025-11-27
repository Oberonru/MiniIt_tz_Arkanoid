using Zenject;

namespace Infrastructure.Factory
{
    public abstract class MonoBehaviourFactoryInstaller<TInterface, TEntity> : MonoInstaller
        where TInterface : class, IMonoBehaviourFactory
        where TEntity : class, TInterface
    {
        public override void InstallBindings()
        {
            Container.Bind<TInterface>().To<TEntity>().AsSingle();
        }
    }
}
using UnityEngine;

namespace Infrastructure.Installers
{
    //Это для UI, для сцен контекста
    public class BaseObjectInstallerFromHierarchy<T> : BaseObjectInstaller where T : MonoBehaviour
    {
        public override void InstallBindings()
        {
            if (AsSingle)
                Container.Bind<T>().FromComponentInHierarchy().AsSingle();
            else Container.Bind<T>().FromComponentInHierarchy();
        }
    }
}
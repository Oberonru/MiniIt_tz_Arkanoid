using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProviderInstaller<T> : MonoInstaller where T : Object
    {
        [SerializeField] private T[] _elements = new T[0];

        public override void InstallBindings()
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                 Container.Bind(_elements[i].GetType()).
                    FromInstance(_elements[i]);

                 if (_elements[i] is IInitializable initializable)
                 {
                     initializable.Initialize();
                 }
            }
        }
    }
}
using UnityEngine;
using Zenject;

namespace Core.Game.Installer
{
    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _prefab;
        [SerializeField] private bool _dontDestroyOnLoad;
        
        public override void InstallBindings()
        {
            var newObject = Container.InstantiatePrefabForComponent<GameManager>(_prefab);

            Container.Bind<IGameStateProvider>().FromInstance(newObject).AsSingle();

            Container.Bind<GameManager>().FromInstance(newObject).AsSingle();

            if (_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(newObject.gameObject);
            }
        }
    }
}   
using System;
using System.Collections.Generic;
using Infrastructure.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace VFX.Factory
{
    public class VFXObjectFactory : IVFXObjectFactory, IDisposable
    {
        [Inject] private DiContainer _di;
        [Inject] private VFXFactoryConfig _config;
        public IObservable<Unit> OnSpawn => _onSpawn;
        private Subject<Unit> _onSpawn = new();

        private Dictionary<int, PoolMono<VFXObject>> _dictionary;

        public VFXObjectFactory()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }
        
        public IVFXObject Create(VFXObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<int, PoolMono<VFXObject>>();
            }

            if (!_dictionary.TryGetValue(prefab.GetInstanceID(), out PoolMono<VFXObject> poolMono))
            {
                poolMono = new PoolMono<VFXObject>(prefab, null, _di, _config.StartCount, true);
                _dictionary.Add(prefab.GetInstanceID(), poolMono);
            }

            var effect = poolMono.GetFreeElement();

            if (parent != null)
            {
                effect.transform.SetParent(parent);
            }
            effect.transform.SetPositionAndRotation(position, rotation);

            _onSpawn.OnNext(Unit.Default);

            return effect;
        }

        private void OnActiveSceneChanged(Scene olsdSceen, Scene newSceen)
        {
            Cleanup();
        }

        private void Cleanup()
        {
            if (_dictionary == null) return;
            
            
        }

        public void Dispose()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }
    }
}
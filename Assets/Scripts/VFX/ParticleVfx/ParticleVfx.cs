using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Utils;
using UnityEngine;

namespace VFX.ParticleVfx
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleVfx : VFXObject, IPoolable<ParticleVfx>
    {
        [SerializeField] protected ParticleSystem _particle;
        private PoolMono<ParticleVfx> _pool;
        private float _lifeTime = 1f;

        private void OnValidate()
        {
            if (_particle == null) _particle = GetComponent<ParticleSystem>();
        }

        public override async void PlayAnimation()
        {
            _particle.Play();
            await WaitForEnd();
            Disable();
        }

        public override void Disable()
        {
            if (_pool != null)
            {
                _pool.ReturnToPool(this);
            }
            else if (this != null && gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }

        private async UniTask WaitForEnd()
        {
            while (_particle != null && _particle.IsAlive())
            {
                await UniTask.Yield();
            }
        }

        public void OnCreated(PoolMono<ParticleVfx> pool)
        {
            _pool = pool;
        }

        public void OnTakenFromPool()
        {
            throw new NotImplementedException();
        }

        public void OnReturnedToPool()
        {
            throw new NotImplementedException();
        }
    }
}
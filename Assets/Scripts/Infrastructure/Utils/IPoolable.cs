using UnityEngine;

namespace Infrastructure.Utils
{
    public interface IPoolable<T> where T : MonoBehaviour
    {
        void OnCreated(PoolMono<T> pool);
        void OnTakenFromPool();
        void OnReturnedToPool();
    }
}
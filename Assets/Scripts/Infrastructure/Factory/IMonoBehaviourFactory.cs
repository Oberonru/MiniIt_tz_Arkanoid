using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IMonoBehaviourFactory : IFactoryObject
    {
    }

    public interface IMonoBehaviourFactory<TPrefab, TReturn> : IMonoBehaviourFactory where TPrefab : MonoBehaviour where TReturn : class, IFactoryObject
    {
        IObservable<Unit> OnSpawn { get; }
        TReturn Create(TPrefab prefab, Vector3 position, Quaternion rotation, [CanBeNull] Transform parent = null);
    }
}
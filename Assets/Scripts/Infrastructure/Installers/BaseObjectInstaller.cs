using UnityEngine;
using Zenject;

public abstract class BaseObjectInstaller : MonoInstaller
{
    [SerializeField] private bool _asSingle = true;

    public bool AsSingle => _asSingle;
}
using UniRx;
using UnityEngine;

namespace Core.BaseComponents
{
    [RequireComponent(typeof(HealthComponent))]
    public class StateComponentHandler : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        private IStateComponent[] _stateComponents;

        private void OnEnable()
        {
            _health.OnDestroyed.
                Take(1).
                Subscribe(_ =>
            {
                DisableAllComponents();
            }).AddTo(this);
        }

        private void OnValidate()
        {
            if (_health == null) _health = GetComponent<HealthComponent>();
        }

        private void Start()
        {
            _stateComponents = GetComponentsInChildren<IStateComponent>();
        }

        public void DisableAllComponents()
        {
            if (_stateComponents == null) return;
            
            foreach (var component in _stateComponents)
            {
                component?.Disable();
            }
        }

        public void EnableAllComponents()
        {
            if (_stateComponents == null) return;

            foreach (var component in _stateComponents)
            {
                component?.Enable();
            }
        }
    }
}
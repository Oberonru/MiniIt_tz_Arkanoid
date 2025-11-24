using Core.BaseComponents;
using Core.Configs.Platform;
using Core.Platform.Components;
using UnityEngine;
using Zenject;

namespace Core.Platform
{
    [RequireComponent(typeof(PlatformController))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(StateComponentHandler))]
    public class PlatformInstance : MonoBehaviour, IStateComponent
    {
        [Inject] private PlatformConfig _config;
        [SerializeField] PlatformController controller;
        [SerializeField] HealthComponent _health;
        [SerializeField] StateComponentHandler _stateHandler;
        public Transform Transform => transform;

        public PlatformConfig Stats => _config;
        public PlatformController Controller => controller;
        public HealthComponent Health => _health;
        public StateComponentHandler StateHandler => _stateHandler;

        private void OnValidate()
        {
            if (controller == null) controller = GetComponent<PlatformController>();
            if (_health == null) _health = GetComponent<HealthComponent>();
            if (_stateHandler == null) _stateHandler = GetComponent<StateComponentHandler>();
        }

        private void Awake()
        {
            _health.Init(_config.MaxHealth);
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }
    }
}
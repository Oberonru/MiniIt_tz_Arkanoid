using Core.Ball.Components;
using Core.Configs.Ball;
using UnityEngine;
using Zenject;

namespace Core.Ball
{
    [RequireComponent(typeof(BallController))]
    public class BallInstance : MonoBehaviour
    {
        [Inject] private BallConfig _config;
        [SerializeField] private BallController _controller;
        public Transform Transform => transform;

        public BallConfig Stats => _config;
        public BallController Controller => _controller;

        private MonoBehaviour[] _components;

        private void OnValidate()
        {
            if (_controller == null) _controller = GetComponent<BallController>();
        }

        public void Enable()
        {
            if (_components == null) _components = GetComponents<MonoBehaviour>();
           
            foreach (var component in _components)
            {
                component.enabled = true;
            }
        }

        public void Disable()
        {
            if (_components == null) _components = GetComponents<MonoBehaviour>();
           
            foreach (var component in _components)
            {
                component.enabled = false;
            }
        }
    }
}
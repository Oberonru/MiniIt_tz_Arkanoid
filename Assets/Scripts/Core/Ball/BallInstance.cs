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
        public BallController BallController => _controller;

        private void OnValidate()
        {
            if  (_controller == null) _controller = GetComponent<BallController>();
        }
    }
}
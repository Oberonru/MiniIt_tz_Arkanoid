using Core.Ball;
using Infrastructure.Configs;
using UnityEngine;

namespace Core.Configs.Ball
{
    [CreateAssetMenu(menuName = "Config/Ball/BallConfig", fileName = "BallConfig")]
    public class BallConfig : ScriptableConfig
    {
        [Header("Movement Settings")]
        [SerializeField] private float _speed = 1f;
        [SerializeField] private BallStateType _ballStateType = BallStateType.Waiting;
        [SerializeField] private float _verticalVelocity = 10f;
        [SerializeField] private float _maxAngle = 60f;
        [SerializeField] private float _startEpsilon = 0.05f;
        [SerializeField] private float _maxSpeed = 15f;

        [Header("Acceleration Settings")]
        [SerializeField] private int _platformLoopCount = 5;
        [SerializeField] private float _platformAcceleration = 0.2f;
        [SerializeField] private int _brickLoopCount = 10;
        [SerializeField] private float _brickAcceleration = 0.1f;
        [SerializeField] private float _minVerticalVelocity = 0.2f;
        [SerializeField] private float _horizontalJitter = 0.1f;
        [SerializeField] private float _minAngle = 10f;
        
        public float Speed => _speed;
        public BallStateType StateType => _ballStateType;
        public float VerticalVelocity => _verticalVelocity;
        public float MaxAngle => _maxAngle;
        public float StartEpsilon => _startEpsilon;
        public float MaxSpeed => _maxSpeed;

        public int PlatformLoopCount => _platformLoopCount;
        public float PlatformAcceleration => _platformAcceleration;

        public int BrickLoopCount => _brickLoopCount;
        public float BrickAcceleration => _brickAcceleration;
        public float MinVerticalVelocity => _minVerticalVelocity;
        public float HorizontalJitter => _horizontalJitter;
        public float MinAngle => _minAngle;
    }
}
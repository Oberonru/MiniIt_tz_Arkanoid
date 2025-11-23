using Core.Ball;
using Infrastructure.Configs;
using UnityEngine;

namespace Core.Configs.Ball
{
    [CreateAssetMenu(menuName = "Config/Ball/BallConfig", fileName = "BallConfig")]
    public class BallConfig : ScriptableConfig
    {
        [Header("Movement Settings")]
        [SerializeField] private float _speed = 8f;
        [SerializeField] private BallStateType _ballStateType = BallStateType.Waiting;
        [SerializeField] private float _verticalVelocity = 10f;
        [SerializeField] private float _maxAngle = 60f;    
        [SerializeField] private float _startEpsilon = 0.05f;  

        [Header("Anti-Loop Settings")]
        [SerializeField] private int _positionMemorySize = 50;
        [SerializeField] private int _repeatThreshold = 3;
        [SerializeField] private float _positionTolerance = 0.05f;

        public float Speed => _speed;
        public BallStateType StateType
        {
            get => _ballStateType;
            set => _ballStateType = value;
        }

        public float VerticalVelocity => _verticalVelocity;
        public float MaxAngle => _maxAngle;
        public float StartEpsilon => _startEpsilon;

        public int PositionMemorySize => _positionMemorySize;
        public int RepeatThreshold => _repeatThreshold;
        public float PositionTolerance => _positionTolerance;
    }
}
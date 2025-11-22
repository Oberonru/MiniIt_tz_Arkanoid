using Core.Ball;
using Infrastructure.Configs;
using UnityEngine;

namespace Core.Configs.Ball
{
    [CreateAssetMenu(menuName = "Config/Ball/BallConfig", fileName = "BallConfig")]
    public class BallConfig : ScriptableConfig
    {
        [SerializeField] private float _speed = 8f;
        [SerializeField] private BallStateType _ballStateType = BallStateType.Waiting;
        [SerializeField] private float _verticalVelocity = 10f;

        [SerializeField] private float _maxAngle = 60f;    
        [SerializeField] private float _startEpsilon = 0.05f;  

        public float Speed => _speed;
        public BallStateType StateType
        {
            get => _ballStateType;
            set => _ballStateType = value;
        }
        
        public float VerticalVelocity => _verticalVelocity;
        public float MaxAngle => _maxAngle;
        public float StartEpsilon => _startEpsilon;
    }
}
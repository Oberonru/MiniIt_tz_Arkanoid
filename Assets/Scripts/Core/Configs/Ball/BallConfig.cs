using Core.Ball;
using Infrastructure.Configs;
using UnityEngine;

namespace Core.Configs.Ball
{
    [CreateAssetMenu(menuName = "Config/Ball/BallConfig", fileName = "BallConfig")]
    public class BallConfig : ScriptableConfig
    {
        [SerializeField] private float _speed;
        [SerializeField] private BallStateType _ballStateType = BallStateType.Waiting;

        public float Speed => _speed;
        public BallStateType StateType
        {
            get
            {
                return _ballStateType;
            }
            set => _ballStateType = value;
        }
    }
}
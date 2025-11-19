using Infrastructure.Configs;
using UnityEngine;

namespace Core.Configs.Platform
{
    [CreateAssetMenu(menuName = "Config/Platform/PlatformConfig", fileName = "PlatformConfig")]
    public class PlatformConfig : ScriptableConfig
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _speed;
        
        public int MaxHealth => _maxHealth;
        public float Speed => _speed;
    }
}
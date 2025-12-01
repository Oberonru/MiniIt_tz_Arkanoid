using Infrastructure.Configs;
using UnityEngine;

namespace VFX
{
    [CreateAssetMenu(menuName = "VFX/Config/VFXFactoryConfig", fileName = "VFXFactoryConfig")]

    public class VFXFactoryConfig : ScriptableConfig
    {
        [SerializeField] private int _startCount;
        
        public int StartCount => _startCount;
    }
}
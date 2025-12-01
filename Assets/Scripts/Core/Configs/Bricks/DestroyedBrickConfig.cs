using Infrastructure.Configs;
using UnityEngine;
using VFX.FloatingText;
using VFX.ParticleVfx;

namespace Core.Configs.Bricks
{
    [CreateAssetMenu(menuName = "Config/Brick/DestroyedBrickConfig", fileName = "DestroyedBrickConfig")]

    public class DestroyedBrickConfig : ScriptableConfig
    {
        [SerializeField] private ParticleVfx _particleVfx;
        [SerializeField] private FloatingText _floatingText;
        
        public ParticleVfx ParticleVfx => _particleVfx;
        public FloatingText FloatingText => _floatingText;
    }
}
using Core.Audio;
using Core.BaseComponents;
using Core.Bricks.Model;
using Core.Configs.Audio;
using Core.Configs.Bricks;
using UniRx;
using UnityEngine;
using VFX.Factory;
using VFX.FloatingText;
using Zenject;

namespace Core.Bricks
{
    [RequireComponent(typeof(HealthComponent))]
    public class Brick : MonoBehaviour
    {
        [Inject] private IAudioHandler _audioHandler;
        [Inject] private AudioClipsConfig _audioConfig;
        [Inject] private IVFXObjectFactory _factory;
        [Inject] private DestroyedBrickConfig _config;
        [SerializeField] private Sprite _damagedSprite;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private BrickType _brickType = BrickType.Brick;
        public BrickType BrickType => _brickType;
        public HealthComponent HealthComponent => _healthComponent;

        private SpriteRenderer _spriteRenderer;

        public int Reward => _reward;
        private int _reward;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _healthComponent.OnHealthChanged.Subscribe(current =>
            {
                if (_brickType == BrickType.Brick)
                {
                    _audioHandler?.PlaySfx(_audioConfig.BrickHits);
                    var particle = _factory.Create(_config.ParticleVfx,
                        transform.position + new Vector3(0, 0, -1),
                        transform.rotation);
                    particle.PlayAnimation();
                }

                else if (_brickType == BrickType.Wall)
                {
                    _audioHandler?.PlaySfx(_audioConfig.WallHit);
                }

                if (current == 1)
                    _spriteRenderer.sprite = _damagedSprite;
            }).AddTo(this);

            _healthComponent.OnDestroyed.Take(1).Subscribe(_ =>
            {
                _audioHandler?.PlaySfx(_audioConfig.DestroyClip);
                Destroy(gameObject);

                //Testing Floating vfx effect
                // var text = _factory.Create(_destroyedBrickConfig.FloatingText, transform.position,
                //         transform.rotation)
                //     as FloatingText;
                // if (text != null)
                // {
                //     text.SetTextInfo(_reward.ToString(), transform.position);
                //     text.SetColor(Color.yellow);
                // }
            }).AddTo(this);
        }

        private void OnValidate()
        {
            if (_healthComponent == null) _healthComponent = GetComponent<HealthComponent>();
        }

        public void Init(int health, Sprite damagedSprite, int reward, BrickType brickType)
        {
            _healthComponent.Init(health);
            _damagedSprite = damagedSprite;
            _reward = reward;
            _brickType = brickType;
        }
    }
}
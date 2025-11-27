using Core.Audio;
using Core.BaseComponents;
using Core.Bricks.Model;
using Core.Configs.Audio;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Bricks
{
    [RequireComponent(typeof(HealthComponent))]
    public class Brick : MonoBehaviour
    {
        [Inject] private IAudioHandler _audioHandler;
        [Inject] private AudioClipsConfig _config;
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
                    _audioHandler?.PlaySfx(_config.BrickHits);
                }

                else if (_brickType == BrickType.Wall)
                {
                    _audioHandler?.PlaySfx(_config.WallHit);
                }

                if (current == 1)
                    _spriteRenderer.sprite = _damagedSprite;
            }).AddTo(this);

            _healthComponent.OnDestroyed.Take(1).Subscribe(_ =>
            {
                _audioHandler?.PlaySfx(_config.DestroyClip);
                Destroy(gameObject);
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
using Core.Audio;
using Core.BaseComponents;
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
        public HealthComponent HealthComponent => _healthComponent;

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _healthComponent.OnHit.Subscribe(_ =>
            {
                _audioHandler?.PlaySfx(_config.BrickHits);
                _spriteRenderer.sprite = _damagedSprite;
            }).AddTo(this);
            
            _healthComponent.OnDead.Take(1).Subscribe(_ =>
            {
                _audioHandler?.PlaySfx(_config.DestroyClip);
                Destroy(gameObject);
            }).AddTo(this);
        }

        private void OnValidate()
        {
            if (_healthComponent == null) _healthComponent = GetComponent<HealthComponent>();
        }

        public void Init(int health, Sprite damagedSprite)
        {
            _healthComponent.Init(health);
            _damagedSprite = damagedSprite;
        }
    }
}
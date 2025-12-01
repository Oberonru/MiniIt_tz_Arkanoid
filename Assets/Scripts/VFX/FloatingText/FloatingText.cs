using Infrastructure.Utils;
using TMPro;
using UnityEngine;

namespace VFX.FloatingText
{
    public class FloatingText : VFXObject, IPoolable<FloatingText>
    {
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private float _lifeTime = 1.5f;

        private PoolMono<FloatingText> _pool;
        private float _timer;
        private Vector3 _offset = new Vector3(0f, 1f, 0f);

        private void OnValidate()
        {
            if (_text == null) _text = GetComponentInChildren<TextMeshPro>();
        }

        public void SetTextInfo(string info, Vector3 worldPosition)
        {
            transform.position = worldPosition + _offset;
            gameObject.SetActive(true);
            _text.text = info;
            _timer = _lifeTime;
        }

        public void SetColor(Color color)
        {
            _text.color = color;
        }

        private void Update()
        {
            if (!gameObject.activeSelf) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                Disable();
            }
        }

        public override void Disable()
        {
            if (_pool != null)
                _pool.ReturnToPool(this);
            else
                gameObject.SetActive(false);
        }

        public void OnCreated(PoolMono<FloatingText> pool)
        {
            _pool = pool;
        }

        public void OnTakenFromPool()
        {
        }

        public void OnReturnedToPool()
        {
            _text.text = string.Empty;
        }
    }
}
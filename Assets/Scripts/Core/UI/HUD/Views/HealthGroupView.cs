using System.Collections.Generic;
using Core.Platform;
using UniRx;
using UnityEngine;

namespace Core.UI.HUD.Views
{
    public class HealthGroupView : MonoBehaviour
    {
        [SerializeField] private HealthView _prefab;
        [SerializeField] private PlatformInstance _platform;

        private List<HealthView> _views = new();

        private void OnEnable()
        {
            _platform.Health.OnHealthChanged.Subscribe(value =>
            {
                ShowCurrentHealth(value);
            }).AddTo(this);
        }
        
        private void OnValidate()
        {
            if (_platform == null) _platform = FindObjectOfType<PlatformInstance>();
        }

        public void Start()
        {
            for (var i = 0; i < _platform.Health.MaxHealth; i++)
            {
                var heart = Instantiate(_prefab, transform);
                heart.transform.SetParent(transform, false);

                SetVisible(heart, false);

                _views.Add(heart);
            }

            ShowCurrentHealth(_platform.Health.CurrentHealth);
        }

        public void ShowCurrentHealth(int currentHealth)
        {
            for (var i = 0; i < _views.Count; i++)
            {
                SetVisible(_views[i], i < currentHealth);
            }
        }

        private void SetVisible(HealthView view, bool visible)
        {
            view.gameObject.SetActive(visible);
        }
    }
}
using System;
using UnityEngine;
using UniRx;

namespace Core.BaseComponents
{
    public class HealthComponent : MonoBehaviour
    {
        public IObservable<Unit> OnHit => _hit;
        private Subject<Unit> _hit = new();

        public IObservable<int> OnHealthChanged => _changed;
        private Subject<int> _changed = new();

        public IObservable<Unit> OnDead => _dead;
        private Subject<Unit> _dead = new();

        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        private int _maxHealth;

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                var current = Mathf.Clamp(value, 0, MaxHealth);
                _currentHealth = current;
            }
        }

        private int _currentHealth;

        private void OnDestroy()
        {
            _hit?.OnCompleted();
            _changed?.OnCompleted();
            _dead?.OnCompleted();
        }

        public void Init(int health)
        {
            _maxHealth = health;
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0) return;

            CurrentHealth -= damage;
            _hit?.OnNext(Unit.Default);
            _changed?.OnNext(_currentHealth);

            if (CurrentHealth <= 0) _dead?.OnNext(Unit.Default);
        }

        public void Heal(int heal)
        {
            if (heal <= 0) return;
            
            CurrentHealth += heal;
            _changed?.OnNext(_currentHealth);
        }
    }
}
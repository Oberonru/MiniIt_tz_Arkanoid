using System;
using Core.UI.Screens;
using UniRx;
using UnityEngine;

namespace Core.Gameplay
{
    public class LoseZone : UIScreen
    {
        public IObservable<Unit> OnLose => _onLose;
        private Subject<Unit> _onLose = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            _onLose?.OnNext(Unit.Default);
        }
    }
}
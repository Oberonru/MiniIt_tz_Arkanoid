using System;
using System.Collections.Generic;
using Core.Bricks;
using Core.UI.View;
using UniRx;
using UnityEngine;

namespace Core.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScoreView _view;
        public IObservable<Unit> OnWin => _win;
        private Subject<Unit> _win = new();

        private List<Brick> _bricks = new();

        public void RegisterBrick(Brick brick)
        {
            if (brick == null) return;

            _bricks.Add(brick);

            brick.HealthComponent.OnDestroyed.Subscribe(_ =>
            {
                _view.AddScore(brick.Reward);
                _bricks.Remove(brick);
                
                if (_bricks.Count == 0)
                {
                    _win.OnNext(Unit.Default);
                }
            }).AddTo(this);
        }
    }
}
using System;
using System.Collections.Generic;
using Core.Bricks;
using Core.UI.View;
using UniRx;
using UnityEngine;

namespace Core.Game
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        [SerializeField] private ScoreView _view;
        public GameState State => _state;
        private GameState _state = GameState.Playing;
        public IObservable<bool> OnPaused => _onPaused;
        private Subject<bool> _onPaused = new();
        public IObservable<Unit> OnWin => _win;
        private Subject<Unit> _win = new();

        private List<Brick> _bricks = new();

        private void OnValidate()
        {
            if (_view == null) _view = FindObjectOfType<ScoreView>();
        }

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
                    _state = GameState.Win;
                    _win.OnNext(Unit.Default);
                }
            }).AddTo(this);
        }

        public void Pause()
        {
            if (State == GameState.Paused) return;
            
            _state = GameState.Paused;
            Time.timeScale = 0;
            _onPaused.OnNext(true);
            
            print("PAUSED");
        }
        
        public void Play()
        {
            if  (State == GameState.Playing) return;
            
            _state = GameState.Playing;
            Time.timeScale = 1;
            _onPaused.OnNext(false);

        }
    }
}
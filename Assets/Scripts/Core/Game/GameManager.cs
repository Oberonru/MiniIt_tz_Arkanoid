using System.Collections.Generic;
using Core.Bricks;
using Core.Gameplay;
using Core.UI.Handlers;
using Core.UI.Model;
using Core.UI.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Game
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private IScreenHandler _screenHandler;
        [SerializeField] private LoseZone _loseZone;
        [SerializeField] private ScoreView _view;

        private List<Brick> _bricks = new();

        private void OnEnable()
        {
            _loseZone.OnLose.Subscribe(_ => Lose())
                .AddTo(this);
        }
        
        public void RegisterBrick(Brick brick)
        {
            if (brick == null) return;

            _bricks.Add(brick);
            
            brick.HealthComponent.OnDestroyed.
                Subscribe(_ => _view.AddScore(brick.Reward)).AddTo(this);
        }

        private void Lose()
        {
            _screenHandler.SetScreen(ScreenType.LoseScreen);
        }
    }
}
using Core.Ball;
using Core.Game;
using Core.Gameplay;
using Core.Platform;
using Core.UI.Handlers;
using Core.UI.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Handlers
{
    public class LevelStateHandler : MonoBehaviour
    {
        [Inject] private IScreenHandler _screenHandler;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private LoseZone _loseZone;
        [SerializeField] private PlatformInstance _platform;
        [SerializeField] private BallInstance _ball;

        private void OnEnable()
        {
            _loseZone.OnLose.Subscribe(_ =>
                {
                    _platform.Health.TakeDamage(1);
                    ResetPosition();
                })
                .AddTo(this);

            _platform.Health.OnDestroyed.Take(1).Subscribe(_ => _screenHandler.SetScreen(ScreenType.LoseScreen))
                .AddTo(this);

            _gameManager.OnWin.Take(1).Subscribe(_ =>
            {
                _screenHandler.SetScreen(ScreenType.WinScreen);
                ResetPosition();

                _platform.StateHandler.DisableAllComponents();
            }).AddTo(this);
        }

        private void ResetPosition()
        {
            _platform.Controller.Reset();
            _ball.Controller.Reset();
            _ball.Disable();
            _ball.Enable();
        }
    }
}
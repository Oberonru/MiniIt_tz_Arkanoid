using Core.Gameplay;
using Core.UI.Handlers;
using Core.UI.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Game
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private IScreenHandler _screenHandler;
        [SerializeField] private LoseZone _loseZone;

        private void OnEnable()
        {
            _loseZone.OnLose.
                Subscribe(_ => Lose())
                .AddTo(this);
        }

        private void Lose()
        {
            _screenHandler.SetScreen(ScreenType.LoseScreen);
        }
    }
}
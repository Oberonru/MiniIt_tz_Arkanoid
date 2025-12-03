using Core.Game;
using UnityEngine;
using Zenject;

namespace Core.UI.Buttons
{
    public class PlayGameBtn : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;

        public void PlayGame()
        {
            _gameManager.Play();
        }
    }
}
using Core.Game;
using UnityEngine;
using Zenject;

namespace Core.UI.Buttons
{
    public class PauseBtn : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;

        public void PauseGame()
        {
            _gameManager.Pause();
        }
    }
}
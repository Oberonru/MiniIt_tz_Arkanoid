using Core.Services;
using Core.UI.Model;
using UnityEngine;
using Zenject;

namespace Core.UI.Buttons
{
    public class Restart : MonoBehaviour
    {
        [Inject] private SceneLoaderService _loader;

        public void RestartGame()
        {
            _loader.LoadScene(nameof(SceneName.Level1));
        }
    }
}
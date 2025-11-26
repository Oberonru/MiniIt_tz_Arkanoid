using Core.Services;
using Core.UI.Model;
using UnityEngine;
using Zenject;

namespace Core.UI.Buttons
{
    public class Start : MonoBehaviour
    {
        [Inject] private SceneLoaderService _loader;

        public void StartGame()
        {
            _loader.LoadScene(nameof(SceneName.Level1));
        }
    }
}
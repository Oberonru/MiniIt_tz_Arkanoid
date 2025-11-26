using Core.Services;
using Core.UI.Model;
using UnityEngine;
using Zenject;

namespace Core.UI.Buttons
{
    public class BackToMenu : MonoBehaviour
    {
        [Inject] private SceneLoaderService _loader;
        public void Back()
        {
            _loader.LoadScene(nameof(SceneName.MainMenu));
        }
    }
}
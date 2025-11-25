using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.UI.Screens
{
    public class MainMenuScreen : UIScreen
    {
        [SerializeField] private UIButton _startButton;
        [SerializeField] private UIButton _settingsButton;
        [SerializeField] private UIButton _quitButton;

        private void OnEnable()
        {
        }

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
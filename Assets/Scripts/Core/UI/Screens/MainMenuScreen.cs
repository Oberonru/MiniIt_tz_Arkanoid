using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace Core.UI.Screens
{
    public class MainMenuScreen : UIScreen
    {
        [SerializeField] private UIButton _startButton;
        [SerializeField] private UIButton _settingsButton;
        [SerializeField] private UIButton _quitButton;
    }
}
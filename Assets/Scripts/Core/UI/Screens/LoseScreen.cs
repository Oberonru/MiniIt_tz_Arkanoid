using Core.Services;
using Core.UI.Model;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.UI.Screens
{
    public class LoseScreen : UIScreen
    {
        [Inject] private SceneLoaderService _loader;
        
        [SerializeField] private TextMeshProUGUI _heading;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _mainMenu;

        private void OnEnable()
        {
            _restart.onClick.AsObservable().
                Subscribe(_ => _loader.LoadScene(SceneName.Level1.ToString())).
                AddTo(this);

            _mainMenu.onClick.AsObservable().
                Subscribe(_ => _loader.LoadScene(SceneName.MainMenu.ToString()))
                .AddTo(this);
        }
    }
    
}
using Core.Services;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.UI.Handlers
{
    public class UIInputHintViewHandler : MonoBehaviour
    {
        [Inject] private InputHintService _hintService;

        [SerializeField] private TextMeshProUGUI _inputText;
        [SerializeField] private Image _icon;

        private void OnEnable()
        {
            _hintService.OnControlsChanged.Subscribe(changeType => { Refresh(); }).AddTo(this);
        }

        private void Refresh()
        {
            var moveAction = _hintService.GetMoveActionFromCurrentDevice();

            if (_hintService.IsKeyboard)
            {
                var displayName = _hintService.GetDisplayName(moveAction);
                _inputText.text = displayName;
                _icon.enabled = false;
            }
            else
            {
                _inputText.text = "";
                _icon.enabled = true;
                _icon.sprite = _hintService.GetIcon(moveAction);
            }
        }
    }
}
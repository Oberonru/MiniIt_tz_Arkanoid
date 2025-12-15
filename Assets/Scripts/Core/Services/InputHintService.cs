using System;
using Core.Configs.Input;
using Core.Input.Data;
using Core.UI;
using Infrastructure.Services;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Services
{
    [CreateAssetMenu(menuName = "Services/InputHintService", fileName = "InputHintService")]
    public class InputHintService : ScriptableService
    {
        [Inject] private IconSetConfig _iconSetConfig;
        [SerializeField] private InputHintConfig _config;

        public bool IsKeyboard
        {
            get { return _isKeyboard; }
        }

        public IObservable<InputHintType> OnControlsChanged => _controlsChanged;
        private Subject<InputHintType> _controlsChanged = new();

        private PlayerInput _input;
        private bool _isKeyboard;
        private IconResolver _resolver;

        public void SwitchToKeyboard()
        {
            _isKeyboard = true;
            _controlsChanged.OnNext(InputHintType.Text);
        }

        //метод для вызова в меню для смены устройства, выбор геймпада
        public void SwitchToGamepad()
        {
            _isKeyboard = false;
            _controlsChanged.OnNext(InputHintType.Icon);
        }

        public string GetDisplayName(InputActionReference inputAction)
        {
            return inputAction.action.GetBindingDisplayString();
        }

        public Sprite GetIcon(InputActionReference inputAction)
        {
            var binding = inputAction.action.bindings[0];
            return _resolver.ResolveIcon(binding);
        }

        public void SetPlayerInput(PlayerInput playerInput)
        {
            _input = playerInput;
            _input.onControlsChanged += HandleControlChanged;

            _resolver = new IconResolver(_iconSetConfig);
        }
        
        //Возвращает какой сейчас инпут экшен выбран в текстовом виде
        //Потом чтобы в юай отображать название клавиш текстом
        public InputActionReference GetMoveActionFromCurrentDevice()
        {
            return _isKeyboard ? _config.MoveHint : _config.MoveGamepadHint;
        }

        private void HandleControlChanged(PlayerInput input)
        {
            _isKeyboard = input.currentControlScheme != "Gamepad";

            var type = _isKeyboard ? InputHintType.Text : InputHintType.Icon;
            _controlsChanged.OnNext(type);
        }
    }
}
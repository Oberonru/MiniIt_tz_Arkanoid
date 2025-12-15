using Infrastructure.Configs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Configs.Input
{
    [CreateAssetMenu(menuName = "Config/Input/InputHintConfig", fileName = "InputHintConfig")]
    public class InputHintConfig : ScriptableConfig
    {
        [SerializeField] private InputActionReference _moveHint;
        [SerializeField] private InputActionReference _moveGamepadHint;
        
        public InputActionReference MoveHint => _moveHint;
        public InputActionReference MoveGamepadHint => _moveHint;
    }
}
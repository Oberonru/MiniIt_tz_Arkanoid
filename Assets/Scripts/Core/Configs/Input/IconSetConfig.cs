using Infrastructure.Configs;
using UnityEngine;

namespace Core.Configs.Input
{
    [CreateAssetMenu(menuName = "Config/Input/IconSetConfig", fileName = "IconSetConfig")]
    public class IconSetConfig : ScriptableConfig
    {
        [Header("Xbox / Generic")] 
        [SerializeField] private Sprite _a;
        [SerializeField] private Sprite _b;
        [SerializeField] private Sprite _x;
        [SerializeField] private Sprite _y;

        [Header("PlayStation")] 
        [SerializeField] private Sprite _cross;
        [SerializeField] private Sprite _circle;
        [SerializeField] private Sprite _square;
        [SerializeField] private Sprite _triangle;

        [Header("Nintendo Switch")] 
        [SerializeField] private Sprite _switchA;
        [SerializeField] private Sprite _switchB;
        [SerializeField] private Sprite _switchX;
        [SerializeField] private Sprite _switchY;

        [Header("Sticks")] 
        [SerializeField] private Sprite _leftStick;
        [SerializeField] private Sprite _rightStick;

        [Header("D-Pad")] 
        [SerializeField] private Sprite _dPadLeft;
        [SerializeField] private Sprite _dPadRight;
        [SerializeField] private Sprite _dPadUp;
        [SerializeField] private Sprite _dPadDown;

        public Sprite A => _a;
        public Sprite B => _b;
        public Sprite X => _x;
        public Sprite Y => _y;

        public Sprite Cross => _cross;
        public Sprite Circle => _circle;
        public Sprite Square => _square;
        public Sprite Triangle => _triangle;

        public Sprite SwitchA => _switchA;
        public Sprite SwitchB => _switchB;
        public Sprite SwitchX => _switchX;
        public Sprite SwitchY => _switchY;

        public Sprite LeftStick => _leftStick;
        public Sprite RightStick => _rightStick;

        public Sprite DPadLeft => _dPadLeft;
        public Sprite DPadRight => _dPadRight;
        public Sprite DPadUp => _dPadUp;
        public Sprite DPadDown => _dPadDown;
    }
}
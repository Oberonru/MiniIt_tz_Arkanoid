using Core.Configs.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.UI
{
    public class IconResolver
    {
        private readonly IconSetConfig _icons;

        public IconResolver(IconSetConfig icons)
        {
            _icons = icons;
        }

        public Sprite ResolveIcon(InputBinding binding)
        {
            var path = binding.effectivePath;

            if (path.Contains("buttonSouth"))
                return ResolveSouth(path);

            if (path.Contains("buttonNorth"))
                return ResolveNorth(path);

            if (path.Contains("buttonEast"))
                return ResolveEast(path);

            if (path.Contains("buttonWest"))
                return ResolveWest(path);

            if (path.Contains("dpad/left"))
                return _icons.DPadLeft;

            if (path.Contains("dpad/right"))
                return _icons.DPadRight;

            if (path.Contains("leftStick"))
                return _icons.LeftStick;

            return null;
        }

        private Sprite ResolveSouth(string path)
        {
            if (path.Contains("DualShock"))
                return _icons.Cross;

            if (path.Contains("Switch"))
                return _icons.SwitchB;

            return _icons.A; // Xbox / Generic
        }

        private Sprite ResolveNorth(string path)
        {
            if (path.Contains("DualShock"))
                return _icons.Triangle;

            if (path.Contains("Switch"))
                return _icons.SwitchX;

            return _icons.Y;
        }

        private Sprite ResolveEast(string path)
        {
            if (path.Contains("DualShock"))
                return _icons.Circle;

            if (path.Contains("Switch"))
                return _icons.SwitchA;

            return _icons.B;
        }

        private Sprite ResolveWest(string path)
        {
            if (path.Contains("DualShock"))
                return _icons.Square;

            if (path.Contains("Switch"))
                return _icons.SwitchY;

            return _icons.X;
        }
    }
}
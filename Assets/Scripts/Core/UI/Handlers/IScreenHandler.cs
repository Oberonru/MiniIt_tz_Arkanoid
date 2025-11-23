using System;
using Core.UI.Model;
using Core.UI.Screens;
using Infrastructure.Utils;

namespace Core.UI.Handlers
{
    public interface IScreenHandler
    {
        KeyValueList<ScreenType, UIScreen> Screens { get; }
        ScreenType CurrentScreen { get; }
        IObservable<ScreenType> OnScreenChanged { get; }
        void SetScreen(ScreenType screenType);
        T GetScreen<T>(ScreenType type) where T : UIScreen;
    }
}
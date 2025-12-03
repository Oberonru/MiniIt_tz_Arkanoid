using System;

namespace Core.Game
{
    public interface IGameStateProvider
    {
        GameState State { get; }
        IObservable<bool> OnPaused { get; }
    }
}
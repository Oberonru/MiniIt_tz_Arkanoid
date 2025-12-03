namespace Core.Game
{
    public interface IGameManager : IGameStateProvider
    {
        void Pause();
        void Play();
    }
}
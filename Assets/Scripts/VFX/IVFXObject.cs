using Infrastructure.Factory;

namespace VFX
{
    public interface IVFXObject : IFactoryObject
    {
        void PlayAnimation();
        void Disable();
    }
}
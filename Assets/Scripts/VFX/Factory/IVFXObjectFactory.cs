using Infrastructure.Factory;

namespace VFX.Factory
{
    public interface IVFXObjectFactory : IMonoBehaviourFactory<VFXObject, IVFXObject>
    {
    }
}
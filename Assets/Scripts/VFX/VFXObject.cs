using UnityEngine;

namespace VFX
{
    public abstract class VFXObject : MonoBehaviour, IVFXObject
    {
        public virtual void PlayAnimation()
        {
        }

        public virtual void Disable()
        {
        }
    }
}
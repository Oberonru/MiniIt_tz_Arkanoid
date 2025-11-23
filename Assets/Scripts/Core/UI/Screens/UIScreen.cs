using UnityEngine;

namespace Core.UI.Screens
{
    public abstract class UIScreen : MonoBehaviour
    {
        public void SetVisible(bool visible) => gameObject.SetActive(visible);
    }
}
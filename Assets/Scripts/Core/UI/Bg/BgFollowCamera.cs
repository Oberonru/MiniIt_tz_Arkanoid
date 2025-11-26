using UnityEngine;

namespace Core.UI.Bg
{
    public class BgFollowCamera : MonoBehaviour
    {
        private void Start()
        {
            transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);
        }
    }
}
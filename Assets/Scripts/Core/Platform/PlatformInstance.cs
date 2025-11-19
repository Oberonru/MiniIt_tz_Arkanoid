using Core.Configs.Platform;
using Core.Platform.Components;
using UnityEngine;
using Zenject;

namespace Core.Platform
{
    [RequireComponent(typeof(PlatformController))]
    public class PlatformInstance : MonoBehaviour
    {
        [Inject] private PlatformConfig  _config;
        [SerializeField] PlatformController controller;
        
        public PlatformConfig Stats => _config;
        public PlatformController Controller => controller;

        private void OnValidate()
        {
            if (controller == null) controller = GetComponent<PlatformController>();
        }
    }
}
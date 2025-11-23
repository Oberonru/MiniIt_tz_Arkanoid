using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    [CreateAssetMenu( menuName = "Services/SceneLoaderService", fileName = "SceneLoaderService")]

    public class SceneLoaderService : ScriptableService {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
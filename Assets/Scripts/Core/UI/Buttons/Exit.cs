using UnityEngine;

namespace Core.UI.Buttons
{
    public class Exit : MonoBehaviour
    {
        public void ExitGame()
        {
            Debug.Log("Exiting game...");
            Application.Quit();
        }
    }
}
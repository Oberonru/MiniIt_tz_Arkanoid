using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Screens
{
    public class WinScreen : UIScreen
    {
        [SerializeField] private TextMeshProUGUI _heading;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _scores;
        [SerializeField] private Button _nextLevel;
    }
}
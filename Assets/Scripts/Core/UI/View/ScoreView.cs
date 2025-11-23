using TMPro;
using UnityEngine;

namespace Core.UI.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        private int _score;
        public void AddScore(int score)
        {
            _score += score;
            _scoreText.text = _score.ToString();
        }
    }
}
using TMPro;
using UnityEngine;

public class ScoreTextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ScoreSystem _scoreSystem;
    [SerializeField] private int _minDigits = 4;

    private void Update()
    {
        int score = Mathf.FloorToInt(_scoreSystem.CurrentScore);

        string formadedText = score.ToString("D" + _minDigits.ToString());

        _scoreText.text = formadedText;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardElement : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text _usernameText;
    [SerializeField] private TMP_Text _scoreText;

    public void Initialize(int rank, string name, int score, bool isLocalPlayer)
    {
        if (_usernameText != null)
        {
            _usernameText.text = $"{rank}.{name}";
        }
        if (_scoreText != null)
        {
            _scoreText.text = score.ToString();
        }
    }
}

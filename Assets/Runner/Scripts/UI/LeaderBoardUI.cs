using Firebase.Auth;
using System.Collections;
using TMPro;
using UnityEngine;

public class LeaderBoardUI : MonoBehaviour
{
    [Header("TablesPrefabs")]
    [SerializeField] private GameObject _topPlayerPrefab;
    [SerializeField] private GameObject _secondPlayerPrefab;
    [SerializeField] private GameObject _thirdPlayerPrefab;
    [SerializeField] private GameObject _otherPlayerPrefab;

    [Header("Other Sting")]
    [SerializeField] private int _numberOfPlayers = 100;
    [SerializeField] private RectTransform _parantObject;
    [SerializeField] private TMP_Text _currentPlayerNick;
    [SerializeField] private TMP_Text _currentPlayerScore;

    [SerializeField] private DatabaseSystem _databaseSystem;

    private void OnEnable()
    {
        RefreshLeaderboard();
    }

    public void RefreshLeaderboard()
    {
        foreach (Transform child in _parantObject)
        {
            Destroy(child.gameObject);
        }

        _databaseSystem.GetCurrentUserStats(user => {
            if (user != null)
            {
                _currentPlayerNick.text = user.Username;
                _currentPlayerScore.text = user.Score.ToString();
            }
        });

        _databaseSystem.GetTopHighscores(_numberOfPlayers, list => {
            if (list == null) return;

            for (int i = 0; i < list.Count; i++)
            {
                int rank = i + 1;
                GameObject prefabToSpawn = GetPrefabByRank(rank);

                GameObject instance = Instantiate(prefabToSpawn, _parantObject);

                if (instance.TryGetComponent(out LeaderboardElement element))
                {
                    bool isMe = list[i].UserId == FirebaseAuth.DefaultInstance.CurrentUser.UserId;
                    element.Initialize(rank, list[i].Username, list[i].Score, isMe);
                }
            }
        });
    }

    private GameObject GetPrefabByRank(int rank)
    {
        return rank switch
        {
            1 => _topPlayerPrefab,
            2 => _secondPlayerPrefab,
            3 => _thirdPlayerPrefab,
            _ => _otherPlayerPrefab
        };
    }
}

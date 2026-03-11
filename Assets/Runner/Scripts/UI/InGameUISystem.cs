using UnityEngine;
using UnityEngine.UI;

public class InGameUISystem : MonoBehaviour
{
    [SerializeField] private SignOutSystem _signOutSystem;

    [Header("Screens")]
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _gameStartScreen;
    [SerializeField] private GameObject _inGameScreen;
    [SerializeField] private GameObject _leaderBoardScreen;

    [SerializeField] private GameStateSystem _gameStateSystem;
    [SerializeField] private DatabaseSystem _databaseSystem;
    [SerializeField] private SceneControlSystem _sceneControlSystem;
    [SerializeField] private ScoreSystem _scoreSystem;

    [SerializeField] private Button _continueButton;

    private void Start()
    {
        _gameStateSystem.OnDeath += ShowDeathScreen;
        AdService.Instance.OnRewardGranted += ContinueAfterAd;
    }

    private void OnDestroy()
    {
        _gameStateSystem.OnDeath -= ShowDeathScreen;
        if (AdService.Instance != null)
        {
            AdService.Instance.OnRewardGranted -= ContinueAfterAd;
        }
    }

    private void ShowDeathScreen()
    {
        _gameOverScreen.SetActive(true);
        _continueButton.interactable = AdService.Instance != null && AdService.Instance.IsAdReady();
    }

    public void GoToMainMenu()
    {
        _databaseSystem.UpdateUserScore(_scoreSystem.GetScoreToInt());
        _sceneControlSystem.ReloadCurrenScenee();
    }

    public void ContinueGame()
    {
        AdService.Instance.ShowAd();

    }

    private void ContinueAfterAd()
    {
        _gameOverScreen.SetActive(false);
        _gameStateSystem.RevivePlayer();
    }

    public void StartGame()
    {
        _gameStartScreen.SetActive(false);
        _inGameScreen.SetActive(true);
        _gameStateSystem.StartGame();
    }

    public void Exit()
    {
        _sceneControlSystem.ExitGame();
    }

    public void LogOut()
    {
        _signOutSystem.SignOut();
    }

    public void ToLeaderBoard()
    {
        _gameStartScreen.SetActive(false);
        _leaderBoardScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        _leaderBoardScreen.SetActive(false);
        _gameStartScreen.SetActive(true);
    }
}

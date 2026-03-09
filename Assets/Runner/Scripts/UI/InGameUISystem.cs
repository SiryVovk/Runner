using UnityEngine;
using UnityEngine.UI;

public class InGameUISystem : MonoBehaviour
{
    [SerializeField] private SignOutSystem _signOutSystem;

    [Header("Screans")]
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _gameStartScreen;
    [SerializeField] private GameObject _inGameScreen;
    [SerializeField] private GameObject _leaderBoardScreen;

    [SerializeField] private GameStateSystem _gameStateSystem;
    [SerializeField] private DatabaseSystem _databaseSystem;
    [SerializeField] private SceneControllSystem _sceneControllSystem;
    [SerializeField] private ScoreSystem _scoreSystem;

    [SerializeField] private Button _continueButton;

    private void OnEnable()
    {
        _gameStateSystem.OnDeath += ShowDeathScreen;
        AdSystemSinglton.Instance.OnRewardGranted += ContinueAfterAd;
    }

    private void OnDisable()
    {
        _gameStateSystem.OnDeath -= ShowDeathScreen;
        AdSystemSinglton.Instance.OnRewardGranted -= ContinueAfterAd;
    }

    private void ShowDeathScreen()
    {
        _gameOverScreen.SetActive(true);
        _continueButton.interactable =
       AdSystemSinglton.Instance.IsAdReady();
    }

    public void GoToMainMenu()
    {
        _databaseSystem.UpdateUserScore(_scoreSystem.GetScoreToInt());
        _sceneControllSystem.ReloadCurrenScenee();
    }

    public void ContinueGame()
    {
        AdSystemSinglton.Instance.ShowAd();

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
        _sceneControllSystem.ExitGame();
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
}

using UnityEngine;

public class InGameUISystem : MonoBehaviour
{
    [Header("Screans")]
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _gameStartScreen;
    [SerializeField] private GameObject _inGameScreen;
    [SerializeField] private GameObject _leaderBoardScreen;

    [SerializeField] private GameStateSystem _gameStateSystem;
    [SerializeField] private SceneControllSystem _sceneControllSystem;

    private void OnEnable()
    {
        _gameStateSystem.OnDeath += ShowDeathScreen;
    }

    private void OnDisable()
    {
        _gameStateSystem.OnDeath -= ShowDeathScreen;
    }

    private void ShowDeathScreen()
    {
        _gameOverScreen.SetActive(true);
    }

    public void GoToMainMenu()
    {
        _sceneControllSystem.ReloadCurrenScenee();
    }

    public void ContinueGame()
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
        _sceneControllSystem.GoToLogInScreen();
    }

    public void ToLeaderBoard()
    {
        _gameStartScreen.SetActive(false);
        _leaderBoardScreen.SetActive(true);
    }
}

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllSystem : MonoBehaviour
{
    public void ReloadCurrenScenee()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void GoToLogInScreen()
    {

    }
}

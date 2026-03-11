using Firebase.Auth;
using UnityEngine;

public class SignOutSystem : MonoBehaviour
{
    [SerializeField] private SceneControlSystem _sceneControllSystem;

    public void SignOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        _sceneControllSystem.GoToLogInScreen();
    }
}

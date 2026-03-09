using UnityEngine;

public class AuthorisationUI: MonoBehaviour
{
    [SerializeField] private LoginSystem _loginSystem;
    [SerializeField] private RegistrationSystem _registrationSystem;

    [SerializeField] private GameObject _loginScreen;
    [SerializeField] private GameObject _registerScreen;

    public void Login()
    {
        _loginSystem.Login();
    }

    public void Register()
    {
        _registrationSystem.Register();
    }

    public void GoToRegisterScreen()
    {
        _loginScreen.SetActive(false);
        _registerScreen.SetActive(true);
    }

    public void GoToLoginScreen()
    {
        _loginScreen.SetActive(true);
        _registerScreen.SetActive(false);
    }
}

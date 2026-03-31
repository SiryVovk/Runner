using UnityEngine;

public class AuthorisationUI: MonoBehaviour
{
    [SerializeField] private LoginSystem _loginSystem;
    [SerializeField] private RegistrationSystem _registrationSystem;

    [SerializeField] private GameObject _loginScreen;
    [SerializeField] private GameObject _registerScreen;

    [SerializeField] private SoundData sound;

    public void Login()
    {
        SFXPool.Instance.CreateSoundBuilder().WithSoundData(sound).AtPosition(transform.position).Play(this.transform);
        _loginSystem.Login();
    }

    public void Register()
    {
        SFXPool.Instance.CreateSoundBuilder().WithSoundData(sound).AtPosition(transform.position).Play(this.transform);
        _registrationSystem.Register();
    }

    public void GoToRegisterScreen()
    {
        SFXPool.Instance.CreateSoundBuilder().WithSoundData(sound).AtPosition(transform.position).Play(this.transform);
        _loginScreen.SetActive(false);
        _registerScreen.SetActive(true);
    }

    public void GoToLoginScreen()
    {
        SFXPool.Instance.CreateSoundBuilder().WithSoundData(sound).AtPosition(transform.position).Play(this.transform);
        _loginScreen.SetActive(true);
        _registerScreen.SetActive(false);
    }
}

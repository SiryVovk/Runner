using Firebase.Auth;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginSystem : MonoBehaviour
{
    [SerializeField] private SceneControlSystem _sceneControllSystem;

    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _passwordField;

    [SerializeField] private Button _loginButton;

    private void Start()
    {
        CheckUser();
    }


    private void OnEnable()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChange;
        _emailField.onValueChanged.AddListener(FieldChanged);
        _passwordField.onValueChanged.AddListener(FieldChanged);

        _loginButton.interactable = false;
    }

    private void OnDisable()
    {
        _emailField?.onValueChanged.RemoveListener(FieldChanged);
        _passwordField?.onValueChanged.RemoveListener(FieldChanged);

        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateChange;

        ClearFields();
    }
    private void HandleAuthStateChange(object sender, EventArgs e)
    {
        CheckUser();
    }

    private void CheckUser()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            _sceneControllSystem.GoToGameScreen();
        }
    }

    private void FieldChanged(string fieldText)
    {
        CheckFields();
    }

    private void CheckFields()
    {
        bool isNotEmpty = !string.IsNullOrEmpty(_emailField.text) &&
            !string.IsNullOrEmpty(_passwordField.text);


        _loginButton.interactable = isNotEmpty;
    }

    private void ClearFields()
    {
        _emailField.text = string.Empty;
        _passwordField.text = string.Empty;
    }

    public void Login()
    {
        _loginButton.interactable = false;
        StartCoroutine(LoginRoutine(_emailField.text, _passwordField.text));
    }

    private IEnumerator LoginRoutine(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError("Fail " + loginTask.Exception);
            _loginButton.interactable = true;
        }
        else
        {
            Debug.Log("Succes " + loginTask.Result.AdditionalUserInfo);
        }
    }
}


using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;

public class RegistrationSystem : MonoBehaviour
{
    [SerializeField] private SceneControlSystem _sceneControlSystem;
    [SerializeField] private DatabaseSystem _databaseSystem;

    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _userNameField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private TMP_InputField _confirmPasswordField;

    [SerializeField] private Button _registrationButton;

    private void OnEnable()
    {
        _emailField.onValueChanged.AddListener(FieldChanged);
        _userNameField.onValueChanged.AddListener(FieldChanged);
        _passwordField.onValueChanged.AddListener(FieldChanged);
        _confirmPasswordField.onValueChanged.AddListener(FieldChanged);

        _registrationButton.interactable = false;
    }

    private void OnDisable()
    {
        _emailField?.onValueChanged.RemoveListener(FieldChanged);
        _userNameField?.onValueChanged.RemoveListener(FieldChanged);
        _passwordField?.onValueChanged.RemoveListener(FieldChanged);
        _confirmPasswordField?.onValueChanged.RemoveListener(FieldChanged);

        ClearFields();
    }

    private void FieldChanged(string fieldText)
    {
        CheckFields();
    }

    private void CheckFields()
    {
        bool isNotEmpty = !string.IsNullOrEmpty(_emailField.text) && !string.IsNullOrEmpty(_userNameField.text) &&
            !string.IsNullOrEmpty(_passwordField.text) && !string.IsNullOrEmpty(_confirmPasswordField.text);

        bool isPasswordConfirmed = _confirmPasswordField.text == _passwordField.text;

        _registrationButton.interactable = isNotEmpty && isPasswordConfirmed;
    }

    private void ClearFields()
    {
        _emailField.text = string.Empty;
        _userNameField.text = string.Empty;
        _passwordField.text = string.Empty;
        _confirmPasswordField.text = string.Empty;
    }

    public void Register()
    {
        _registrationButton.interactable = false;
        StartCoroutine(RegisterRoutine(_emailField.text, _passwordField.text, _userNameField.text));
    }

    private IEnumerator RegisterRoutine(string email, string password, string userName)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            Debug.LogError($"Registration failed: {registerTask.Exception}");
            _registrationButton.interactable = true;
            yield break;
        }

        FirebaseUser newUser = registerTask.Result.User;
        UserProfile profile = new UserProfile { DisplayName = userName };

        var profileTask = newUser.UpdateUserProfileAsync(profile);
        yield return new WaitUntil(() => profileTask.IsCompleted);

        yield return StartCoroutine(_databaseSystem.AddUserToLeaderboardRoutine(newUser.UserId, userName));

        _sceneControlSystem.GoToGameScreen();
    }
}

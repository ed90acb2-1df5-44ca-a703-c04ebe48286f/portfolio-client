using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Unity.UI.Views
{
    public class AuthenticationView : View<string>
    {
        [SerializeField] private TMP_InputField _accountNameInputField = null!;
        [SerializeField] private TMP_InputField _accountPasswordInputField = null!;
        [SerializeField] private Button _loginButton = null!;

        protected override void OnConstruct(string context)
        {
            _loginButton.onClick.AddListener(OnLoginButtonClicked);
        }

        protected override void OnDeconstruct()
        {
            _loginButton.onClick.RemoveListener(OnLoginButtonClicked);
        }

        private void OnLoginButtonClicked()
        {
            if (string.IsNullOrWhiteSpace(_accountNameInputField.text) ||
                string.IsNullOrWhiteSpace(_accountPasswordInputField.text))
            {
                return;
            }

            Debug.Log(_accountNameInputField.text);
            Debug.Log(_accountPasswordInputField.text);
        }
    }
}

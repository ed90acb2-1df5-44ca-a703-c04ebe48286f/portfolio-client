using Cysharp.Threading.Tasks;
using Portfolio.Core.UI.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Unity.UI.Views
{
    public class AuthenticationView : View<AuthenticationController>
    {
        [SerializeField] private TMP_InputField _accountNameInputField = null!;
        [SerializeField] private TMP_InputField _accountPasswordInputField = null!;
        [SerializeField] private Button _loginButton = null!;

        private AuthenticationController _controller = null!;

        protected override void OnConstruct(AuthenticationController controller)
        {
            _controller = controller;
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

            LoginAsync().Forget();
        }

        private async UniTask LoginAsync()
        {
            _loginButton.interactable = false;

            var response = await _controller.AuthenticateAsync(_accountNameInputField.text, _accountPasswordInputField.text);

            Debug.Log(response?.ErrorCode);

            _loginButton.interactable = true;
        }
    }
}

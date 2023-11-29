using Cysharp.Threading.Tasks;
using Portfolio.Core.Net;
using Portfolio.Core.UI.Views;
using Portfolio.Protocol.Authentication;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Unity.UI.Views
{
    public class AuthenticationView : View<IClient>, IAuthenticationView
    {
        [SerializeField] private TMP_InputField _accountNameInputField = null!;
        [SerializeField] private TMP_InputField _accountPasswordInputField = null!;
        [SerializeField] private Button _loginButton = null!;

        private IClient _client = null!;

        protected override void OnConstruct(IClient client)
        {
            _client = client;
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

            HandleLoginAsync().Forget();
        }

        private async UniTask HandleLoginAsync()
        {
            _loginButton.interactable = false;

            _client.Send(new LoginRequest
            {
                Login = _accountNameInputField.text,
                Password = _accountPasswordInputField.text,
            });

            var response = await _client.WaitFor<LoginResponse>();

            Debug.Log(response?.ErrorCode);

            _loginButton.interactable = true;
        }
    }
}

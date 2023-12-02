using System.Threading.Tasks;
using Portfolio.Core.Net;
using Portfolio.Protocol.Authentication;

namespace Portfolio.Core.UI.Controllers
{
    public class AuthenticationController
    {
        private readonly IClient _client;

        public AuthenticationController(IClient client)
        {
            _client = client;
        }

        public async Task<LoginResponse> AuthenticateAsync(string login, string password)
        {
            _client.Command(new LoginCommand { Login = login, Password = password });
            return await _client.WaitFor<LoginResponse>();
        }
    }
}

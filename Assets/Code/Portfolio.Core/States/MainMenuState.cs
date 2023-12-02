using Portfolio.Core.Net;
using Portfolio.Core.UI;
using Portfolio.Core.UI.Controllers;

namespace Portfolio.Core.States
{
    public class MainMenuState : IState
    {
        private readonly Game _game;
        private readonly IClient _client;
        private readonly IViewFactory _viewFactory;

        private IView _authenticationViewHandle = null!;

        public MainMenuState(Game game, IClient client, IViewFactory viewFactory)
        {
            _game = game;
            _client = client;
            _viewFactory = viewFactory;
        }

        public void Enter()
        {
            var controller = new AuthenticationController(_client);
            _authenticationViewHandle = _viewFactory.Create(controller);
        }

        public void Tick(float delta)
        {
        }

        public void Exit()
        {
            _authenticationViewHandle.Terminate();
        }
    }
}

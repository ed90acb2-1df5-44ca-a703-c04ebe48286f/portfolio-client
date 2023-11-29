using Portfolio.Core.Net;
using Portfolio.Core.UI;
using Portfolio.Core.UI.Views;

namespace Portfolio.Core.States
{
    public class MainMenuState : IState
    {
        private readonly Game _game;
        private readonly IClient _client;
        private readonly IViewFactory _viewFactory;

        private IAuthenticationView _authenticationView = null!;

        public MainMenuState(Game game, IClient client, IViewFactory viewFactory)
        {
            _game = game;
            _client = client;
            _viewFactory = viewFactory;
        }

        public void Enter()
        {
            _authenticationView = _viewFactory.Create<IAuthenticationView>();
            _authenticationView.Construct(_client);
        }

        public void Tick(float delta)
        {
        }

        public void Exit()
        {
            _authenticationView.Deconstruct();
        }
    }
}

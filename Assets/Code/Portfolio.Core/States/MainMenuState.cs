using Portfolio.Core.Net;

namespace Portfolio.Core.States
{
    public class MainMenuState : IState
    {
        private readonly Game _game;
        private readonly IClient _client;

        public MainMenuState(Game game, IClient client)
        {
            _game = game;
            _client = client;
        }

        public void Enter()
        {
        }

        public void Tick(float delta)
        {
        }

        public void Exit()
        {
        }
    }
}

namespace Portfolio.Core.States
{
    public class GameplayState : IState
    {
        private readonly Game _game;

        public GameplayState(Game game)
        {
            _game = game;
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

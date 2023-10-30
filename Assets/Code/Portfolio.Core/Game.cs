using System.Collections.Generic;
using Portfolio.Core.Net;
using Portfolio.Core.States;

namespace Portfolio.Core
{
    public class Game
    {
        internal static Game Instance { get; private set; } = null!;

        private readonly List<ITickable> _tickables = new();

        private readonly ILogger _logger;
        private readonly IClient _client;

        private IState _state;

        public Game(ILogger logger, IClient client)
        {
            _logger = logger;
            _client = client;

            //_client.RegisterHandler();
            _client.Connect();

            _state = new MainMenuState(this, _client);
            _state.Enter();

            Instance = this;
        }

        public void ToMainMenuState()
        {
            _state.Exit();
            _state = new MainMenuState(this, _client);
            _state.Enter();
        }

        public void ToGameplayState()
        {
            _state.Exit();
            _state = new GameplayState(this);
            _state.Enter();
        }

        public void Tick(float delta)
        {
            _client.PollEvents();

            for (var i = 0; i < _tickables.Count; i++)
            {
                _tickables[i].Tick(delta);
            }

            _state.Tick(delta);
        }

        public void AddTickable(ITickable tickable)
        {
            _tickables.Add(tickable);
        }

        public void RemoveTickable(ITickable tickable)
        {
            _tickables.Remove(tickable);
        }
    }
}

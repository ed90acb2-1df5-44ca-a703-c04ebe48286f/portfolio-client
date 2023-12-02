using System.Collections.Generic;
using Portfolio.Core.Actors;
using Portfolio.Core.DependencyInjection;
using Portfolio.Core.Input;
using Portfolio.Core.Logging;
using Portfolio.Core.MessageHandlers;
using Portfolio.Core.Net;
using Portfolio.Core.States;
using Portfolio.Core.UI;
using Portfolio.Protocol.Authentication;
using Portfolio.Protocol.Messages;

namespace Portfolio.Core
{
    public class Game
    {
        internal static Game Instance { get; private set; } = null!;

        private readonly List<ITickable> _tickables = new();

        private readonly ILogger _logger;
        private readonly IClient _client;
        private readonly IInput _input;
        private readonly IActorFactory _actorFactory;
        private readonly IViewFactory _viewFactory;

        private IState? _state;

        public Game(IContainer container, ILogger logger, IClient client, IInput input, IActorFactory actorFactory, IViewFactory viewFactory)
        {
            _logger = logger;
            _client = client;
            _input = input;
            _actorFactory = actorFactory;
            _viewFactory = viewFactory;

            RegisterHandler<PlayerSpawnedMessage, PlayerSpawnedMessageHandler>();
            RegisterHandler<WorldStateMessage, WorldStateMessageHandler>();

            Instance = this;

            return;

            void RegisterHandler<TMessage, TMessageHandler>()
                where TMessageHandler : class, IMessageHandler<TMessage>
                where TMessage : class, new()
            {
                var handler = container.Instantiate<TMessageHandler>();
                client.RegisterHandler<TMessage>(handler.Handle);
            }
        }

        public void ToMainMenuState()
        {
            _state?.Exit();
            _state = new MainMenuState(this, _client, _viewFactory);
            _state.Enter();
        }

        public void ToGameplayState()
        {
            _state?.Exit();
            _state = new GameplayState(this, _input, _actorFactory);
            _state.Enter();
        }

        public void Tick(float delta)
        {
            _client.PollEvents();

            for (var i = 0; i < _tickables.Count; i++)
            {
                _tickables[i].Tick(delta);
            }

            _state?.Tick(delta);
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

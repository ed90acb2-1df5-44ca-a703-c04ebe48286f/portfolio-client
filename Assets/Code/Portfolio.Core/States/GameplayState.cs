using System.Collections.Generic;
using Portfolio.Core.Actors;
using Portfolio.Core.Systems;
using Portfolio.Entities;

namespace Portfolio.Core.States
{
    public class GameplayState : IState
    {
        private readonly Game _game;
        private readonly IActorFactory _actorFactory;
        private readonly World _world;

        private readonly List<ISystem> _systems = new();

        public GameplayState(Game game, IInput input, IActorFactory actorFactory)
        {
            _game = game;
            _actorFactory = actorFactory;

            _world = new World();
            // _systems.Add(new MoveViaInputSystem(_world, input));
            // _systems.Add(new TranslationSystem(_world));
        }

        public void Enter()
        {
        }

        public void Tick(float delta)
        {
            for (var i = 0; i < _systems.Count; i++)
            {
                _systems[i].Tick(delta);
            }
        }

        public void Exit()
        {
        }
    }
}

using Portfolio.Core.Components;
using Portfolio.Entities;

namespace Portfolio.Core.Systems
{
    public class TranslationSystem : ISystem
    {
        private readonly World _world;
        private readonly Query _query;

        public TranslationSystem(World world)
        {
            _world = world;
            _query = new QueryBuilder(world)
                .Require<Velocity>()
                .Require<Position>()
                .Build();
        }

        public void Tick(float delta)
        {
            var entities = _world.Query(_query);

            for (var i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var position = _world.GetComponent<Position>(entity);
                var velocity = _world.GetComponent<Velocity>(entity);

                position.Value += velocity.Value * delta;

                if (_world.HasComponent<IActor>(entity))
                {
                    _world.GetComponent<IActor>(entity).SetPosition(position.Value);
                }
            }
        }
    }
}

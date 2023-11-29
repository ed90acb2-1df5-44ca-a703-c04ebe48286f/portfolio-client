using System.Numerics;
using Portfolio.Core.Components;
using Portfolio.Entities;

namespace Portfolio.Core.Systems
{
    public class MoveViaInputSystem : ISystem
    {
        private readonly World _world;
        private readonly IInput _input;
        private readonly Query _query;

        public MoveViaInputSystem(World world, IInput input)
        {
            _world = world;
            _input = input;
            _query = new QueryBuilder(world)
                .Require<MoveViaInput>()
                .Require<Velocity>()
                .Build();
        }

        public void Tick(float delta)
        {
            var entities = _world.Query(_query);

            for (var i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var direction = new Vector2();

                if (_input.IsKeyDown(InputKey.Left))
                {
                    direction.X = -1;
                }

                if (_input.IsKeyDown(InputKey.Right))
                {
                    direction.X = 1;
                }

                if (_input.IsKeyDown(InputKey.Up))
                {
                    direction.Y = 1;
                }

                if (_input.IsKeyDown(InputKey.Down))
                {
                    direction.Y = -1;
                }

                if (direction.X != 0 || direction.Y != 0)
                {
                    direction = Vector2.Normalize(direction);
                }

                _world.SetComponent(entity, new Velocity(direction * 5f));
            }
        }
    }
}

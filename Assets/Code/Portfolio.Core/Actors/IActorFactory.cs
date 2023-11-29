using Portfolio.Core.Components;

namespace Portfolio.Core.Actors
{
    public interface IActorFactory
    {
        IActor Create(ActorType type);
    }
}

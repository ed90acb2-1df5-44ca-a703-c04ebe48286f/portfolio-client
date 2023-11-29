using Portfolio.Core.Actors;
using Portfolio.Core.Components;
using UnityEngine;

namespace Portfolio.Unity.Actors
{
    public class UnityActorFactory : IActorFactory
    {
        public IActor Create(ActorType type)
        {
            return Object.Instantiate(Resources.Load<UnityActor>("Player"));
        }
    }
}

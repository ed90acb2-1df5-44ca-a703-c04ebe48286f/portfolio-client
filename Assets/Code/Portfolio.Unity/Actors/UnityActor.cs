using Portfolio.Core.Components;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Portfolio.Unity.Actors
{
    public class UnityActor : MonoBehaviour, IActor
    {
        public void SetPosition(Vector2 position)
        {
            transform.position = new Vector3(position.X, 0, position.Y);
        }
    }
}

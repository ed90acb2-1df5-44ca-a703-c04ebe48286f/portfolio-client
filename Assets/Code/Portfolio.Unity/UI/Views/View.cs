using System;
using System.Collections.Generic;
using Portfolio.Core.UI.Views;
using UnityEngine;

namespace Portfolio.Unity.UI.Views
{
    public abstract class View<TContext> : MonoBehaviour, IView<TContext>
    {
        protected readonly List<IDisposable> Lifetime = new();

        public void Construct(TContext context)
        {
            // ...

            OnConstruct(context);
        }

        public void Deconstruct()
        {
            foreach (var disposable in Lifetime)
            {
                disposable.Dispose();
            }

            OnDeconstruct();

            Destroy(gameObject);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnConstruct(TContext context)
        {
        }

        protected virtual void OnDeconstruct()
        {
        }
    }
}

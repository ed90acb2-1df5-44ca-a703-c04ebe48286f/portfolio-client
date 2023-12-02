using System;
using System.Collections.Generic;
using Portfolio.Core.UI;
using UnityEngine;

namespace Portfolio.Unity.UI.Views
{
    public abstract class View<TController> : MonoBehaviour, IView
    {
        protected readonly List<IDisposable> Lifetime = new();

        public void Initialize(TController controller)
        {
            // ...

            OnConstruct(controller);
        }

        public void Terminate()
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

        protected virtual void OnConstruct(TController context)
        {
        }

        protected virtual void OnDeconstruct()
        {
        }
    }
}

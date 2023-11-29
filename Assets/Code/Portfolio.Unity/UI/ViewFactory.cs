using Portfolio.Core.UI;
using UnityEngine;

namespace Portfolio.Unity.UI
{
    public class ViewFactory : IViewFactory
    {
        private readonly Transform _container;

        public ViewFactory(Transform container)
        {
            _container = container;
        }

        public TView Create<TView>()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/UI/Views/AuthenticationView");
            return Object.Instantiate(prefab, _container).GetComponent<TView>();
        }
    }
}

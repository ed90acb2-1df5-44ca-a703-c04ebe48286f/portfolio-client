using Portfolio.Core.UI;
using Portfolio.Unity.UI.Views;
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

        public IView Create<TController>(TController controller)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/UI/Views/AuthenticationView");
            var view = Object.Instantiate(prefab, _container).GetComponent<View<TController>>();
            view.Initialize(controller);
            return view;
        }
    }
}

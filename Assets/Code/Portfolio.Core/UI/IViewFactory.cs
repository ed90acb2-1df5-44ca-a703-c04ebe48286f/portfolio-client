namespace Portfolio.Core.UI
{
    public interface IViewFactory
    {
        IView Create<TController>(TController controller);
    }
}

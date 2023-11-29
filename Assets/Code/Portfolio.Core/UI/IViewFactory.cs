namespace Portfolio.Core.UI
{
    public interface IViewFactory
    {
        TView Create<TView>();
    }
}

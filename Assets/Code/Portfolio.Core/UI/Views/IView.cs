namespace Portfolio.Core.UI.Views
{
    public interface IView<in TContext>
    {
        void Construct(TContext context);

        void Deconstruct();

        void Show();

        void Hide();
    }
}

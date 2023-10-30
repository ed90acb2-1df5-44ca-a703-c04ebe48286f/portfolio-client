namespace Portfolio.Core.States
{
    public interface IState
    {
        void Enter();

        void Tick(float delta);

        void Exit();
    }
}

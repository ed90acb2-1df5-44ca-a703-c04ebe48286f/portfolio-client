namespace Portfolio.Core
{
    public interface IInput
    {
        bool IsKeyDown(InputKey key);

        bool IsKeyPressed(InputKey key);

        bool IsKeyReleased(InputKey key);
    }
}

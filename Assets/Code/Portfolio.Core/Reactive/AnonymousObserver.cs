using System;

namespace Portfolio.Core.Reactive
{
    public class AnonymousObserver<T> : IObserver<T>
    {
        private readonly Action<T> _action;

        public AnonymousObserver(Action<T> action)
        {
            _action = action;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(T value)
        {
            _action.Invoke(value);
        }
    }
}

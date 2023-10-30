using System;

namespace Portfolio.Core.Reactive
{
    public class ObservableFilter<T> : IObservable<T>, IObserver<T>
    {
        private readonly IObservable<T> _observable;
        private readonly Func<T, bool> _predicate;

        private IObserver<T>? _observer;

        public ObservableFilter(IObservable<T> observable, Func<T, bool> predicate)
        {
            _observable = observable;
            _predicate = predicate;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observer = observer;
            return _observable.Subscribe(this);
        }

        public void OnCompleted()
        {
            _observer?.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _observer?.OnError(error);
        }

        public void OnNext(T value)
        {
            if (_predicate(value))
            {
                _observer!.OnNext(value);
            }
        }
    }
}

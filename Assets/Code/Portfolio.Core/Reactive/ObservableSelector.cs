using System;
using System.Collections.Generic;

namespace Portfolio.Core.Reactive
{
    public class ObservableSelector<T> : IObservable<T>, IObserver<IReadOnlyList<T>>
    {
        private readonly IObservable<IReadOnlyList<T>> _observable;
        private readonly Func<T, T> _selector;

        private IObserver<T>? _observer;

        public ObservableSelector(IObservable<IReadOnlyList<T>> observable, Func<T, T> selector)
        {
            _observable = observable;
            _selector = selector;
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

        public void OnNext(IReadOnlyList<T> list)
        {
            foreach (var value in list)
            {
                _observer!.OnNext(_selector(value));
            }
        }
    }
}

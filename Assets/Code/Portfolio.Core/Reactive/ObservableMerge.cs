using System;

namespace Portfolio.Core.Reactive
{
    public class ObservableMerge<T> : IObservable<T>, IObserver<T>
    {
        private readonly IObservable<T> _a;
        private readonly IObservable<T> _b;

        private IObserver<T>? _observer;

        public ObservableMerge(IObservable<T> a, IObservable<T> b)
        {
            _a = a;
            _b = b;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observer = observer;

            return new DisposablePair(_a.Subscribe(this), _b.Subscribe(this));
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
            _observer!.OnNext(value);
        }
    }
}

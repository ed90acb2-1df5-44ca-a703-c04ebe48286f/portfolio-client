using System;
using System.Diagnostics;

namespace Portfolio.Core.Reactive
{
    public class ObservableCooldown<T> : IObservable<T>, IObserver<T>
    {
        private readonly IObservable<T> _observable;
        private readonly TimeSpan _cooldown;

        private Stopwatch? _stopwatch;
        private IObserver<T>? _observer;

        public ObservableCooldown(IObservable<T> observable, TimeSpan cooldown)
        {
            _observable = observable;
            _cooldown = cooldown;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observer = observer;
            _stopwatch = new Stopwatch();
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
            if (_stopwatch!.IsRunning && _stopwatch.Elapsed < _cooldown)
            {
                return;
            }

            _stopwatch.Restart();
            _observer!.OnNext(value);
        }
    }
}

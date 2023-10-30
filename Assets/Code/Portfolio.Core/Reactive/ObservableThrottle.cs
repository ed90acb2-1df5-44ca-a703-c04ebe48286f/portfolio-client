using System;

namespace Portfolio.Core.Reactive
{
    public class ObservableThrottle<T> : ITickable, IObserver<T>, IObservable<T>, IDisposable
    {
        private readonly IObservable<T> _observable;
        private readonly float _duration;

        private IObserver<T>? _observer;
        private float _elapsed;
        private T? _lastValue;
        private bool _hasValue;

        public ObservableThrottle(IObservable<T> observable, int milliseconds)
        {
            _observable = observable;
            _duration = milliseconds / 1000f;

            Game.Instance.AddTickable(this);
        }

        public void Tick(float delta)
        {
            _elapsed += delta;

            if (_elapsed < _duration || !_hasValue)
            {
                return;
            }

            _observer?.OnNext(_lastValue!);

            _elapsed = 0;
            _hasValue = false;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observer = observer;
            _observable.Subscribe(this);
            return this;
        }

        public void Dispose()
        {
            Game.Instance.RemoveTickable(this);
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
            _lastValue = value;
            _hasValue = true;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Portfolio.Core.Reactive
{
    public class ObservableBuffer<T> : IObservable<IReadOnlyList<T>>, IObserver<T>
    {
        private readonly IObservable<T> _observable;
        private readonly List<T> _buffer;
        private readonly int _bufferSize;

        private IObserver<IReadOnlyList<T>>? _observer;

        public ObservableBuffer(IObservable<T> observable, int bufferSize)
        {
            _observable = observable;
            _bufferSize = bufferSize;
            _buffer = new List<T>(_bufferSize);
        }

        public IDisposable Subscribe(IObserver<IReadOnlyList<T>> observer)
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
            _buffer.Add(value);

            if (_buffer.Count < _bufferSize)
            {
                return;
            }

            _observer!.OnNext(new List<T>(_buffer));
            _buffer.Clear();
        }
    }
}

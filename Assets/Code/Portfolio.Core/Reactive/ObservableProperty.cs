using System;

namespace Portfolio.Core.Reactive
{
    public class ObservableProperty<T> : IObservable<T>, IDisposable
    {
        private readonly Property<T> _property;

        private IObserver<T>? _observer;

        public ObservableProperty(Property<T> property)
        {
            _property = property;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observer = observer;
            _property.Changed += OnPropertyChanged;

            return this;
        }

        public void Dispose()
        {
            _property.Changed -= OnPropertyChanged;
        }

        private void OnPropertyChanged(T value)
        {
            _observer?.OnNext(value);
        }
    }
}

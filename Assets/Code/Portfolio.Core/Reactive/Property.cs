using System;

namespace Portfolio.Core.Reactive
{
    public class Property<T>
    {
        public event Action<T>? Changed;

        private T _value;

        public Property(T value)
        {
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke(_value);
            }
        }
    }
}

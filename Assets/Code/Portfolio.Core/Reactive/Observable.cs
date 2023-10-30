using System;
using System.Collections.Generic;

namespace Portfolio.Core.Reactive
{
    public static class Observable
    {
        public static IObservable<float> FromGameTick()
        {
            return new ObservableGameTick(Game.Instance);
        }

        public static IObservable<T> FromProperty<T>(Property<T> property)
        {
            return new ObservableProperty<T>(property);
        }

        public static IObservable<T> AsObservable<T>(this Property<T> property)
        {
            return new ObservableProperty<T>(property);
        }

        public static IObservable<IReadOnlyList<T>> Buffer<T>(this IObservable<T> observable, int buffer)
        {
            return new ObservableBuffer<T>(observable, buffer);
        }

        public static IObservable<T> Select<T>(this IObservable<IReadOnlyList<T>> observable, Func<T, T> selector)
        {
            return new ObservableSelector<T>(observable, selector);
        }

        public static IObservable<T> Throttle<T>(this IObservable<T> observable, int milliseconds)
        {
            return new ObservableThrottle<T>(observable, milliseconds);
        }

        public static IObservable<T> Cooldown<T>(this IObservable<T> observable, TimeSpan timeSpan)
        {
            return new ObservableCooldown<T>(observable, timeSpan);
        }

        public static IObservable<T> Merge<T>(this IObservable<T> a, IObservable<T> b)
        {
            return new ObservableMerge<T>(a, b);
        }

        public static IObservable<T> Filter<T>(this IObservable<T> observable, Func<T, bool> predicate)
        {
            return new ObservableFilter<T>(observable, predicate);
        }

        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> action)
        {
            return observable.Subscribe(new AnonymousObserver<T>(action));
        }

        public static void DisposeWith(this IDisposable disposable, ICollection<IDisposable> collection)
        {
            collection.Add(disposable);
        }
    }
}

using System;

namespace Portfolio.Core.Reactive
{
    public class ObservableGameTick : ITickable, IObserver<float>, IObservable<float>, IDisposable
    {
        private readonly Game _game;
        private IObserver<float>? _observer;

        public ObservableGameTick(Game game)
        {
            _game = game;
            _game.AddTickable(this);
        }

        public void Tick(float delta)
        {
            OnNext(delta);
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            _observer = observer;
            return this;
        }

        public void Dispose()
        {
            _game.RemoveTickable(this);
        }

        public void OnCompleted()
        {
            _observer?.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _observer?.OnError(error);
        }

        public void OnNext(float value)
        {
            _observer?.OnNext(value);
        }
    }
}

using System;

namespace Portfolio.Core.Reactive
{
    public class DisposablePair : IDisposable
    {
        private readonly IDisposable _a;
        private readonly IDisposable _b;

        public DisposablePair(IDisposable a, IDisposable b)
        {
            _a = a;
            _b = b;
        }

        public void Dispose()
        {
            _a.Dispose();
            _b.Dispose();
        }
    }
}

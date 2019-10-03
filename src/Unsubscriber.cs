using System;
using System.Collections.Generic;

namespace src
{
    public class Unsubscriber<News> : IDisposable
    {
        private readonly IList<IObserver<News>> _observers;
        private readonly IObserver<News> _observer;

        internal Unsubscriber(IList<IObserver<News>> observers, IObserver<News> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}
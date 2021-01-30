using System;
using System.Collections.Generic;

namespace BinManagement
{


    public interface ICabinetEventPublisher<CabinetEvent> : IObservable<CabinetEvent>
    {
        // Publishes an event 
        void Publish(CabinetEvent cabinetEvent);
    }

    /// <summary>
    ///  Handles Publishing Cabinet evetns to  it subscirbers
    /// </summary>
    public class CabinetEventPublisher<CabinetEvent> : ICabinetEventPublisher<CabinetEvent>
    {
        List<IObserver<CabinetEvent>> observers;

        public CabinetEventPublisher()
        {
            observers = new List<IObserver<CabinetEvent>>();
        }

        // Dispose implementation to remove itself from the subscribers collection.
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<CabinetEvent>> _observers;
            private IObserver<CabinetEvent> _observer;

            public Unsubscriber(List<IObserver<CabinetEvent>> observers, IObserver<CabinetEvent> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }

        // Register all the Observers
        // Returns cancellation token
        public IDisposable Subscribe(IObserver<CabinetEvent> observer)
        {

            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        // Pulishes events to all the subscribers
        public void Publish(CabinetEvent cabinetEvent)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(cabinetEvent);
            }
        }

    }
}

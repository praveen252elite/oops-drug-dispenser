using System;
namespace BinManagement
{
    // Listens to the Strem of events and log appropriate event for compliance.
    public class ComplainceLogging : IObserver<CabinetEvent>
    {
        public ComplainceLogging()
        {
        }

        // Stream has Ended. 
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        // Error occured while reciving a stream
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        // Receives stream of events
        public void OnNext(CabinetEvent cabinetEvent)
        {
            // Apply fitlers ....
            Console.WriteLine("------Complaince ----------");
            Console.WriteLine($" Logging - {cabinetEvent.User} - {cabinetEvent.Action} - {cabinetEvent.EventTime}");
        }
    }
}

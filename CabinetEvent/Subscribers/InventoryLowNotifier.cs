using System;
namespace BinManagement
{
    // Listens to the Strem of events and notifies when the units in a bin are < 20%
    public class CabinetInventoryLowNotifier : IObserver<CabinetEvent>
    {
        // Lower threshould in Percentile
        public int LowerThreshould
        {
            get;
            private set;
        }

        public CabinetInventoryLowNotifier(int threshould = 20)
        {
            this.LowerThreshould = threshould;
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
            // Apply Business rules.....
            if (null == cabinetEvent.Bin)
            {
                return;
            }
            int currentStockInPercentage = (int)((cabinetEvent.Bin.Count / cabinetEvent.Bin.MaxUnitCount) * 100);
            if (currentStockInPercentage <= LowerThreshould)
            {
                Console.WriteLine("-----Low On Inventory -----");
               Console.WriteLine($"{cabinetEvent.Bin}");
            }

        }
    }
}

using System;

namespace BinManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            ICabinetEventPublisher<CabinetEvent> cabinetEventsPublisher = new CabinetEventPublisher<CabinetEvent>();

            // Register Inventory low notifier to Cabinet Events Publisher
            var inventoryLowNotifier = new CabinetInventoryLowNotifier();
            cabinetEventsPublisher.Subscribe(inventoryLowNotifier);

            // Register Complaice logging to Cabinet Events Publisher
            var complaince = new ComplainceLogging();
            cabinetEventsPublisher.Subscribe(complaince);

            // Create new Cabinet, Inject Event publisher
            ICabinet cabinet = new MedicalCabinet(cabinetEventsPublisher);

            cabinet.AssignMedication("praveen", "00", "Tylenol", "tynl");
            cabinet.AddMedicationUnit("praveen", "00", 5);
            cabinet.RemoveMedicationUnit("praveen", "00", 1);
            
            cabinet.RemoveMedicationUnit("praveen", "00", 3);
            
            cabinet.Report();
            Console.ReadLine();
        }
    }
}

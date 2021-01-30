using System;
using System.Collections.Generic;
using System.Linq;

namespace BinManagement
{
    #region Cabinet

    // Medical Cabinet Unit 
    public interface ICabinet
    {
        // Reports over all Inventory with count
        void Report();

        // Adds a medication to a bin.  
        void AddMedicationUnit(string user, string binId, int count);

        // Removes a medication unit
        void RemoveMedicationUnit(string user, string binId, int count);

        // Assigns medication to bin
        void AssignMedication(string user, string binId, string medicationName, string medicationId);
    }

    //  Simple Medical Cabinet
    //  TODO: Implement State Machine managemet for Cabinet with following states
    //  1. IdealState
    //  2. LoggedInState,
    //  3. LoggeOutSate
    public class MedicalCabinet : ICabinet
    {
        // Manages the locations
        private readonly List<ILocation> locations = new List<ILocation>();

        // Publishes events happening on this Cabinet.
        private readonly ICabinetEventPublisher<CabinetEvent> _event;

        // Constructor with injected event publisher.
        public MedicalCabinet(ICabinetEventPublisher<CabinetEvent> eventPublisher, int locationCount = 10)
        {
            _event = eventPublisher;

            //Allocate Locations
            for (int index = 0; index < locationCount; index++)
            {
                locations.Add(new Location(index.ToString()));
            }
        }

        /// <summary>
        /// Reports all the Inventory
        /// </summary>
        public void Report()
        {
            // Output all the Bins inventory in each loaction
            Print("------ Report --------");

            for (int locationId = 0; locationId < locations.Count; locationId++)
            {
                ILocation location = (ILocation)locations[locationId];
                foreach (var bin in location.Bins())
                {
                    Console.WriteLine($"{location} {bin}");
                }
            }
        }

        /// <summary>
        /// Adds Medication to the Bin Units
        /// </summary>
        public void AddMedicationUnit(string user, string binId, int count)
        {
            // Check Inputs
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(binId))
            {
                Print("User Id or Bin id is Missing");
            }

            // Lookup bin
            Bin bin = LookupForBin(binId);
            if (null == bin) return;

            // Add medication to bin
            bool isAddSuccess = bin.AddMedication(count);

            // Prints Message
            string displayMessage = isAddSuccess ? $"Added Medication ({count}) to {bin}" : $"Cannot add to {bin}";
            Print(displayMessage);

            // Stream event
            PublishEvent(user, CabinetAction.AddMedication, bin);
        }

        /// <summary>
        /// Removes medication from the bin units
        /// </summary>
        public void RemoveMedicationUnit(string user, string binId, int count)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(binId))
            {
                Print("User Id or Bin id is Missing");
            }

            // Lookup Bin
            Bin bin = LookupForBin(binId);
            if (null == bin) return;

            // Remove medication
            bool removeSuccess = bin.RemoveMedication(count);

            // Prints Message
            string displayMessage = removeSuccess ? $"Removed Medication ({count}) from {bin}" : $"No Stock on {bin}";
            Print(displayMessage);

            // Stream event
            PublishEvent(user, CabinetAction.AddMedication, bin);
        }

        // Assign Medication to Bin
        public void AssignMedication(string user, string binId, string medicationName, string medicationId)
        {
            if (string.IsNullOrEmpty(user)
                || string.IsNullOrEmpty(medicationName)
                || string.IsNullOrEmpty(medicationId)
                || string.IsNullOrEmpty(binId))
            {
                Print("User Id or Bin id is Missing");
            }

            // Lookup Bin
            Bin bin = LookupForBin(binId);
            if (null == bin) return;

            // Assign medication
            bin.AssignMedication(new Medication(medicationId, medicationName));
            Print($"Assigned Medication - {bin}");

            // Stream Event
            PublishEvent(user, CabinetAction.AddMedication, bin);

        }

        // Looks Up for matching Bin in all the locations, Prints message if not found
        private Bin LookupForBin(string binId)
        {
            foreach (var location in locations)
            {
                if (location.HasBin(binId))
                {
                    return location.GetBin(binId);
                }
            }

            //Bin not found
            Print($"Bin - {binId} not found");
            return null;
        }

        // Prints the Message on the Canbinet display
        private void Print(string message)
        {
            Console.WriteLine(message);
        }

        // Stream Events
        private void PublishEvent(string user, CabinetAction action, Bin bin)
        {
            _event.Publish(new CabinetEvent(user, action, bin));
        }
    }

    #endregion

}

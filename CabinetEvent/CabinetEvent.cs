using System;
namespace BinManagement
{
    // User Actions on the Cabinet.
    public enum CabinetAction
    {
        AssignMedication,

        AddMedication,

        RemoveMedication,
    }

    // Captures all the UserEvents/Actions performed on this Cabinet 
    public class CabinetEvent
    {
        // Optional Bin Information
        public Bin Bin
        {
            get;
            private set;
        }

        // User info who is performing actions on the cabinet.
        public string User
        {
            get;
            private set;
        }

        // Initiated User Action on this Cabinet
        public CabinetAction Action
        {
            get;
            private set;
        }

        // Recorded time stamp.
        public DateTime EventTime
        {
            get;
            private set;
        }

        public CabinetEvent(string user, CabinetAction action, Bin bin )
        {
            this.User = user;
            this.Action = action;
            this.Bin = bin;
        }
    }
}

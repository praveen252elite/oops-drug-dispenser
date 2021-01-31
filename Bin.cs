using System;
namespace BinManagement
{
    // Sizes of the Logical Bins
    public enum BinSize
    {
        Small,
        Medium,
        Large
    }

    // Factory which creates the Bins.
    public class BinFactory
    {
        // Creates a new logical bin
        public static Bin Create(BinSize binType)
        {
            return binType switch
            {
                BinSize.Small => new SmallBin(),
                BinSize.Medium => new MediumBin(),
                BinSize.Large => new LargeBin(),
                _ => new SmallBin(),
            };
        }
    }

    // Logical Bins which will store the medications
    public abstract class Bin
    {
        // Generated an Unique Identifier of each Bin
        public string Id { get; set; }

        // Specifies the Bin size category
        public abstract BinSize Type { get; }

        // Number of Maximum units it can accomodate
        public abstract int MaxUnitCount { get; }

        // Number of Medications availabe in a Bin
        public int Count { get; protected set; }

        // Medication
        public IMedication Medication { get; protected set; }

        //Specifies if the user has assigned a medication in this bin or not
        public bool HasMedication()
        {
            return Medication != null;
        }

        // Specifies what Medication need to be stored in the bin
        public void AssignMedication(IMedication medication)
        {
            ResetMedication();
            this.Medication = medication;
        }

        // Add Medication units to the bin 
        public bool AddMedication(int medicationCount)
        {
            bool canAccomidate = Count + medicationCount <= MaxUnitCount;
            if (HasMedication() && canAccomidate)
            {
                Count += medicationCount;
                return true;
            }

            return false;
        }

        // Removes Medication units form the bin
        public bool RemoveMedication(int medicationCount)
        {
            bool inStock = MaxUnitCount - Count - medicationCount >= 0;
            if (HasMedication() && !inStock)
            {
                Count -= medicationCount;
                return true;
            }

            return false;
        }

        // Resets the Bin for the diffrent medication
        public void ResetMedication()
        {
            this.Count = 0;
            this.Medication = null;
        }

        public Bin()
        {
            //Default count is Zero
            this.Count = 0;
            Console.WriteLine(Guid.NewGuid().ToString());
        }

        // Return's the Bin label
        public override string ToString()
        {
            string medicationOutput = HasMedication() ? this.Medication.ToString() : "--None--";
            return $"Bin:{ this.Id}, Medication:{medicationOutput}, Units:{this.Count}/{ this.MaxUnitCount}";
        }
    }

    //Small Bin with Max 3 units
    public class SmallBin : Bin
    {
        /// <summary>
        /// <see cref="Bin.Type"/>
        /// </summary>
        public override BinSize Type => BinSize.Small;

        /// <summary>
        /// <see cref="Bin.MaxUnitCount"/>
        /// </summary>
        public override int MaxUnitCount => 3;

        public SmallBin()
        {
        }

        public SmallBin(Medication meditation, int count)
        {
            this.Medication = meditation;
            this.Count = count;
        }
    }

    // Medium Bin with Max 10 units
    public class MediumBin : Bin
    {
        /// <summary>
        /// <see cref="Bin.MaxUnitCount"/>
        /// </summary>
        public override BinSize Type => BinSize.Medium;

        /// <summary>
        /// <see cref="Bin.MaxUnitCount"/>
        /// </summary>
        public override int MaxUnitCount => 10;

        public MediumBin()
        {
        }

        public MediumBin(Medication meditation, int count)
        {
            this.Medication = meditation;
            this.Count = count;
        }
    }

    // Large Bin with max 5 units
    public class LargeBin : Bin
    {
        /// <summary>
        /// <see cref="Bin.MaxUnitCount"/>
        /// </summary>
        public override BinSize Type => BinSize.Medium;

        /// <summary>
        /// <see cref="Bin.MaxUnitCount"/>
        /// </summary>
        public override int MaxUnitCount => 5;

        public LargeBin()
        {
        }

        public LargeBin(Medication meditation, int count)
        {
            this.Medication = meditation;
            this.Count = count;
        }
    }
}

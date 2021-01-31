using System;
using System.Collections.Generic;
using System.Linq;

namespace BinManagement
{
    // Physical Loaction in a cabinet which holds mutiple bins
    public interface ILocation
    {
        // Unique Indentifier of the Bin
        string Id { get; }

        // Verifies if this Location has given Bin
        bool HasBin(string binId);

        // Find the Bin in this location
        Bin GetBin(string binId);

        // Allocated Bins in this location.
        List<Bin> Bins();
    }

    // Physical Loaction in a cabinet.
    public class Location : ILocation
    {
        // Maximum number of various Bins which can accomidate in a single location
        private readonly Dictionary<BinSize, int> Capacity = new Dictionary<BinSize, int>()
                {
                    { BinSize.Small, 3 },
                    { BinSize.Medium, 5 },
                    { BinSize.Large , 2 }
                };

        // Manages Bins
        private readonly Dictionary<string, Bin> _bins = new Dictionary<string, Bin>();

        // Location Id
        public string Id { get; private set; }

        // Constructor, Allocates Bins based on the capacity.
        public Location(string id)
        {
            this.Id = id;
            AllocateBins();
        }

        // Constructor with Capacity
        public Location(Dictionary<BinSize, int> capacity)
        {
            this.Capacity = capacity;
        }

        // Allocates the Logical small medium and large bins based on the configured capacity
        private void AllocateBins()
        {
            // Get all Bin sizes
            BinSize[] binSizes = (BinSize[])Enum.GetValues(typeof(BinSize));

            foreach (BinSize binSize in binSizes)
            {
                //Max Bins that can be allocated in this location;
                int maxBins = Capacity[binSize];
                for (int binIndex = 0; binIndex < maxBins; binIndex++)
                {
                    Bin bin = BinFactory.Create(binSize);
                    bin.Id = $"{this.Id}{binIndex}";
                    _bins[bin.Id] = bin;
                }
            }
        }

        /// <see cref="ILocation.GetBin(string)"/>
        public Bin GetBin(string binId)
        {
            return HasBin(binId) ? _bins[binId] : null;
        }

        /// <see cref="ILocation.HasBin(string)"/>
        public bool HasBin(string binId)
        {
            return _bins.ContainsKey(binId ?? string.Empty);
        }

        /// <see cref="ILocation.Bins"/>
        public List<Bin> Bins()
        {
            return _bins.Values.ToList();
        }

        // Loction Output format
        public override string ToString()
        {
            return $"Location - { this.Id}";
        }
    }
}

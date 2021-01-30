
namespace BinManagement
{
    // Medication Attributes
    public interface IMedication
    {
        // Medication unique Identifier
        string Id { get; }

        //Medication Name
        string Name { get; }
    }

    // Simple Medication model caputures all the medical attributes.
    public class Medication : IMedication
    {
        // Medication unique Identifier
        public string Id { get; }

        //Medication Name
        public string Name { get; }

        public Medication(string id, string name)
        {
            this.Id = id ?? "";
            this.Name = name ?? "";
        }

        // Outputs the medication;
        public override string ToString()
        {
            return $"MedicationId:{this.Id}, MedicationName:{this.Name}";
        }
    }
}

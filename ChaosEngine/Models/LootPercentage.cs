
namespace ChaosEngine.Models
{
    public class LootPercentage
    {
        public int ID { get; }
        public int Percentage { get; }
        public int Quantity { get; }
        public LootPercentage(int id, int percentage, int quanitity)
        {
            ID = id;
            Percentage = percentage;
            Quantity = quanitity;
        }
    }
}

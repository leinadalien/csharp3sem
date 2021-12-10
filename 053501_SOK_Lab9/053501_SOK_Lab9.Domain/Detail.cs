namespace _053501_SOK_Lab9.Domain
{
    public class Detail
    {
        public double Weight { get; set; }
        public string Description { get; set; }
        public uint ID { get; set; }
        static uint idCounter;
        public Detail()
        {
            idCounter++;
            Weight = 0;
            Description = null;
            ID = idCounter;
        }
        public Detail(string description, double weight)
        {
            idCounter++;
            ID = idCounter;
            Description = description;
            Weight = weight;
        }
    }
}

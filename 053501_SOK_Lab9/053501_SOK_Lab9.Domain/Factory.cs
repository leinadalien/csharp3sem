namespace _053501_SOK_Lab9.Domain
{
    public class Factory
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Storage Storage { get; set; }
        public Factory()
        {
            Name = null;
            Description = null;
            Address = null;
            Storage = null;
        }
        public Factory(string name, string description, string address, Storage storage)
        {
            Name = name;
            Description = description;
            Address = address;
            Storage = storage;
        }
    }
}

namespace _053501_SOK_Lab10
{
    class Employee
    {
        public int Age { get; set; }
        public bool IsWorking { get; set; }
        public string Name { get; set; }
        public Employee()
        {
            Age = 0;
            IsWorking = false;
            Name = null;
        }
        public Employee(string name, int age)
        {
            Name = name;
            Age = age;
            IsWorking = age > 17 && age < 65;
        }
        public Employee(string name, int age, bool isWorking)
        {
            Name = name;
            Age = age;
            IsWorking = isWorking;
        }
    }
}

namespace Binary
{
    internal class Car : Vehicle
    {
        public Car(string make, string model, int year, int range, double engineCapacity)
        {
            Make = make;
            Model = model;
            Year = year;
            Range = range;
            EngineCapacity = engineCapacity;
        }
    }
}

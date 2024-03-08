using System;
using System.Threading.Tasks;

namespace Binary
{
    public class TestApp
    {
        private readonly BinaryFileHandler<Vehicle> Handler;
        
        public TestApp()
        {
            Handler = new VehicleBinaryFileHandler();
        }

        public async Task RunTestAsync()
        {
            Car car1 = new Car("Honda", "Jazz", 2010, 10000, 2);
            Car car2 = new Car("Nissan", "Skyline", 2005, 500, 2.5);
            Car car3 = new Car("Toyota", "Carolla", 2008, 700, 1.8);

            Handler.AddObjectToWrite(car1);
            Handler.AddObjectToWrite(car2);
            Handler.AddObjectToWrite(car3);
            await Handler.WriteValuesAsync();

            Handler.ReadDataFile();
        }
    }
}

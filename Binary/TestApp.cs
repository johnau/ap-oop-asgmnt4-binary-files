using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary
{
    public class TestApp
    {
        private BinaryFileHandler<Vehicle> handler;
        
        public TestApp()
        {
            handler = new VehicleBinaryFileWriter();
        }

        public async Task RunTestAsync()
        {
            Car car1 = new Car("Honda", "Jazz", 2010, 10000, 2);
            Car car2 = new Car("Nissan", "Skyline", 2005, 500, 2.5);
            Car car3 = new Car("Toyota", "Carolla", 2008, 700, 1.8);

            handler.AddObjectToWrite(car1);
            handler.AddObjectToWrite(car2);
            handler.AddObjectToWrite(car3);
            await handler.WriteValuesAsync();

            handler.ReadDataFile();
        }
    }
}

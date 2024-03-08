using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Binary
{
    internal class VehicleBinaryFileHandler : BinaryFileHandler<Vehicle>
    {
        private readonly Vehicle _terminatorObject;
        
        public VehicleBinaryFileHandler()
            : base()
        {
            _terminatorObject = new Vehicle()
            {
                Make = "xxx",
                Model = "xxx",
                Year = -1,
                Range = -1,
                EngineCapacity = -1,
            };
        }

        protected override void WriteEndObject(BinaryWriter writer)
        {
            writer.Write(_terminatorObject.Make);
            writer.Write(_terminatorObject.Model);
            writer.Write(_terminatorObject.Year);
            writer.Write(_terminatorObject.Range);
            writer.Write(_terminatorObject.EngineCapacity);
        }

        protected override void WriteObject(BinaryWriter writer, Vehicle vehicle)
        {
            writer.Write(vehicle.Make);
            writer.Write(vehicle.Model);
            writer.Write(vehicle.Year);
            writer.Write(vehicle.Range);
            writer.Write(vehicle.EngineCapacity);
        }

        public override List<Vehicle> ReadDataFile()
        {
            items.Clear();
            if (!File.Exists(filePath))
                throw new Exception($"An expected file was not found: {filePath}");
            
            using (var stream = File.Open(filePath, FileMode.Open))
            using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
            {
                ReadData(reader);
            }

            return items;
        }

        private void ReadData(BinaryReader reader)
        {
            bool eof = false;
            while (!eof)
            {
                var model = reader.ReadString();
                Debug.WriteLine($"Read model: {model}");
                var make = reader.ReadString();
                Debug.WriteLine($"Read make: {make}");
                var year = reader.ReadInt32();
                Debug.WriteLine($"Read year: {year}");
                var range = reader.ReadInt32();
                Debug.WriteLine($"Read range: {range}");
                var engineCapacity = reader.ReadDouble();
                Debug.WriteLine($"Read capcity: {engineCapacity}");

                var vehicle = new Vehicle()
                {
                    Model = model,
                    Make = make,
                    Year = year,
                    Range = range,
                    EngineCapacity = engineCapacity,
                };

                if (model != "xxx") items.Add(vehicle);
                else
                {
                    eof = true;
                    Debug.WriteLine($"Reached end of file");
                }
            }
        }
    }
}

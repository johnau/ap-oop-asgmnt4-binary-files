using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Automation.Peers;

namespace Binary
{
    public abstract class BinaryFileHandler<T>
    {
        protected const string filePath = "U:\\data.dat";

        protected List<T> items;

        public BinaryFileHandler()
        {
            items = new List<T>();
        }

        public void AddObjectsToWrite(List<T> items)
        {
            this.items.AddRange(items);
        }

        public void AddObjectToWrite(T item)
        {
            items.Add(item);
        }

        protected abstract void WriteObject(BinaryWriter writer, T item);

        protected abstract void WriteEndObject(BinaryWriter writer);

        public async Task WriteValuesAsync()
        {
            await Task.Run(() => WriteValues());
        }

        public void WriteValues()
        {
            using (var stream = File.Open(filePath, FileMode.OpenOrCreate))
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
            {
                foreach (var item in items)
                {
                    WriteObject(writer, item);
                }

                WriteEndObject(writer);
            }
            
            items.Clear();
            Debug.WriteLine($"Items cleared after write: List size={items.Count}");
        }

        public abstract List<T> ReadDataFile();
    }
}

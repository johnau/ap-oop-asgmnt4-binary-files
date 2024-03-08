using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Binary
{
    public abstract class BinaryFileHandler<T>
    {
        protected const string filename = "data.bin";
        protected string filePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);

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
            Debug.WriteLine($"Filepath is: {filePath}");
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

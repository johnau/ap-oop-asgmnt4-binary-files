﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Binary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private List<Vehicle> vehicles = new List<Vehicle>();

        public MainPage()
        {
            this.InitializeComponent();

            var car1 = new Car("Honda", "Jazz", 2015, 12000, 3.6);
            var car2 = new Car("Honda", "Jazz", 2015, 12000, 3.6);
            vehicles.Add(car1);
            vehicles.Add(car2);

            Save();
        }

        async void Save()
        {
            var fileName = "myFile.bin";
            var storageFolder = ApplicationData.Current.LocalFolder;
            var file = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            Debug.WriteLine(file.ToString());

            using (var stream = File.Open(file.Path, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    foreach (var item in vehicles)
                    {
                        writer.Write(item.Make);
                        writer.Write(item.Model);
                        writer.Write(item.Year);
                        writer.Write(item.EngineCapacity);
                        writer.Write(item.Range);
                    }
                    writer.Write("XXX");
                    writer.Write("XXX");
                    writer.Write(-1);
                    writer.Write(-1);
                    writer.Write(-1);
                }
            }
        }

        void load(string fileName)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            using (var stream = File.Open(
                storageFolder.Path + "\\" + fileName,
                FileMode.Open
                ))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    string make = reader.ReadString();
                    string model = reader.ReadString();
                    int year = reader.ReadInt32();
                    double engineCapacity = reader.ReadDouble();
                    int range = reader.ReadInt32();

                    var car = new Car(make, model, year, range, engineCapacity);
                    vehicles.Add(car);
                }

            }
        }


    }
}

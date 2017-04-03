using CarBatteryAccounter.Model.Entities;
using CarBatteryAccounter.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarBatteryAccounter.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Car> collection;

        public MainWindow()
        {
            InitializeComponent();

            InitCollection();
        }

        private void InitCollection()
        {
            collection = new ObservableCollection<Car>();
            //Sample data --------------------------------

            for (int i = 0; i < 10; i++)
            {
                var battary = new Battaty() { Model = "Duracell", SetDate = DateTime.Now, WriteOffDate = null, SubreportDate = null };
                var batList = new List<Battaty>();
                batList.Add(battary);
                var car = new Car { Model = "Audi A8", Number = "2134-AB", DriverName = "Dmitry",  Battaries = batList };
                
                collection.Add(car);
               
            }
            listView.ItemsSource = collection;
            //--------------------------------------------
        }
    }
}
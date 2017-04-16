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
        private ObservableCollection<Car> carCollection;

        private ObservableCollection<Battary> battaryCollection;

        public MainWindow()
        {
            InitializeComponent();

            InitCarCollection();
        }

        private void InitCarCollection()
        {
            carCollection = new ObservableCollection<Car>();
            //Sample data --------------------------------

            for (int i = 0; i < 10; i++)
            {
                var battary = new Battary()
                {
                    Model = "Duracell",
                    SetDate = DateTime.Now,
                    WriteOffDate = DateTime.Now,
                    SubreportDate = DateTime.Now,
                    NomenclatureNumber = "wwf32r1313r",
                    SerialNumber = "123214",
                    Type = BattaryType.Type3
                };
                var batList = new List<Battary>();
                batList.Add(battary);
                batList.Add(battary);
                var car = new Car { Model = "Audi A8", Number = "2134-AB", DriverName = "Dmitry", Battaries = batList };

                carCollection.Add(car);

            }
            carListView.ItemsSource = carCollection;
            //--------------------------------------------
        }

        private void CarListView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var car = item as Car;
                var battaryList = car.Battaries;
                battaryCollection = new ObservableCollection<Battary>();
                battaryList.ForEach(battary => battaryCollection.Add(battary));
                BattaryListView.ItemsSource = battaryCollection;
                ClearInfoForm();
            }
        }

        private void BattaryListView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var battary = item as Battary;
                FillInfoForm(battary);
            }
        }

        private void FillInfoForm(Battary battary)
        {
            modelTextBox.Text = battary.Model;
            typeTextBox.Text = battary.Type.ToString();
            serialNumberTextBox.Text = battary.SerialNumber;
            nomenclatureNumberTextBox.Text = battary.NomenclatureNumber;
            setDateTextBox.Text = battary.SetDate.ToString("dd.mm.yyyy");
            writeOffDateTextBox.Text = battary.WriteOffDate?.ToString("dd.mm.yyyy");
            subreportDateTextBox.Text = battary.SubreportDate?.ToString("dd.mm.yyyy");
        }

        private void ClearInfoForm()
        {
            modelTextBox.Text = string.Empty;
            typeTextBox.Text = string.Empty;
            serialNumberTextBox.Text = string.Empty;
            nomenclatureNumberTextBox.Text = string.Empty;
            setDateTextBox.Text = string.Empty;
            writeOffDateTextBox.Text = string.Empty;
            subreportDateTextBox.Text = string.Empty;
        }
    }
}


using CarBatteryAccounter.Model;
using CarBatteryAccounter.Model.Entities;
using CarBatteryAccounter.Model.ViewModels;
using CarBatteryAccounter.Samples;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

        private ObservableCollection<Battary> subBattaryCollection;

        private DocumentManager documentManager;

        private Car selectedCar;

        private Battary selectedBattary;

        private Battary selectedSubBattary;

        public MainWindow()
        {
            //CodeSamples.TestOledbConnection();

            var fileUpdater = new FileUpdater();

            fileUpdater.CopyBattaryTableToLocalDirectory();
            fileUpdater.CopyCarTableToLocalDirectory();
            //using (var databaseOperations = new OleDatabaseOperations(Properties.Settings.Default.tableName))
            //{
            //    databaseOperations.UpdateSourceTable();
            //}


                InitializeComponent();

            documentManager = new DocumentManager();
            
            selectedCar = null;
            selectedBattary = null;
            selectedSubBattary = null;

            InitCarCollection();
            InitSubBattariesCollection();
        }

        private void InitCarCollection()
        {
            carCollection = new ObservableCollection<Car>();

            List<Car> cars = EntityManager.GetCarCollection();

            cars.ForEach(car => carCollection.Add(car));

            carListView.ItemsSource = carCollection;
        }

        private void InitSubBattariesCollection()
        {
            subBattaryCollection = new ObservableCollection<Battary>();

            var battaryList = EntityManager.GetBattaryCollection();
            battaryList.ForEach(battary => subBattaryCollection.Add(battary));

            subBattaryListView.ItemsSource = subBattaryCollection;
        }

        private void CarListView_Click(object sender, RoutedEventArgs e)
        {
            ClearCarInfoForm();
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var car = item as Car;
                selectedCar = car;
                var battaryList = car.Battaries;
                battaryCollection = new ObservableCollection<Battary>();
                battaryList.ForEach(battary => battaryCollection.Add(battary));
                BattaryListView.ItemsSource = battaryCollection;
                FillCarInfoForm(car);
            }
        }

        private void BattaryListView_Click(object sender, RoutedEventArgs e)
        {
            ClearBattaryInfoForm();
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var battary = item as Battary;
                selectedBattary = battary;
                FillBattaryInfoForm(battary);
            }
        }

        private void SubBattaryListView_Click(object sender, RoutedEventArgs e)
        {
            ClearSubBattaryInfoForm();
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var battary = item as Battary;
                selectedSubBattary = battary;
                FillSubBattaryInfoForm(battary);
            }
        }

        private void FillCarInfoForm(Car car)
        {
            carModelTextBox.Text = car.Model;
            carNumberTextBox.Text = car.Number;
            carDriverNameTextBox.Text = car.DriverName;
            carDriverName2TextBox.Text = car.DriverName2;
        }

        private void ClearCarInfoForm()
        {
            carModelTextBox.Text = string.Empty;
            carNumberTextBox.Text = string.Empty;
            carDriverNameTextBox.Text = string.Empty;
            carDriverName2TextBox.Text = string.Empty;
        }

        private void FillBattaryInfoForm(Battary battary)
        {
            battaryTypeTextBox.Text = battary.Type.ToString();
            battarySerialNumberTextBox.Text = battary.SerialNumber;
            battaryNomenclatureNumberTextBox.Text = battary.NomenclatureNumber;
            battarySetDateTextBox.Text = battary.SetDate?.ToString("dd.MM.yyyy");
        }

        private void ClearBattaryInfoForm()
        {
            battaryTypeTextBox.Text = string.Empty;
            battarySerialNumberTextBox.Text = string.Empty;
            battaryNomenclatureNumberTextBox.Text = string.Empty;
            battarySetDateTextBox.Text = string.Empty;
        }

        private void FillSubBattaryInfoForm(Battary battary)
        {
            subBattaryTypeTextBox.Text = battary.Type.ToString();
            subBattarySerialNumberTextBox.Text = battary.SerialNumber;
            subBattaryNomenclatureNumberTextBox.Text = battary.NomenclatureNumber;
            subBattarySetDateTextBox.Text = battary.SetDate?.ToString("dd.MM.yyyy");
        }

        private void ClearSubBattaryInfoForm()
        {
            subBattaryTypeTextBox.Text = string.Empty;
            subBattarySerialNumberTextBox.Text = string.Empty;
            subBattaryNomenclatureNumberTextBox.Text = string.Empty;
            subBattarySetDateTextBox.Text = string.Empty;
        }

        private CarViewModel GetCarViewModel()
        {
            return new CarViewModel
            {
                DriverName = carDriverNameTextBox.Text,
                DriverName2 = carDriverName2TextBox.Text,
                Model = carModelTextBox.Text,
                Number = carNumberTextBox.Text
            };
        }

        private BattaryViewModel GetBattaryViewModel()
        {
            return new BattaryViewModel
            {
                NomenclatureNumber = battaryNomenclatureNumberTextBox.Text,
                SerialNumber = battarySerialNumberTextBox.Text,
                SetDate = battarySetDateTextBox.Text,
                Type = battaryTypeTextBox.Text,
                WriteOffDate = string.Empty,
                SubreportDate = string.Empty
            };
        }

        private BattaryViewModel GetSubBattaryViewModel()
        {
            return new BattaryViewModel
            {
                NomenclatureNumber = subBattaryNomenclatureNumberTextBox.Text,
                SerialNumber = subBattarySerialNumberTextBox.Text,
                SetDate = subBattarySetDateTextBox.Text,
                Type = subBattaryTypeTextBox.Text,
                WriteOffDate = string.Empty
            };
        }

        private void writeOffButton_Click(object sender, RoutedEventArgs e)
        {
            documentManager.GenerateDoc(DocumentType.writeOfDoc, GetCarViewModel(), GetBattaryViewModel(), null);
        }

        private void SubreportButton_Click(object sender, RoutedEventArgs e)
        {
            documentManager.GenerateDoc(DocumentType.subreportDoc, GetCarViewModel(), GetBattaryViewModel(), null);
        }

        private void SetSubreportedButton_Click(object sender, RoutedEventArgs e)
        {
            documentManager.GenerateDoc(DocumentType.setSubreportedDoc, GetCarViewModel(), GetBattaryViewModel(), GetSubBattaryViewModel());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            documentManager.Dispose();
        }
    }
}
using CarBatteryAccounter.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Model.ViewModels
{
    public class CarViewModel
    {
        /// <summary>
        /// Модель аккумулятора
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Номер машины
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Имя водителя
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// Имя водителя 2 (в конце акта)
        /// </summary>
        public string DriverName2 { get; set; }
    }
}

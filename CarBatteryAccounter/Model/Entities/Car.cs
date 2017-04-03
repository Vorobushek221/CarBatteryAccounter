using CarBatteryAccounter.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Model.Entities
{
    public class Car
    {
        /// <summary>
        /// Модель машины
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
        /// Аккумуляторы
        /// </summary>
        public List<Battaty> Battaries { get; set; }

        public CarViewModel ToViewModel()
        {
            return new CarViewModel
            {
                Model = this.Model,
                DriverName = this.DriverName,
                Number = this.Number
            };
        }
    }
}

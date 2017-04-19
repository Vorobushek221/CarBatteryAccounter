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
        /// Идентификатор машины
        /// </summary>
        public int Id { get; set; }

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
        /// Имя водителя 2 (в конце акта)
        /// </summary>
        public string DriverName2 { get; set; }

        /// <summary>
        /// Аккумуляторы
        /// </summary>
        public List<Battary> Battaries { get; set; }

        public CarViewModel ToViewModel()
        {
            return new CarViewModel
            {
                Model = (Model != null) ? this.Model : string.Empty,
                DriverName = (DriverName != null) ? this.DriverName : string.Empty,
                DriverName2 = (DriverName2 != null) ? this.DriverName2 : string.Empty,
                Number = (Number != null) ? this.Number : string.Empty
            };
        }
    }
}
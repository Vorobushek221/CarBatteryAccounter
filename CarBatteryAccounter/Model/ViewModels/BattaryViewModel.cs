using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Model.ViewModels
{
    public class BattaryViewModel
    {
        /// <summary>
        /// Модель аккумулятора
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Тип аккумулятора
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Заводской номер
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Номенклатурный номер
        /// </summary>
        public string NomenclatureNumber { get; set; }

        /// <summary>
        /// Дата установки
        /// </summary>
        public string SetDate { get; set; }


        /// <summary>
        /// Дата списывания
        /// </summary>
        public string WriteOffDate { get; set; }


        /// <summary>
        /// Дата установки на подотчет
        /// </summary>
        public string SubreportDate { get; set; }
    }
}

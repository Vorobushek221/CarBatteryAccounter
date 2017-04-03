using System;

namespace CarBatteryAccounter.Model.Entities
{
    public class Battaty
    {
        /// <summary>
        /// Модель аккумулятора
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Тип аккумулятора
        /// </summary>
        public BattaryType Type { get; set; }

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
        public DateTime SetDate { get; set; }


        /// <summary>
        /// Дата списывания
        /// </summary>
        public DateTime? WriteOffDate { get; set; }


        /// <summary>
        /// Дата установки на подотчет
        /// </summary>
        public DateTime? SubreportDate { get; set; }
    }
}
using CarBatteryAccounter.Model.ViewModels;
using System;

namespace CarBatteryAccounter.Model.Entities
{
    public class Battary
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
        public DateTime? SetDate { get; set; }


        /// <summary>
        /// Дата списывания
        /// </summary>
        public DateTime? WriteOffDate { get; set; }


        /// <summary>
        /// Дата установки на подотчет
        /// </summary>
        public DateTime? SubreportDate { get; set; }

        public BattaryViewModel ToViewModel()
        {
            return new BattaryViewModel
            {
                Model = (Model != null) ? this.Model : string.Empty,
                Type = (Type != null) ? this.Type.ToString() : string.Empty,
                SerialNumber = (SerialNumber != null) ? this.SerialNumber : string.Empty,
                NomenclatureNumber = (NomenclatureNumber != null) ? this.NomenclatureNumber : string.Empty,
                SetDate = (SetDate != null) ? this.SetDate?.ToString("dd.MM.yyyy") : string.Empty,
                WriteOffDate = (WriteOffDate != null) ? this.WriteOffDate?.ToString("dd.MM.yyyy") : string.Empty,
                SubreportDate = (SubreportDate != null) ? this.SubreportDate?.ToString("dd.MM.yyyy") : string.Empty,
            };
        }
    }
}
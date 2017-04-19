using CarBatteryAccounter.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Model
{
    public static class EntityManager
    {
        public static List<Car> GetCarCollection()
        {
            var databaseOperations = new DatabaseOperations(Properties.Settings.Default.tableName);
            var carsQuery = databaseOperations.GetDistinctCarsQuery();
            var battatiesQuery = databaseOperations.GetBattariesQuery();

            var cars = new List<Car>();


            foreach (var row in carsQuery)
            {
                if (row["Llist"].ToString() != "False")
                {
                    var car = new Car
                    {
                        Id = int.Parse(row["gnaid"].ToString()),
                        Number = row["gn"].ToString(),
                        Model = row["Nm"].ToString(),
                        Battaries = new List<Battary>()
                    };
                    cars.Add(car);
                }
            }

            foreach (var row in battatiesQuery)
            {
                if (row["Llist"].ToString() != "False")
                {

                    var battary = new Battary
                    {
                        Type = row["Tipa"].ToString(),
                        SerialNumber = row["Nomak"].ToString(),
                        NomenclatureNumber = row["Nnomak"].ToString()
                    };

                    DateTime setDate = default(DateTime);
                    DateTime writeOffDate = default(DateTime);

                    if (DateTime.TryParse(row["Datspis"].ToString(), out writeOffDate))
                    {
                        battary.WriteOffDate = writeOffDate;
                    }
                    else
                    {
                        battary.WriteOffDate = null;
                    }

                    if (DateTime.TryParse(row["Datpol"].ToString(), out setDate))
                    {
                        battary.SetDate = setDate;
                    }
                    else
                    {
                        battary.SetDate = null;
                    }

                    if (battary.WriteOffDate != null)
                    {
                        continue;
                    }

                    int carId = int.Parse(row["gnaid"].ToString());
                    cars.Where(car => car.Id == carId).First().Battaries.Add(battary);
                }
            }
            return cars;
        }

        public static List<Battary> GetBattaryCollection()
        {
            var battaryList = new List<Battary>();

            var databaseOperations = new DatabaseOperations(Properties.Settings.Default.tableName);
            var battatiesQuery = databaseOperations.GetBattariesQuery();

            foreach (var row in battatiesQuery)
            {
                if (row["Llist"].ToString() == "False")
                {
                    var battary = new Battary
                    {
                        Type = row["Tipa"].ToString(),
                        SerialNumber = row["Nomak"].ToString(),
                        NomenclatureNumber = row["Nnomak"].ToString()
                    };

                    DateTime setDate = default(DateTime);
                    DateTime writeOffDate = default(DateTime);

                    if (DateTime.TryParse(row["Datspis"].ToString(), out writeOffDate))
                    {
                        battary.WriteOffDate = writeOffDate;
                    }
                    else
                    {
                        battary.WriteOffDate = null;
                    }

                    if (DateTime.TryParse(row["Datpol"].ToString(), out setDate))
                    {
                        battary.SetDate = setDate;
                    }
                    else
                    {
                        battary.SetDate = null;
                    }

                    if (battary.WriteOffDate != null)
                    {
                        continue;
                    }
                    else
                    {
                        battaryList.Add(battary);
                    }
                }

            }

            return battaryList;
        }
    }

    
}

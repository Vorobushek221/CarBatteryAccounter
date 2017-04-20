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
            var databaseOperations = new OleDatabaseOperations();
            var carsQuery = databaseOperations.GetDistinctCarsQuery();
            var battatiesQuery = databaseOperations.GetBattariesQuery();

            var cars = new List<Car>();


            foreach (var row in carsQuery)
            {
                if (row["llist"].ToString() != "False")
                {
                    var car = new Car
                    {
                        Id = int.Parse(row["gnaid"].ToString()),
                        Number = row["gn"].ToString(),
                        Model = row["nm"].ToString(),
                        Battaries = new List<Battary>()
                    };
                    cars.Add(car);
                }
            }

            foreach (var row in battatiesQuery)
            {
                if (row["llist"].ToString() != "False")
                {

                    var battary = new Battary
                    {
                        Type = row["tipa"].ToString(),
                        SerialNumber = row["nomak"].ToString(),
                        NomenclatureNumber = row["nnomak"].ToString()
                    };

                    DateTime setDate = default(DateTime);
                    DateTime writeOffDate = default(DateTime);

                    if (DateTime.TryParse(row["datspis"].ToString(), out writeOffDate))
                    {
                        battary.WriteOffDate = writeOffDate;
                    }
                    else
                    {
                        battary.WriteOffDate = null;
                    }

                    if (DateTime.TryParse(row["datpol"].ToString(), out setDate))
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

            var databaseOperations = new OleDatabaseOperations();
            var battatiesQuery = databaseOperations.GetBattariesQuery();

            foreach (var row in battatiesQuery)
            {
                if (row["llist"].ToString() == "False")
                {
                    var battary = new Battary
                    {
                        Type = row["tipa"].ToString(),
                        SerialNumber = row["nomak"].ToString(),
                        NomenclatureNumber = row["nnomak"].ToString()
                    };

                    DateTime setDate = default(DateTime);
                    DateTime writeOffDate = default(DateTime);

                    if (DateTime.TryParse(row["datspis"].ToString(), out writeOffDate))
                    {
                        battary.WriteOffDate = writeOffDate;
                        if(writeOffDate.Year < 1900)
                        {
                            var a = 1;
                        }
                    }
                    else
                    {
                        battary.WriteOffDate = null;
                    }

                    if (DateTime.TryParse(row["datpol"].ToString(), out setDate))
                    {
                        battary.SetDate = setDate;
                    }
                    else
                    {
                        battary.SetDate = null;
                    }

                    if (battary.WriteOffDate != null)
                    {
                        if (battary.WriteOffDate?.Year != 1899)
                        {
                            continue;
                        }
                    }

                    battaryList.Add(battary);

                }

            }
            return battaryList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Travel.Saga.Models;

namespace Travel.Saga.Repository
{
   public  class CsvReader : ICsvReader
    {
        private readonly string _dataFile = ConfigurationManager.AppSettings["dataFile"].ToString();
        public  List<Basket> GetBasketsFromCSV()
        {
            List<Basket> values = File.ReadAllLines(_dataFile)
                                           .Skip(1)
                                           .Select(FromCSV)
                                           .ToList();
            return values;
        }
        public  Basket FromCSV(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Basket basket = new Basket()
            {
                TransactionNumber=Guid.Parse(values[0]),
                NumberOfPassengers=string.IsNullOrEmpty(values[1])==false ? Convert.ToInt32(values[1]) :0,
                Domain= string.IsNullOrEmpty(values[2]) == false ? Convert.ToInt32(values[2]):0,
                AgentId = string.IsNullOrEmpty(values[3]) == false ? Convert.ToInt32(values[3]):0,
                ReferrerUrl = Convert.ToString(values[4]),
                CreatedDateTime = DateTime.Parse(values[5]),
                UserId = Convert.ToString(values[6]),
                SelectedCurrency = Convert.ToString(values[7]),
                ReservationSystem = Convert.ToString(values[8])
            };
            return basket;
        }
    }
}

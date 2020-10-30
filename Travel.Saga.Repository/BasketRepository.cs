using System;
using System.Collections.Generic;
using System.Linq;
using Travel.Saga.Models;

namespace Travel.Saga.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ICsvReader _csvReader;
        public BasketRepository(ICsvReader csvReader)
        {
            _csvReader = csvReader;
        }

        public List<Basket> GetAllBaskets(int domain=0)
        {
            var lst = GetAll();
            if (domain != 0)
            {
                lst = lst.Where(x => x.Domain == domain).ToList();
            }
            return lst.OrderByDescending(x=>x.CreatedDateTime)
                .ToList();
        }

        public List<Basket> GetAll()
        {
            return _csvReader.GetBasketsFromCSV();
        }

        public Basket GetBasketByTransactionNumber(Guid transactionNumber)
        {
            return GetAll()
                .FirstOrDefault(x => x.TransactionNumber == transactionNumber);
        }
    }
}

using System;
using System.Collections.Generic;
using Travel.Saga.Models;

namespace Travel.Saga.Repository
{
    public interface IBasketRepository
    {
        Basket GetBasketByTransactionNumber(Guid transactionNumber);

        List<Basket> GetAllBaskets(int domain = 1);
        List<Basket> GetAll();
    }
}
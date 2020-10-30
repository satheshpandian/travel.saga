using System.Collections.Generic;
using Travel.Saga.Models;

namespace Travel.Saga.Repository
{
    public interface ICsvReader
    {
        List<Basket> GetBasketsFromCSV();

        Basket FromCSV(string csvLine);
    }
}
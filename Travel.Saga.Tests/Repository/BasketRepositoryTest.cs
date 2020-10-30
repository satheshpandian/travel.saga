using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Travel.Saga.Models;
using Travel.Saga.Repository;

namespace Travel.Saga.Tests.Repository
{

    public class BasketRepositoryTest
    {
        private readonly Mock<ICsvReader> _mockCsvReader;
        public BasketRepositoryTest()
        {
            //Arrange
            _mockCsvReader = new Mock<ICsvReader>();
            _mockCsvReader.Setup(x => x.GetBasketsFromCSV()).Returns(new List<Basket>()
            {
                new Basket()
                {
                    NumberOfPassengers = 1,
                    Domain = 1,
                    TransactionNumber = Guid.NewGuid(),
                    CreatedDateTime = DateTime.Now.AddYears(-1)
                },
                new Basket()
                {
                    NumberOfPassengers = 12,
                    Domain = 1,
                    TransactionNumber = Guid.NewGuid(),
                    CreatedDateTime = DateTime.Now.AddYears(-2)
                },
                new Basket()
                {
                NumberOfPassengers = 1,
                Domain = 2,
                TransactionNumber = Guid.NewGuid(),
                CreatedDateTime = DateTime.Now.AddYears(-1)
            },
            new Basket()
            {
                NumberOfPassengers = 12,
                Domain = 3,
                TransactionNumber = Guid.NewGuid(),
                CreatedDateTime = DateTime.Now.AddYears(-2)
            },
            new Basket()
            {
                NumberOfPassengers = 12,
                Domain = 3,
                TransactionNumber = Guid.Parse("0002b3be-4f0b-418a-b7dd-670494981a89"),
                CreatedDateTime = DateTime.Now.AddYears(-2)
            }
            });
        }
        [TestCase(1,2)]
        [TestCase(2, 1)]
        [TestCase(4, 0)]
        public void TestGetAllBaskets(int domain, int expectedCount)
        {
            
            BasketRepository basketRepository = new BasketRepository(_mockCsvReader.Object);
            //Act
            var baskets= basketRepository.GetAllBaskets(domain);

           //Assert
           Assert.NotNull(baskets);
           Assert.AreEqual(expectedCount,baskets.Count);
        }

        
        [TestCase("0002b3be-4f0b-418a-b7dd-670494981a89", 1)]
        [TestCase("0003af0c-24a5-4948-81e4-5f19abf2f565", 0)]
        public void GetBasketByTransactionNumber(string transactionNumber, int expectedCount)
        {

            BasketRepository basketRepository = new BasketRepository(_mockCsvReader.Object);
            //Act
            var basket = basketRepository.GetBasketByTransactionNumber(Guid.Parse(transactionNumber));

            //Assert
            if (expectedCount > 0)
            {
                Assert.NotNull(basket);
                Assert.AreEqual(transactionNumber, basket.TransactionNumber.ToString());
            }
            else
            {
                Assert.Null(basket);
            }

        }
    }
}

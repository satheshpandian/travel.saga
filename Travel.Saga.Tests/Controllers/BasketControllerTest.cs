using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Travel.Saga.Controllers;
using Travel.Saga.Models;
using Travel.Saga.Repository;

namespace Travel.Saga.Tests.Controllers
{

    public class BasketControllerTest
    {
        private readonly Mock<IBasketRepository> _mockBasketRepository;
        public BasketControllerTest()
        {
            //Arrange
            _mockBasketRepository = new Mock<IBasketRepository>();
            
        }
        [TestCase(1,2)]
        [TestCase(2, 1)]
        [TestCase(4, 0)]
        public void TestGetAllBaskets(int domain, int expectedCount)
        {
            _mockBasketRepository.Setup(x => x.GetAllBaskets(domain)).Returns(TestData(domain));
            BasketController basketController = new BasketController(_mockBasketRepository.Object);
            //Act
            var baskets= basketController.GetAllBaskets(domain).ToList();

           //Assert
           Assert.NotNull(baskets);
           Assert.AreEqual(expectedCount,baskets.Count);
        }

        
        [TestCase("0002b3be-4f0b-418a-b7dd-670494981a89", 1)]
        [TestCase("0003af0c-24a5-4948-81e4-5f19abf2f565", 0)]
        public void GetBasketByTransactionNumber(string transactionNumber, int expectedCount)
        {
            _mockBasketRepository.Setup(x => x.GetBasketByTransactionNumber(Guid.Parse(transactionNumber))).Returns(TestData().FirstOrDefault(x=>x.TransactionNumber.ToString()==transactionNumber));
            BasketController basketController = new BasketController(_mockBasketRepository.Object);
            //Act
            var basket = basketController.GetBasketByTransactionNumber(Guid.Parse(transactionNumber)) ;

            
            //Assert
            if (expectedCount > 0)
            {
                var output = ((System.Web.Http.Results.OkNegotiatedContentResult<Travel.Saga.Models.Basket>)basket).Content;
                Assert.NotNull(output);
               Assert.AreEqual(transactionNumber, output.TransactionNumber.ToString());
            }
            else
            {
                Assert.AreEqual("NotFoundResult", basket.GetType().Name);
            }

        }

        private List<Basket> TestData(int domain =0)
        {
            List <Basket>  baskets=new List<Basket>()
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
            };
            if (domain != 0)
            {
                baskets = baskets.Where(x => x.Domain == domain).ToList();
            }
            return baskets;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Travel.Saga.Models;
using Travel.Saga.Repository;

namespace Travel.Saga.Controllers
{
    [RoutePrefix("api/basket")]
    public class BasketController : ApiController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [Route("getallbaskets/{domain?}")]
        public IEnumerable<Basket> GetAllBaskets(int domain = 0)
        {
            return _basketRepository.GetAllBaskets(domain);
        }

        [Route("getbasketbytransactionnumber/{id}")]
        [ResponseType(typeof(Basket))]
        public IHttpActionResult GetBasketByTransactionNumber(Guid id)
        {
            var basket = _basketRepository.GetBasketByTransactionNumber(id);
            if (basket == null)
            {
                return NotFound();
            }
            return Ok(basket);
        }
    }
}

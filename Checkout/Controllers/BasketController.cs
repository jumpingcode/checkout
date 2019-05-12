using Checkout.Models.ViewModels;
using Checkout.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Checkout.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public BasketViewModel Create()
        {
            return _basketService.Create();
        }

        [HttpGet]
        [Route("{basketId:guid}")]
        public ActionResult<BasketViewModel> Get(Guid basketId)
        {
            var basket = _basketService.Get(basketId);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }

        [HttpPut]
        [Route("{basketId:guid}/add/{itemId:guid}")]
        public ActionResult<BasketViewModel> Add(Guid basketId, Guid itemId)
        {
            var basket = _basketService.Add(basketId, itemId);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }

        [HttpPut]
        [Route("{basketId:guid}/remove/{itemId:guid}")]
        public ActionResult<BasketViewModel> Remove(Guid basketId, Guid itemId)
        {
            var basket = _basketService.Remove(basketId, itemId);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }

        [HttpPut]
        [Route("{basketId:guid}/clear")]
        public ActionResult<BasketViewModel> Clear(Guid basketId)
        {
            var basket = _basketService.Clear(basketId);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }
    }
}

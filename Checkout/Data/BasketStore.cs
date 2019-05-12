using Checkout.Data.Interfaces;
using Checkout.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Data
{
    public class BasketStore : IBasketStore
    {
        private readonly List<BasketEntity> _baskets;

        public BasketStore()
        {
            _baskets = new List<BasketEntity>();
        }

        public Guid Create(Guid token)
        {
            var basket = new BasketEntity
            {
                BasketId = Guid.NewGuid(),
                Token = token
            };
            _baskets.Add(basket);

            return basket.BasketId;
        }

        public BasketEntity Get(Guid basketId)
        {
            return _baskets.SingleOrDefault(x => x.BasketId == basketId);
        }

        public void Update(BasketEntity basket)
        {
            var originalBasket = _baskets.SingleOrDefault(x => x.BasketId == basket.BasketId);
            _baskets.Remove(originalBasket);
            _baskets.Add(basket);
        }
    }
}

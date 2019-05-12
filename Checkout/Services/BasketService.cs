using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Data.Interfaces;
using Checkout.Models.Data;
using Checkout.Models.Mappers.Interfaces;
using Checkout.Models.ViewModels;
using Checkout.Services.Interfaces;
using Checkout.Wrappers.Interfaces;

namespace Checkout.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketStore _basketStore;
        private readonly IItemStore _itemStore;
        private readonly IGuidWrapper _guidWrapper;
        private readonly IBasketMapper _basketMapper;

        public BasketService(IBasketStore basketStore, IItemStore itemStore, IGuidWrapper guidWrapper, IBasketMapper basketMapper)
        {
            _basketStore = basketStore;
            _itemStore = itemStore;
            _guidWrapper = guidWrapper;
            _basketMapper = basketMapper;
        }

        public BasketViewModel Create()
        {
            var userToken = _guidWrapper.NewGuid();
            var basketId = _basketStore.Create(userToken);

            return new BasketViewModel
            {
                BasketId = basketId,
                Token = userToken
            };
        }

        public BasketViewModel Get(Guid basketId)
        {
            var basket = _basketStore.Get(basketId);
            if (basket == null) return null;
            return _basketMapper.MapFrom(basket);
        }

        public BasketViewModel Clear(Guid basketId)
        {
            var basket = _basketStore.Get(basketId);
            if (basket == null) return null;
            basket.Items = new List<ItemEntity>();
            _basketStore.Update(basket);

            return _basketMapper.MapFrom(basket);
        }

        public BasketViewModel Add(Guid basketId, Guid itemId)
        {
            var basket = _basketStore.Get(basketId);
            if (basket == null) return null;
            var item = _itemStore.Get(itemId);
            basket.Items.Add(item);
            _basketStore.Update(basket);

            return _basketMapper.MapFrom(basket);
        }

        public BasketViewModel Remove(Guid basketId, Guid itemId)
        {
            var basket = _basketStore.Get(basketId);
            if (basket == null) return null;
            var item = _itemStore.Get(itemId);
            var itemToRemove = basket.Items.SingleOrDefault(x => x.ItemId == itemId);
            basket.Items.Remove(itemToRemove);
            _basketStore.Update(basket);

            return _basketMapper.MapFrom(basket);
        }
    }
}

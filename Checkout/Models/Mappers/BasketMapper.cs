using Checkout.Models.Data;
using Checkout.Models.Mappers.Interfaces;
using Checkout.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Models.Mappers
{
    public class BasketMapper : IBasketMapper
    {
        private readonly IItemMapper _itemMapper;

        public BasketMapper(IItemMapper itemMapper)
        {
            _itemMapper = itemMapper;
        }

        public BasketViewModel MapFrom(BasketEntity basketEntity)
        {
            var itemViewModels = new List<ItemViewModel>();
            var items = basketEntity.Items.GroupBy(x => x.ItemId);

            foreach (var item in items)
            {
                var itemViewModel = _itemMapper.MapFrom(item.First());
                itemViewModel.Quantity = item.Count();
                itemViewModels.Add(itemViewModel);
            }

            return new BasketViewModel
            {
                BasketId = basketEntity.BasketId,
                Token = basketEntity.Token,
                Items = itemViewModels,
                Total = basketEntity.Items.Sum(x => x.Cost)
            };
        }
    }
}

using Checkout.Models.Data;
using Checkout.Models.Mappers.Interfaces;
using Checkout.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Models.Mappers
{
    public class ItemMapper : IItemMapper
    {
        public ItemViewModel MapFrom(ItemEntity itemEntity)
        {
            return new ItemViewModel
            {
                ItemId = itemEntity.ItemId,
                Name = itemEntity.Name,
                Cost = itemEntity.Cost
            };
        }
    }
}

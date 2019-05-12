using System;
using System.Collections.Generic;
using Checkout.Models.ViewModels;

namespace Checkout.Models.Data
{
    public class BasketEntity
    {
        public Guid BasketId { get; set; }

        public Guid Token { get; set; }

        public List<ItemEntity> Items { get; set; } = new List<ItemEntity>();
        public List<ItemViewModel> Select { get; internal set; }
    }
}

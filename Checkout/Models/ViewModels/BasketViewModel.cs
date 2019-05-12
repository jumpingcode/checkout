using System;
using System.Collections.Generic;

namespace Checkout.Models.ViewModels
{
    public class BasketViewModel
    {
        public Guid BasketId { get; set; }

        public Guid Token { get; set; }

        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();

        public decimal Total { get; set; }
    }
}

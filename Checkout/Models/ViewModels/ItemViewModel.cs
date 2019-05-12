using System;

namespace Checkout.Models.ViewModels
{
    public class ItemViewModel
    {
        public Guid ItemId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }
    }
}
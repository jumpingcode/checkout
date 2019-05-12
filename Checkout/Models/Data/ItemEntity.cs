using System;

namespace Checkout.Models.Data
{
    public class ItemEntity
    {
        public Guid ItemId { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }
    }
}
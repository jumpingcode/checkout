using Bogus;
using Checkout.Data.Interfaces;
using Checkout.Models.Data;
using System;

namespace Checkout.Data
{
    public class ItemStore : IItemStore
    {
        private readonly Faker _faker;

        public ItemStore()
        {
            _faker = new Faker();
        }

        public ItemEntity Get(Guid itemId)
        {
            return new ItemEntity
            {
                ItemId = itemId,
                Name = _faker.Commerce.ProductName(),
                Cost = _faker.Random.Decimal() * _faker.Random.Int(min: 0, max: 1000)
            };
        }
    }
}

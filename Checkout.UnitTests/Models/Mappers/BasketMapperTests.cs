using Bogus;
using Checkout.Models.Data;
using Checkout.Models.Mappers;
using Checkout.Models.Mappers.Interfaces;
using Checkout.Models.ViewModels;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Checkout.UnitTests.Models.Mappers
{
    public class BasketMapperTests
    {
        private readonly Mock<IItemMapper> _itemMapper;
        private readonly BasketMapper _basketMapper;
        private readonly Faker _faker;

        public BasketMapperTests()
        {
            _itemMapper = new Mock<IItemMapper>();
            _basketMapper = new BasketMapper(_itemMapper.Object);
            _faker = new Faker();
        }

        [Fact]
        public void MapFrom_ShouldMapFromEntityToViewModel()
        {
            // Given
            var basket = CreateFakeBasket();
            var itemViewModel = new ItemViewModel();
            _itemMapper.Setup(x => x.MapFrom(It.IsAny<ItemEntity>())).Returns(itemViewModel);

            // When
            var result = _basketMapper.MapFrom(basket);

            // Then
            result.BasketId.ShouldBe(basket.BasketId);
            result.Token.ShouldBe(basket.Token);
            result.Items.Count.ShouldBe(basket.Items.Count);
            result.Total.ShouldBe(basket.Items.Sum(x => x.Cost));
        }

        private BasketEntity CreateFakeBasket()
        {
            var items = new List<ItemEntity>();
            var numberOfItems = _faker.Random.Int(min: 0, max: 5);

            for(var index = 0; index < numberOfItems; index++)
            {
                items.Add(CreateFakeItem());
            }

            return new BasketEntity
            {
                BasketId = _faker.Random.Guid(),
                Token = _faker.Random.Guid(),
                Items = items
            };
        }

        private ItemEntity CreateFakeItem()
        {
            return new ItemEntity
            {
                ItemId = _faker.Random.Guid(),
                Name = _faker.Commerce.ProductName(),
                Cost = _faker.Random.Decimal() * _faker.Random.Int(0, 1000)
            };
        }
    }
}

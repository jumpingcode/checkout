using Bogus;
using Checkout.Models.Data;
using Checkout.Models.Mappers;
using Shouldly;
using Xunit;

namespace Checkout.UnitTests.Models.Mappers
{
    public class ItemMapperTests
    {
        private readonly ItemMapper _itemMapper;
        private readonly Faker _faker;

        public ItemMapperTests()
        {
            _itemMapper = new ItemMapper();
            _faker = new Faker();
        }

        [Fact]
        public void MapFrom_ShouldMapPropertiesBetweenEntityAndViewModel()
        {
            // Given
            var item = CreateFakeItem();

            // When
            var result = _itemMapper.MapFrom(item);

            // Then
            result.ItemId.ShouldBe(result.ItemId);
            result.Name.ShouldBe(result.Name);
            result.Quantity.ShouldBe(result.Quantity);
            result.Cost.ShouldBe(result.Cost);
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

using Bogus;
using Checkout.Data.Interfaces;
using Checkout.Models.Data;
using Checkout.Models.Mappers.Interfaces;
using Checkout.Models.ViewModels;
using Checkout.Services;
using Checkout.Wrappers.Interfaces;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Checkout.UnitTests.Services
{
    public class BasketServiceTests
    {
        private readonly Mock<IBasketStore> _basketStore;
        private readonly Mock<IItemStore> _itemStore;
        private readonly Mock<IGuidWrapper> _guidWrapper;
        private readonly Mock<IBasketMapper> _basketMapper;
        private readonly BasketService _basketService;

        public BasketServiceTests()
        {
            _basketStore = new Mock<IBasketStore>();
            _itemStore = new Mock<IItemStore>();
            _guidWrapper = new Mock<IGuidWrapper>();
            _basketMapper = new Mock<IBasketMapper>();
            _basketService = new BasketService(_basketStore.Object, _itemStore.Object, _guidWrapper.Object, _basketMapper.Object);
        }

        [Fact]
        public void Create_ShouldCreateBasketInBasketStore()
        {
            // Given
            var token = Guid.NewGuid();
            var basketId = Guid.NewGuid();
            _guidWrapper.Setup(x => x.NewGuid()).Returns(token);
            _basketStore.Setup(x => x.Create(token)).Returns(basketId);

            // When
            var result = _basketService.Create();

            // Then
            result.BasketId.ShouldBe(basketId);
            result.Token.ShouldBe(token);
        }

        [Fact]
        public void Get_ShouldFetchBasketDetailsFromBasketStore()
        {
            // Given
            var basketId = Guid.NewGuid();
            var basketEntity = new BasketEntity { BasketId = basketId };
            _basketStore.Setup(x => x.Get(basketId)).Returns(basketEntity);
            var basketViewModel = new BasketViewModel { BasketId = basketId };
            _basketMapper.Setup(x => x.MapFrom(basketEntity)).Returns(basketViewModel);

            // When
            var result = _basketService.Get(basketId);

            // Then
            result.ShouldBe(basketViewModel);
        }

        [Fact]
        public void Clear_ShouldRemoveItemsFromBasket_AndCallUpdate()
        {
            // Given
            var basketId = Guid.NewGuid();
            var basketEntity = new BasketEntity { BasketId = basketId, Items = new List<ItemEntity> { new ItemEntity(), new ItemEntity() } };
            _basketStore.Setup(x => x.Get(basketId)).Returns(basketEntity);
            var basketViewModel = new BasketViewModel { BasketId = basketId };
            _basketMapper.Setup(x => x.MapFrom(basketEntity)).Returns(basketViewModel);

            // When
            var result = _basketService.Clear(basketId);

            // Then
            result.ShouldBe(basketViewModel);
            _basketStore.Verify(x => x.Update(It.Is<BasketEntity>(p => p.Items.Count == 0)));
        }

        [Fact]
        public void Add_ShouldAddItemToBasket_AndCallUpdate()
        {
            // Given
            var basketId = Guid.NewGuid();
            var basketEntity = new BasketEntity { BasketId = basketId };
            _basketStore.Setup(x => x.Get(basketId)).Returns(basketEntity);
            var itemId = Guid.NewGuid();
            var itemEntity = new ItemEntity { ItemId = itemId };
            _itemStore.Setup(x => x.Get(itemId)).Returns(itemEntity);
            var basketViewModel = new BasketViewModel { BasketId = basketId };
            _basketMapper.Setup(x => x.MapFrom(It.IsAny<BasketEntity>())).Returns(basketViewModel);

            // When 
            var result = _basketService.Add(basketId, itemId);

            // Then
            result.ShouldBe(basketViewModel);
            _basketStore.Verify(x => x.Update(It.Is<BasketEntity>(p => p.Items.Any(y => y.ItemId == itemId))));
        }

        [Fact]
        public void Remove_ShouldRemoveItemToBasket_AndCallUpdate()
        {
            // Given
            var basketId = Guid.NewGuid();
            var basketEntity = new BasketEntity { BasketId = basketId };
            var itemId = Guid.NewGuid();
            var itemEntity = new ItemEntity { ItemId = itemId };
            basketEntity.Items.Add(itemEntity);
            _basketStore.Setup(x => x.Get(basketId)).Returns(basketEntity);
            _itemStore.Setup(x => x.Get(itemId)).Returns(itemEntity);
            var basketViewModel = new BasketViewModel { BasketId = basketId };
            _basketMapper.Setup(x => x.MapFrom(It.IsAny<BasketEntity>())).Returns(basketViewModel);

            // When 
            var result = _basketService.Remove(basketId, itemId);

            // Then
            result.ShouldBe(basketViewModel);
            _basketStore.Verify(x => x.Update(It.Is<BasketEntity>(p => !p.Items.Any(y => y.ItemId == itemId))));
        }
    }
}

using Checkout.Controllers;
using Checkout.Models.ViewModels;
using Checkout.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using Xunit;

namespace Checkout.UnitTests.Controllers
{
    public class BasketControllerTests
    {
        private readonly Mock<IBasketService> _basketService;
        private readonly BasketController _basketController;

        public BasketControllerTests()
        {
            _basketService = new Mock<IBasketService>();
            _basketController = new BasketController(_basketService.Object);
        }

        [Fact]
        public void Create_ShouldCall_BasketServiceCreate()
        {
            // Given
            var basketViewModel = new BasketViewModel();
            _basketService.Setup(x => x.Create()).Returns(basketViewModel);

            // When
            var result = _basketController.Create();

            // Then
            result.ShouldBe(basketViewModel);
        }

        [Fact]
        public void Get_ShouldRetrieveBasket_ForGivenId()
        {
            // Given
            var basketId = Guid.NewGuid();
            var basketViewModel = new BasketViewModel { BasketId = basketId };
            _basketService.Setup(x => x.Get(basketId)).Returns(basketViewModel);

            // When
            var result = _basketController.Get(basketId);

            // Then
            result.Value.ShouldBe(basketViewModel);
        }

        [Fact]
        public void Get_ShouldReturnNotFound_WhenNoBasket()
        {
            // When
            var result = _basketController.Get(basketId: Guid.NewGuid());

            // Then
            result.Result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public void Clear_ShouldCall_BasketServiceClear()
        {
            // Given
            var basketId = Guid.NewGuid();
            var basketViewModel = new BasketViewModel { BasketId = basketId };
            _basketService.Setup(x => x.Clear(basketId)).Returns(basketViewModel);

            // When
            var result = _basketController.Clear(basketId);

            // Then
            result.Value.ShouldBe(basketViewModel);
        }

        [Fact]
        public void Clear_ShouldReturnNotFound_WhenNoBasket()
        {
            // When
            var result = _basketController.Clear(basketId: Guid.NewGuid());

            // Then
            result.Result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public void Add_ShouldCall_BasketServiceAdd()
        {
            // Given
            var basketId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var basketViewModel = new BasketViewModel { BasketId = basketId };
            _basketService.Setup(x => x.Add(basketId, itemId)).Returns(basketViewModel);

            // When
            var result = _basketController.Add(basketId, itemId);

            // Then
            result.Value.ShouldBe(basketViewModel);
        }

        [Fact]
        public void Add_ShouldReturnNotFound_WhenNoBasket()
        {
            // When
            var result = _basketController.Add(basketId: Guid.NewGuid(), itemId: Guid.NewGuid());

            // Then
            result.Result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public void Remove_ShouldCall_BasketServiceRemove()
        {
            // Given
            var basketId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var basketViewModel = new BasketViewModel { BasketId = basketId };
            _basketService.Setup(x => x.Remove(basketId, itemId)).Returns(basketViewModel);

            // When
            var result = _basketController.Remove(basketId, itemId);

            // Then
            result.Value.ShouldBe(basketViewModel);
        }

        [Fact]
        public void Remove_ShouldReturnNotFound_WhenNoBasket()
        {
            // When
            var result = _basketController.Remove(basketId: Guid.NewGuid(), itemId: Guid.NewGuid());

            // Then
            result.Result.ShouldBeOfType<NotFoundResult>();
        }
    }
}

using System;
using Checkout.Models.ViewModels;

namespace Checkout.Services.Interfaces
{
    public interface IBasketService
    {
        BasketViewModel Create();

        BasketViewModel Get(Guid basketId);

        BasketViewModel Clear(Guid basketId);

        BasketViewModel Add(Guid basketId, Guid itemId);

        BasketViewModel Remove(Guid basketId, Guid itemId);
    }
}
using Checkout.Models.Data;
using System;

namespace Checkout.Data.Interfaces
{
    public interface IBasketStore
    {
        Guid Create(Guid token);

        BasketEntity Get(Guid basketId);
        void Update(BasketEntity basket);
    }
}

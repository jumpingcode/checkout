using Checkout.Models.Data;
using System;

namespace Checkout.Data.Interfaces
{
    public interface IItemStore
    {
        ItemEntity Get(Guid itemId);
    }
}

using Checkout.Models.Data;
using Checkout.Models.ViewModels;

namespace Checkout.Models.Mappers.Interfaces
{
    public interface IItemMapper
    {
        ItemViewModel MapFrom(ItemEntity itemEntity);
    }
}

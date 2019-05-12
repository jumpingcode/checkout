using System;
using Checkout.Wrappers.Interfaces;

namespace Checkout.Wrappers
{
    public class GuidWrapper : IGuidWrapper
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}

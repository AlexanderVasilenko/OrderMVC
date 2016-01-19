using System.Collections.Generic;

namespace OrderForm.Domain.Abstract
{
    public interface IOrderRepository
    {
        Entities.Order Order { get; }
    }
}

using System.Collections.Generic;

namespace OrderForm.Domain.Abstract
{
    public interface IOrdersRepository
    {
        IEnumerable<Entities.Order> Orders { get; }
    }
}

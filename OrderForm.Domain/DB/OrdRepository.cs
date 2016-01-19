using System.Collections.Generic;
using OrderForm.Domain.Abstract;

namespace OrderForm.Domain.DB
{
    public class OrdRepository:IOrdersRepository
    {
        OrdDbContext context = new OrdDbContext();
        public IEnumerable<Entities.Order> Orders
        {
            get { return context.Orders; }
        }
    }
}

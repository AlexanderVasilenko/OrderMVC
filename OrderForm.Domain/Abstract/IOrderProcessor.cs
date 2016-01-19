using OrderForm.Domain.Entities;

namespace OrderForm.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Order order,string country);
    }
}

using OrderForm.Domain.Entities;

namespace OrderForm.Domain.Abstract
{
    public interface ISaveOrderProcessor
    {
        void ProcessOrder(Order order, string country);
    }
}

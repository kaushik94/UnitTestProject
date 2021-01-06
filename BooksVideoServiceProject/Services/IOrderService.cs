using BooksVideoServiceProject.Entities;

namespace BooksVideoServiceProject.Services
{
    public interface IOrderService
    {
        void Process(Order order, Customer customer);

        // bool isOrderProcessable(Order order);

        // bool isShippableOrder(Order order);

        // void createShippingSlip(Order order);
    }
}

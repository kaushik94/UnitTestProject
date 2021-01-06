using BooksVideoServiceProject.Entities;
using System;
using System.Text.RegularExpressions;

namespace BooksVideoServiceProject.Services
{
    public class OrderService : IOrderService
    {

        private Customer getCustomerOfOrder(Order order) {
            throw new NotImplementedException();
        }
        private bool isOrderLineVideoMemberShip(string line) {
            Regex video_pattern = new Regex(@"\bVideo Club Membership\w*\b");
            return video_pattern.IsMatch(line);
        }

        private bool isOrderLineBookMemberShip(string line) {
            Regex book_pattern = new Regex(@"\bBook Club Membership\w*\b");
            return book_pattern.IsMatch(line);
        }

        private bool isOrderLineShippingBook(string line) {
            String start_with = "Book";
            String not_start_with = "Book Club Membership";

            return line.StartsWith(start_with) && !line.StartsWith(not_start_with);
        }

        private bool isOrderLineShippingVideo(string line) {
            String start_with = "Video";
            String not_start_with = "Video Club Membership";

            return line.StartsWith(start_with) && !line.StartsWith(not_start_with);
        }

        public void Process(Order order, Customer customer)
        {
            applyAllRulesIfApplicable(order, customer);
        }

        private void createShippingSlipIfApplicable(Order order, Customer customer)
        {
            if (isOrderProcessable(order)) {
                applyMembershipWhereApplicable(order, customer);
            }
        }
        private void applyAllRulesIfApplicable(Order order, Customer customer)
        {
            applyMembershipWhereApplicable(order, customer);
            createShippingSlipWhereApplicable(order, customer);
        }
        private bool isOrderProcessable(Order order)
        {
            return true;
        }

        private void applyMembership(String type, Customer customer)
        {
            customer.AddMembership(type);
        }
        public void applyMembershipWhereApplicable(Order order, Customer customer)
        {
            foreach(var item in order.OrderLines) {
                if (isOrderLineBookMemberShip(item.Line)) {
                    applyMembership("book", customer);
                }
                if (isOrderLineVideoMemberShip(item.Line)) {
                    applyMembership("video", customer);
                }
            }
        }

        public void createShippingSlipWhereApplicable(Order order, Customer customer)
        {
            foreach(var item in order.OrderLines) {
                if (isOrderLineShippingBook(item.Line)) {
                    createShippingSlip(item, order, customer);
                }
                if (isOrderLineShippingVideo(item.Line)) {
                    createShippingSlip(item, order, customer);
                }
            }
        }

        public void createShippingSlip(OrderLine order_line, Order order, Customer customer)
        {
            string shipping_slip = "";
            shipping_slip += customer.Id+"\n";
            shipping_slip += order.Id+"\n";
            shipping_slip += "Item:";
            shipping_slip += order_line.Line+"\n";
            shipping_slip += "Address:";
            shipping_slip += customer.ShippingAddress;

            customer.addShippingSlip(shipping_slip);
        }

    }
}

using BooksVideoServiceProject.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BooksVideoServiceProject.Services;
using System.IO;

using System;
using System.Configuration;
using System.Collections.Specialized;


namespace BooksVideoServiceProject.Tests
{
    [TestClass]
    public class OrderServiceUnitTest
    {
        [TestMethod]
        public void TestCustomerMembership()
        {
            // arrange
            Customer customer = new Customer("Mr. Bryan Walton", "Bryan.Walton@gmail.com");
            Order order = new Order();
            order.addOrderLine("Book Club Membership");

            // act
            OrderService os = new OrderService();
            os.Process(order, customer);

            // assert
            Assert.IsTrue(customer.memberships.Contains("book"));
        }

        [TestMethod]
        public void TestShippingLines()
        {
            // arrange
            Customer customer = new Customer("Mr. Bryan Walton", "Bryan.Walton@gmail.com");
            Order order = new Order();
            order.addOrderLine("Book Harry Potter and the Goblet of Fire");            

            // act
            OrderService os = new OrderService();
            os.Process(order, customer);

            // assert
            Assert.AreEqual(customer.shipping_slips.Count, 1);
            Assert.IsTrue(customer.shipping_slips[0].Contains("Item:Book Harry Potter and the Goblet of Fire"));

            // arrange
            customer = new Customer("Mr. Bryan Waltonx", "Bryan.Waltonx@gmail.com");
            order = new Order();            
            order.addOrderLine("Video 'Transformers'");

            // act
            os = new OrderService();
            os.Process(order, customer);

            // assert
            Assert.AreEqual(customer.shipping_slips.Count, 1);
            Assert.IsTrue(customer.shipping_slips[0].Contains("Video 'Transformers'"));
        }

        [TestMethod]
        public void TestBookBalance()
        {
            // arrange
            Customer customer = new Customer("Mr. Bryan Walton", "Bryan.Walton@gmail.com");
            Order order = new Order();
            order.addOrderLine("Book Harry Potter and the Goblet of Fire");

            // act
            OrderService os = new OrderService();
            os.Process(order, customer);

            // assert
            Assert.AreEqual(customer.balance, 0);

            // arrange
            customer = new Customer("Mr. Bryan Walton", "Bryan.Walton@gmail.com");
            order = new Order();
            order.addOrderLine("Book Harry Potter and the Goblet of Fire");
            order.addOrderLine("Book Club Membership");

            // act
            os = new OrderService();
            os.Process(order, customer);

            // assert
            Assert.AreEqual(customer.balance, 5);

            // arrange
            customer = new Customer("Mr. Bryan Walton", "Bryan.Walton@gmail.com");
            order = new Order();
            order.addOrderLine("Book Harry Potter and the Goblet of Fire");
            order.addOrderLine("Book Club Membership");
            order.addOrderLine("Book Hidden Brains");
            order.addOrderLine("Book Club Membership");

            // act
            os = new OrderService();
            os.Process(order, customer);

            // assert
            Assert.AreEqual(customer.balance, 10);
        }

        [TestMethod]
        public void TestVideoBalance()
        {
            // arrange
            Customer customer = new Customer("Mr. Bryan Walton", "Bryan.Walton@gmail.com");
            Order order = new Order();
            order.addOrderLine("Video Transformers");

            // act
            OrderService os = new OrderService();
            os.Process(order, customer);

            // assert
            Assert.AreEqual(customer.balance, 0);

            // arrange
            customer = new Customer("Mr. Bryan Walton", "Bryan.Walton@gmail.com");
            order = new Order();
            order.addOrderLine("Video Transformers");
            order.addOrderLine("Video Club Membership");

            // act
            os = new OrderService();
            os.Process(order, customer);

            // assert
            Assert.AreEqual(customer.balance, 5);

            // arrange
            customer = new Customer("Mr. Bryan Walton", "Bryan.Walton@gmail.com");
            order = new Order();
            order.addOrderLine("Video Transformers");
            order.addOrderLine("Video Club Membership");
            order.addOrderLine("Video Transporter");
            order.addOrderLine("Video Club Membership");

            // act
            os = new OrderService();
            os.Process(order, customer);

            // assert
            Assert.AreEqual(customer.balance, 10);
        }
    }
}

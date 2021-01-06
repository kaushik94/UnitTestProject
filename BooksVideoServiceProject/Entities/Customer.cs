using System;
using System.Collections.Generic;

namespace BooksVideoServiceProject.Entities
{
    public class Customer : Entity
    {

        private readonly string customer_name;
        public double balance = 0;

        public List<String> memberships = new List<String>();
        public List<String> shipping_slips = new List<String>();

        public void AddMembership(string type) {
            balance = balance + 5;
            memberships.Add(type);
        }

        public void addShippingSlip(string slip) {
            shipping_slips.Add(slip);
        }

        public List<String> getShippingSlips() {
            return shipping_slips;
        }
        public List<String> GetMemberships() {
            return memberships;
        }

        private readonly string customer_email;

        private string customer_address;

        private string customer_shipping_address;

        private Customer() {}

        public Customer(string name, string email) {
            customer_name = name;
            customer_email = email;   
        }

        public string ShippingAddress 
        {
            get {
                return customer_shipping_address;
            }

            set {
                customer_shipping_address = value;
            }
        }

        public bool hasShippingAddress() {
            return customer_shipping_address != null && customer_shipping_address != "";
        }
    }
}

using System;

namespace MyLibrary
{
    public class LogCustomer : ICustomer
    {
        private readonly ICustomer customer;

        public LogCustomer(ICustomer customer)
        {
            this.customer = customer;
        }

        public void Contact()
        {
            Console.WriteLine($"== Call Contact Method Start");
            customer.Contact();
            Console.WriteLine($"== Call Contact Method Finish");
            Console.WriteLine();
        }
    }
}
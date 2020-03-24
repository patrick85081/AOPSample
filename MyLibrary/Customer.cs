using System;

namespace MyLibrary
{
    public class Customer : ICustomer
    {
        public void Contact()
        {
            Console.WriteLine("contact customer...");
        }
    }
}
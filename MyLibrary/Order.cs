using System;

namespace MyLibrary
{
    public class Order : IOrder
    {
        public bool Insert(string id, string name)
        {
            Console.WriteLine($"Order Insert Id: {id}, Name: {name}");
            return true;
        }

        public bool Update(string id, string name)
        {
            Console.WriteLine($"Order Update Id: {id}, Name: {name}");
            return true;
        }

        public bool Delete(string id)
        {
            Console.WriteLine($"Order Delete Id: {id}");
            return true;
        }
    }
}
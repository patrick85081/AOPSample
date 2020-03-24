using System;

namespace MyLibrary
{
    public class LogOrder : IOrder
    {
        private readonly IOrder order;

        public LogOrder(IOrder order)
        {
            this.order = order;
        }

        public bool Insert(string id, string name)
        {
            Console.WriteLine($"== Call Insert Method Start, Args id: {id}, name: {name}");
            var result = order.Insert(id, name);
            Console.WriteLine($"== Call Insert Method Finish, Return: {result}");
            Console.WriteLine();
            return result;
        }

        public bool Update(string id, string name)
        {
            Console.WriteLine($"== Call Update Method Start, Args id: {id}, name: {name}");
            var result = order.Update(id, name);
            Console.WriteLine($"== Call Update Method Finish, Return: {result}");
            Console.WriteLine();
            return result;
        }

        public bool Delete(string id)
        {
            Console.WriteLine($"== Call Delete Method Start, Args id: {id}");
            var result = order.Delete(id);
            Console.WriteLine($"== Call Delete Method Finish, Return: {result}");
            Console.WriteLine();
            return result;
        }
    }
}
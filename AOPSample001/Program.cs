using System.Collections.Concurrent;
using MyLibrary;

namespace AOPSample001
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = Factory.CreateOrder();
            order.Insert("001", "Patrick");

            order.Update("001", "Mark");

            order.Delete("001");
        }
    }

    public static class Factory
    {
        public static IOrder CreateOrder()
        {
            return new LogOrder(new Order());
        }
    }
}
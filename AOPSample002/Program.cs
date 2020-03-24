using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MyLibrary;

namespace Sample002
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = Factory.CreateOrder();
            order.Insert("001", "Patrick");
            order.Update("001", "Mark");
            order.Delete("001");

            var customer = Factory.CreateCustomer();
            customer.Contact();
        }
    }

    public static class CastleConfig
    {
        public static IWindsorContainer Container { get; private set; }

        static CastleConfig()
        {
            Container = new WindsorContainer();

            // 透過 Key 來決定取回的 IOrder 物件
            Container.Register(
                Component.For<IOrder>()
                    .ImplementedBy<Order>()
                    .LifestyleTransient());

            Container.Register(
                Component.For<IOrder>()
                    .Instance(new LogOrder(Container.Resolve<IOrder>()))
                    .Named("logOrder")
                    .LifestyleTransient());

            // 透過註冊順序，直接決定 LogCustomer Decorator 生成
            Container.Register(
                Component.For<ICustomer>()
                    .ImplementedBy<LogCustomer>()
                    .LifestyleTransient());
            
            Container.Register(
                Component.For<ICustomer>()
                    .ImplementedBy<Customer>()
                    .LifestyleTransient());
        }
    }

    public static class Factory
    {
        public static IOrder CreateOrder()
        {
            return CastleConfig.Container.Resolve<IOrder>("logOrder");
        }

        public static ICustomer CreateCustomer()
        {
            return CastleConfig.Container.Resolve<ICustomer>();
        }
    }
}
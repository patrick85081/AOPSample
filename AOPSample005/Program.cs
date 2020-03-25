using System;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MyLibrary;

namespace AOPSample005
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

    public static class Factory
    {
        public static IOrder CreateOrder()
        {
            var order = new ProxyGenerator().CreateInterfaceProxyWithTarget<IOrder>(
                new Order(), 
                new ProxyGenerationOptions(){Selector = new OnlyUpdateMethodBeSelected()}, 
                new LogInterceptor());
            return order;
        }

        public static ICustomer CreateCustomer()
        {
            var customer = new ProxyGenerator().CreateInterfaceProxyWithTarget<ICustomer>(
                new Customer(),
                new ProxyGenerationOptions(),
                new LogInterceptor());
            return customer;
        }
    }
    
    public class LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            var argsStrings = invocation.Method
                .GetParameters()
                .Zip(invocation.Arguments, (p, a) => $"{p.Name}: {a}")
                .ToArray();
            var argsString = argsStrings.Any() ? ", Arts " + string.Join(", ", argsStrings) : "";

            Console.WriteLine($"== Call {methodName} Method Start{argsString} ==");
            
            invocation.Proceed();
            var result = invocation.ReturnValue;
            
            Console.WriteLine($"== Call {methodName} Method Finish, Return: {result}");
            Console.WriteLine();
        }
    }
    
    public class OnlyUpdateMethodBeSelected : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if (method.Name.Contains("Update"))
            {
                return interceptors;
            }
            else
            {
                return Enumerable.Empty<IInterceptor>().ToArray();
            }
        }
    }
}
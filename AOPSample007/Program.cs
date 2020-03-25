using System;
using System.Reflection;
using MyLibrary;

namespace AOPSample007
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = Proxy.Create<IOrder>(new Order());
            order.Insert("001", "Patrick");
            order.Update("001", "Patrick");
            order.Delete("001");
        }
    }

    public class Proxy<T> : Proxy
    {
        private T decorated;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"===  Call {targetMethod.Name}({string.Join(", ", args)}) Start ===");
            
            var output = targetMethod.Invoke(decorated, args);

            if (targetMethod.ReturnType == typeof(void))
                Console.WriteLine($"===  Call {targetMethod.Name} Finish ===");
            else
                Console.WriteLine($"===  Call {targetMethod.Name} Finish, Return: {output} ===");
            return output;
        }

        public void SetInstance(T decorated)
        {
            this.decorated = decorated;
        }
    }

    public abstract class Proxy : DispatchProxy
    {
        public static T Create<T>(T decorated)
        {
            var proxy = DispatchProxy.Create<T, Proxy<T>>();
            (proxy as Proxy<T>).SetInstance(decorated);

            return proxy;
        }

        protected abstract override object Invoke(MethodInfo targetMethod, object[] args);
    }
}
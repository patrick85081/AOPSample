using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AOPSample009
{
    class Program
    {
        static void Main(string[] args)
        {
            var poxy1 = (targetInterface) ProxyGenerator.Create(typeof(targetInterface), new SampleProxy("coreproxy1"));
            poxy1.Write("here was invoked"); //---> "here was invoked by coreproxy1"

            var poxy2 = (targetInterface) ProxyGenerator.Create(typeof(targetInterface), typeof(SampleProxy),
                "coreproxy2");
            poxy2.Write("here was invoked"); //---> "here was invoked by coreproxy2"

            var poxy3 = ProxyGenerator.Create<targetInterface, SampleProxy>("coreproxy3");
            poxy3.Write("here was invoked"); //---> "here was invoked by coreproxy3"
        }
    }

    public class SampleProxy : IInterceptor
    {
        private string proxyName { get; }

        public SampleProxy(string name)
        {
            this.proxyName = name;
        }

        public object Intercept(MethodInfo method, object[] parameters)
        {
            Console.WriteLine(parameters[0] + " by " + proxyName);
            return null;
        }

//        public object Intercept(object target, MethodInfo method, object[] parameters)
//        {
//            Console.WriteLine(parameters[0] + " by " + proxyName);
//            return null;
//        }
    }

    public interface targetInterface
    {
        void Write(string writesome);
    }

    /// <summary>
    /// 拦截器接口
    /// </summary>
    public interface IInterceptor
    {
        /// <summary>
        /// 拦截器调用
        /// </summary>
//        /// <param name="target">代理实例</param>
        /// <param name="method">所拦截的方法</param>
        /// <param name="parameters">所拦截方法传入的参数值</param>
        /// <returns>返回值会传递给方法返回值</returns>    
        object Intercept(MethodInfo method, object[] parameters);
//        object Intercept(object target, MethodInfo method, object[] parameters);
    }

    public class ProxyGenerator : DispatchProxy
    {
        private IInterceptor interceptor { get; set; }

        /// <summary>
        /// 创建代理实例
        /// </summary>
        /// <param name="targetType">所要代理的接口类型</param>
        /// <param name="interceptor">拦截器</param>
        /// <returns>代理实例</returns>
        public static object Create(Type targetType, IInterceptor interceptor)
        {
            object proxy = GetProxy(targetType);
            ((ProxyGenerator) proxy).CreateInstance(interceptor);
            return proxy;
        }

        /// <summary>
        /// 创建代理实例
        /// </summary>
        /// <param name="targetType">所要代理的接口类型</param>
        /// <param name="interceptorType">拦截器类型</param>
        /// <param name="parameters">拦截器构造函数参数值</param>
        /// <returns>代理实例</returns>
        public static object Create(Type targetType, Type interceptorType, params object[] parameters)
        {
            object proxy = GetProxy(targetType);
            ((ProxyGenerator) proxy).CreateInstance(interceptorType, parameters);
            return proxy;
        }


        /// <summary>
        /// 创建代理实例 TTarget:所要代理的接口类型 TInterceptor:拦截器类型
        /// </summary>
        /// <param name="parameters">拦截器构造函数参数值</param>
        /// <returns>代理实例</returns>
        public static TTarget Create<TTarget, TInterceptor>(params object[] parameters)
            where TInterceptor : IInterceptor
        {
            var proxy = GetProxy(typeof(TTarget));
            ((ProxyGenerator) proxy).CreateInstance(typeof(TInterceptor), parameters);
            return (TTarget) proxy;
        }

        private static object GetProxy(Type targetType)
        {
            var callexp = Expression.Call(typeof(DispatchProxy), nameof(DispatchProxy.Create),
                new[] {targetType, typeof(ProxyGenerator)});
            return Expression.Lambda<Func<object>>(callexp).Compile()();
        }

        private void CreateInstance(Type interceptorType, object[] parameters)
        {
            var ctorParams = parameters.Select(x => x.GetType()).ToArray();
            var paramsExp = parameters.Select(x => Expression.Constant(x));
            var newExp = Expression.New(interceptorType.GetConstructor(ctorParams), paramsExp);
            this.interceptor = Expression.Lambda<Func<IInterceptor>>(newExp).Compile()();
        }

        private void CreateInstance(IInterceptor interceptor)
        {
            this.interceptor = interceptor;
        }

        protected override object Invoke(MethodInfo method, object[] parameters)
        {
            return this.interceptor.Intercept(method, parameters);
        }
    }
}
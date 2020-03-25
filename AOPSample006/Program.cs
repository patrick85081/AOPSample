using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace AOPSample006
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var aopClass = new AOPClass("Patrick", 2);
            aopClass.Hello();

            var subAopClass = new SubAOPClass("Mark");
            subAopClass.Pro = "Test";
            subAopClass.Output("Hi");
            subAopClass.ShowMsg();
        }
    }

    [AOP]
    public class AOPClass : ContextBoundObject
    {
        public AOPClass(string name, int age)
        {
            
        }
        public string Hello()
        {
            return "Welcome";
        }
    }

    [AOP]
    public class SubAOPClass :ContextBoundObject
    {
        public string Pro = null;
        private string Msg = null;
 
        public SubAOPClass(string msg)
        {
            Msg = msg;
        }
 
        public void Output(string name)
        {
            Console.WriteLine(name + ",你好！-->P:" + Pro);
        }
 
        public void ShowMsg()
        {
            Console.WriteLine($"构造函數傳的Msg參數內容是：{Msg}");
        } 
    }

    public class AOPAttribute : ProxyAttribute
    {
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            var aopProxy = new AOPProxy(serverType);
            return aopProxy.GetTransparentProxy() as MarshalByRefObject;
        }
    }
    
    public class AOPProxy : RealProxy
    {
        public AOPProxy(Type serverType) :base(serverType)
        {
        }

        public override IMessage Invoke(IMessage msg)
        {
            if (msg is IConstructionCallMessage)
            {
                var constructCallMessage = msg as IConstructionCallMessage;
                var argMessage = BuildArgMessageString(constructCallMessage, constructCallMessage.Args);
                Console.WriteLine($"=== Call {constructCallMessage.ActivationType.Name} Constructor Start{argMessage} ===");
                
                var constructReturnMessage = this.InitializeServerObject(constructCallMessage);
                RealProxy.SetStubData(this, constructReturnMessage.ReturnValue);
                
                
                Console.WriteLine($"=== Call {constructCallMessage.ActivationType.Name} Constructor Finish ===");
                return constructReturnMessage;
            }

            var callMessage = msg as IMethodCallMessage;

            ReturnMessage message;
            try
            {
                var args = callMessage.Args;
                var argMessage = BuildArgMessageString(callMessage, args);

                Console.WriteLine($"=== Call {callMessage.MethodName} Start{argMessage} ===");

                var output = callMessage.MethodBase.Invoke(GetUnwrappedServer(), args);

                var returnMessage = BuildReturnMessageString(callMessage, output);
                Console.WriteLine($"=== Call {callMessage.MethodName} Finish{returnMessage} ===");

                message = new ReturnMessage(output, args, args.Length, callMessage.LogicalCallContext, callMessage);
            }
            catch (Exception e)
            {
                message = new ReturnMessage(e, callMessage);
            }

            return message;
        }

        private static string BuildArgMessageString(IMethodCallMessage callMessage, object[] args)
        {
            var argStrings = callMessage.MethodBase
                .GetParameters()
                .Zip(args, (p, a) => $"{p.Name}: {a}")
                .ToArray();
            var argMessage = args.Any() ? ", Args (" + string.Join(", ", argStrings) + ")" : "";
            return argMessage;
        }

        private static string BuildReturnMessageString(IMethodCallMessage callMessage, object output)
        {
            var returnType = callMessage.MethodBase
                .GetType()
                .GetProperty("ReturnType")
                .GetValue(callMessage.MethodBase) as Type;
            var returnMessage = returnType == typeof(void) ? "" : $", Return: {output ?? "null"}";
            return returnMessage;
        }
    }
}
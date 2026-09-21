using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAtributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList ();
            var methodAtributes = type.GetMethod(method.Name).GetCustomAttribute<MethodInterceptionBaseAttribute>(true);
            classAtributes.AddRange (methodAtributes);

            return classAtributes.OrderBy (x => x.Priority).ToArray ();
        }
    }
}

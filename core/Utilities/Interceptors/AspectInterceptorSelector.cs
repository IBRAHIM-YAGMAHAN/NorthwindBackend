using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes(true)
                .OfType<MethodInterceptionBaseAttribute>();
            var methodAttributes = method.GetCustomAttributes(true)
                .OfType<MethodInterceptionBaseAttribute>();

            var attributes = classAttributes.Union(methodAttributes)
                .OrderBy(x => x.Priority).ToArray();

            return attributes;
        }
    }
}
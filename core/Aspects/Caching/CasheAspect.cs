using core.CrossCuttingConcerns.Cashing;
using core.Utilities.Interceptors;
using core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;

namespace core.Aspects.Caching
{
    public class CasheAspect : MethodInterception
    {
        public int _duration;
        public ICacheManager _cacheManager;
        public int duration;

        public CasheAspect (int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?.ToString() ?? "<Null>"))})";

            if(_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }

            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}

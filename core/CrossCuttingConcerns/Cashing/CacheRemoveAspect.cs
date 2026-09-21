using Castle.DynamicProxy;
using core.Utilities.Interceptors;
using core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace core.CrossCuttingConcerns.Cashing
{
    public class CacheRemoveAspect: MethodInterception
    {
        public string _pattren;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattren = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattren);
        }
    }
}

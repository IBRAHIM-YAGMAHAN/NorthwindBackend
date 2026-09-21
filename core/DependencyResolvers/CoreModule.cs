using core.CrossCuttingConcerns.Cashing;
using core.CrossCuttingConcerns.Cashing.Microsoft;
using core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCasheManager>();
        }

        
    }
}

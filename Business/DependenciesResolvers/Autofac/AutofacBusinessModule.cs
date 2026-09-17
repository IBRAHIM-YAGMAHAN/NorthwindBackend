using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Business.DependenciesResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        override protected void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PrductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
        }
    }
}

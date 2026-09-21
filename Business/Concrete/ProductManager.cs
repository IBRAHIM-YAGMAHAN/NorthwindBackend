using Business.Abstract;
using Business.Constants;
using Business.ValidationRules;
using core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.Concrete
{
    public class PrductManager : IProductService
    {
        private IProductDal _productDal;
        public PrductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAsspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            // Business code can be added here, such as validation or other logic before adding the product
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }   
        

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
           return new SuccessDataResult<List<Product>>(_productDal.GetList(filter: p => p.CategoryId == categoryId).ToList());
        }

        public IResult Update(Product product)
        {
           _productDal.Update(product);
           return new SuccessResult(Messages.ProductUpdated );  
        }
    }
}

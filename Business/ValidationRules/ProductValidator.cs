using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p=> p.ProductName).Length(2,30);
            RuleFor(p => p.Unitprice).GreaterThanOrEqualTo(1);
            RuleFor( p => p.Unitprice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartWithA);
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
 
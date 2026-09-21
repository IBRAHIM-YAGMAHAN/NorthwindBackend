using Castle.Core.Interceptor;
using core.CrossCuttingConcerns.Validation;
using core.Utilities.Interceptors;
using core.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if(!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }
        }
        _validatorType = ValidationType;
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach( var entity in entities)
            {
                ValidationTool.Validate(validator, entity)  ;
            }
        }
    }
}

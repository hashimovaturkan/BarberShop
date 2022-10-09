using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BarberShop.WebApi.Validators
{
    public static class ApiValidators
    {
        public static IMvcBuilder AddFluentValidationCustom(this IMvcBuilder builder)
        {
            builder
                .AddFluentValidation(fv =>
                {
                    fv.ImplicitlyValidateChildProperties = true;
                    fv.ImplicitlyValidateRootCollectionElements = true;

                    fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

            return builder;
        }
    }
}

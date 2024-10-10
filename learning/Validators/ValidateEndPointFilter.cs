using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace learning.Filters
{
    public class ValidateModelFilter<T> : IAsyncActionFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidateModelFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var model = context.ActionArguments.Values.OfType<T>().FirstOrDefault();

            if (model != null)
            {
                var validation = await _validator.ValidateAsync(model);

                if (!validation.IsValid)
                {
                    context.Result = new BadRequestObjectResult(validation.ToDictionary());
                    return;
                }
            }

            await next();
        }
    }
}
using FluentValidation;
using learning.Models;

namespace learning.Validators
{
    public class TimeBillModelValidator : AbstractValidator<TimeBillModel>
    {
        public TimeBillModelValidator() {
            RuleFor(x => x.EmployeeId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.CustomerId).NotEmpty ().GreaterThan(0);
            RuleFor(x => x.HoursWorked).InclusiveBetween(.1,12.0);
            RuleFor(x => x.Rate).InclusiveBetween(50,300);
            RuleFor(x => x.Work).NotEmpty().MinimumLength(5);
        }
    }
}
 
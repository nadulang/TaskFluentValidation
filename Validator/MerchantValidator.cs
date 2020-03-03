using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TaskMediatrFluentValidation.Entity;

namespace TaskMediatrFluentValidation.Validator
{
    public class MerchantValidator : AbstractValidator<Merchant>
    {
        public MerchantValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("name can't be empty");
            RuleFor(x => x.address).NotEmpty().WithMessage("max name_on_card length is 50");
            RuleFor(x => x.rating).ExclusiveBetween(1, 5).WithMessage("exp_month is bettween 1-5");
        }
    }
}
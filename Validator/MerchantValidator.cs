using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TaskMediatrFluentValidation.Controllers;

namespace TaskMediatrFluentValidation.Validator
{
    public class MerchantValidator : AbstractValidator<Merch>
    {
        public MerchantValidator()
        {
            RuleFor(x => x.data.attributes.name).NotEmpty().WithMessage("name can't be empty");
            RuleFor(x => x.data.attributes.address).NotEmpty().WithMessage("address can't be empty");
            RuleFor(x => x.data.attributes.rating).ExclusiveBetween(1, 5).WithMessage("rating is bettween 1-5");
        }
    }
}
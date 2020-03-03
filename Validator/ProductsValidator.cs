using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TaskMediatrFluentValidation.Entity;


namespace TaskMediatrFluentValidation.Validator
{
    public class ProductsValidator : AbstractValidator<Products>
    {
        public ProductsValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("name can't be empty");
            RuleFor(x => x.name).MaximumLength(50).WithMessage("max username length is 50");
            RuleFor(x => x.price).NotEmpty().WithMessage("price can't be empty");
            RuleFor(x => x.price).GreaterThan(1000).WithMessage("price must be greater than 1000");
        }
    }
}
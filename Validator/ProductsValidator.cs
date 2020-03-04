using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TaskMediatrFluentValidation.Controllers;


namespace TaskMediatrFluentValidation.Validator
{
    public class ProductsValidator : AbstractValidator<Products1>
    {
        public ProductsValidator()
        {
            RuleFor(x => x.data.attributes.name).NotEmpty().WithMessage("name can't be empty");
            RuleFor(x => x.data.attributes.name).MaximumLength(50).WithMessage("max username length is 50");
            RuleFor(x => x.data.attributes.price).NotEmpty().WithMessage("price can't be empty");
            RuleFor(x => x.data.attributes.price).GreaterThan(1000).WithMessage("price must be greater than 1000");
        }
    }
}
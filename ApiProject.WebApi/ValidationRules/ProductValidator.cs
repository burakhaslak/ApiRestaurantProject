using ApiProject.WebApi.Entities;
using FluentValidation;

namespace ApiProject.WebApi.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name cannot be empty.").MinimumLength(2).WithMessage("Product name must be at least 2 characters.").MaximumLength(50).WithMessage("Product name must be at most 50 characters.");

            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("Product description cannot be empty.")
                .MinimumLength(10).WithMessage("Product description must be at least 10 characters.")
                .MaximumLength(500).WithMessage("Product description must be at most 500 characters.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price cannot be empty.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .LessThan(1000).WithMessage("Price must be less than 1000.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL cannot be empty.");
               
        }
    }
}

using FluentValidation;

namespace App.Services.Products.Update
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(m => m.Name)
              .NotEmpty()
              .Length(3, 10);
            //.Must(MustUniqueProductName).WithMessage("aynı isimde urun var");


            RuleFor(m => m.Price)
                .GreaterThan(0);

            RuleFor(m => m.Stock)
             .InclusiveBetween(1, 100);
        }
    }
}

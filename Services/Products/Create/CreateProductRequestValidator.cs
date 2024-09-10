using App.Repositories.Products;
using FluentValidation;

namespace App.Services.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _repository;
        public CreateProductRequestValidator(IProductRepository repository)
        {

            RuleFor(m => m.Name)
                .NotEmpty()
                .Length(3, 10);
            //.Must(MustUniqueProductName).WithMessage("aynı isimde urun var");


            RuleFor(m => m.Price)
                .GreaterThan(0);

            RuleFor(m => m.Stock)
             .InclusiveBetween(1, 100);
            _repository = repository;
        }

        //private bool MustUniqueProductName(string productName)
        //{
        //    return !_repository.Where(m=>m.Name==productName).Any();    
        //}
    }
}

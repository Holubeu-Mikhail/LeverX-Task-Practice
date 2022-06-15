using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FluentValidation;
using System.Linq;

namespace BusinessLogicLayer.Validators
{
    public class ProductValidator : Validator<Product>
    {
        public ProductValidator(IRepository<Product> repository) : base(repository)
        {
            _repository = repository;

            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.TypeId).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();

            RuleSet("BeforeCreating", () => { RuleFor(x => x).Must(x => IsNameUnique(x)); });

            RuleSet("BeforeUpdating", () =>
            {
                RuleFor(x => x).Must(x => IsNameUnique(x));
                RuleFor(x => x).Must(x => IsEntityExists(x));
            });
        }

        private bool IsNameUnique(Product product)
        {
            var result = _repository.GetAll().All(x => x.Name != product.Name);
            return result;
        }
    }
}

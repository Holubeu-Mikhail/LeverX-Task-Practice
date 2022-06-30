using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FluentValidation;
using System.Linq;

namespace BusinessLogicLayer.Validators
{
    internal class ProductTypeValidator : Validator<ProductType>
    {
        public ProductTypeValidator(IRepository<ProductType> repository) : base(repository)
        {
            _repository = repository;

            RuleFor(x => x.Name).NotNull().NotEmpty();

            RuleSet("BeforeCreating", () =>
            {
                RuleFor(x => x).Must(x => IsNameUnique(x));
            });

            RuleSet("BeforeUpdating", () =>
            {
                RuleFor(x => x).Must(x => IsNameUnique(x));
                RuleFor(x => x).Must(x => IsEntityExists(x));
            });
        }

        private bool IsNameUnique(ProductType type)
        {
            var result = _repository.GetAll().All(x => x.Name != type.Name);
            return result;
        }
    }

}

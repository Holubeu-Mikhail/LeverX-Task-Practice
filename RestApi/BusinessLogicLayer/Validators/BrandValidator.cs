using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FluentValidation;

namespace BusinessLogicLayer.Validators
{
    internal class BrandValidator : Validator<Brand>
    {
        public BrandValidator(IRepository<Brand> repository) : base(repository)
        {
            _repository = repository;

            RuleFor(x => x.TownId).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Description).NotNull().NotEmpty();
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

        private bool IsNameUnique(Brand brand)
        {
            var result = _repository.GetAll().Where(x => x.TownId == brand.TownId).All(x => x.Name != brand.Name);
            return result;
        }
    }
}

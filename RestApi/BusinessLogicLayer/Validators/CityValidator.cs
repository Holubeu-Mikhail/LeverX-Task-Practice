using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FluentValidation;
using System.Linq;

namespace BusinessLogicLayer.Validators
{
    internal class CityValidator : Validator<City>
    {
        public CityValidator(IRepository<City> repository) : base(repository)
        {
            _repository = repository;

            RuleFor(x => x.Code).GreaterThan(0);
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
        private bool IsNameUnique(City city)
        {
            var result = _repository.GetAll().All(x => x.Name != city.Name);
            return result;
        }
    }
}

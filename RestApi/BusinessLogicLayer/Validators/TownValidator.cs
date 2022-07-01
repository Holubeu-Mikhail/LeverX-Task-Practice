using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FluentValidation;
using System.Linq;

namespace BusinessLogicLayer.Validators
{
    internal class TownValidator : Validator<Town>
    {
        public TownValidator(IRepository<Town> repository) : base(repository)
        {
            _repository = repository;

            RuleFor(x => x.Code);
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
        private bool IsNameUnique(Town town)
        {
            var result = _repository.GetAll().All(x => x.Name != town.Name);
            return result;
        }
    }
}

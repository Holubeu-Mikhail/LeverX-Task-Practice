using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FluentValidation;

namespace BusinessLogicLayer.Validators
{
    public class Validator<T> : AbstractValidator<T> where T : BaseModel
    {
        protected IRepository<T> _repository;

        public Validator(IRepository<T> repository)
        {
            _repository = repository;
        }

        protected bool IsEntityExists(T entity)
        {
            var result = _repository.Get(entity.Id);
            if (result == null)
                return false;
            return true;
        }
    }

}

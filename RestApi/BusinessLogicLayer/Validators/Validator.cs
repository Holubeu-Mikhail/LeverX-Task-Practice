using DataAccessLayer.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

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

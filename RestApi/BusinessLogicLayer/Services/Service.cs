using BusinessLogicLayer.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services
{
    public class Service<T> : IService<T> where T : BaseModel
    {
        private readonly IRepository<T> _repository;
        private readonly IValidator<T> _validator;

        public Service(IRepository<T> repository, IValidator<T> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public List<T> GetAll()
        {
            var entities = _repository.GetAll();
            return entities.ToList();
        }

        public T Get(int id)
        {
            var entity = _repository.Get(id);
            return entity;
        }

        public void Create(T entity)
        {
            _validator.ValidateAndThrow(entity);
            if (_validator.Validate(entity, options => options.IncludeRuleSets("BeforeCreating")).IsValid)
                _repository.Create(entity);
        }

        public void Update(T entity)
        {
            _validator.ValidateAndThrow(entity);
            if (_validator.Validate(entity, options => options.IncludeRuleSets("BeforeUpdating")).IsValid)
                _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}

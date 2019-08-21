using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Data.Converters;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IRepository<Person> repository)
        {
            this._repository = repository;
            this._converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            var personVO = _repository.Create(personEntity);
            return _converter.Parse(personVO);
        }

        public void Delete(long id)
        {
            this._repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(this._repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            var personEntity = this._repository.FindById(id);
            return  _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            return  _converter.Parse(this._repository.Update(personEntity));
        }

    }
}

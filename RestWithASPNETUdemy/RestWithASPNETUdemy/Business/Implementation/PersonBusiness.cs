using Microsoft.AspNetCore.Mvc;
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

        public PersonBusiness(IRepository<Person> repository)
        {
            this._repository = repository;
        }

        public Person Create(Person person)
        {
            return this._repository.Create(person);
        }

        public void Delete(long id)
        {
            this._repository.Delete(id);
        }

        public List<Person> FindAll()
        {
            return this._repository.FindAll();
        }

        public Person FindById(long id)
        {
            return this._repository.FindById(id);
        }

        public Person Update(Person person)
        {

            return this._repository.Update(person);
        }

    }
}

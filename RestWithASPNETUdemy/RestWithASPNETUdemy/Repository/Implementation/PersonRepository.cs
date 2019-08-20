using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MySQLContext _context;

        public PersonRepository(MySQLContext context)
        {
            this._context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                this._context.Persons.Add(person);
                this._context.SaveChanges();
                return person;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long id)
        {
            var result = this._context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            try
            {
                if (result != null)
                {
                    this._context.Remove(result);
                    this._context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Person> FindAll()
        {
            return this._context.Persons.ToList();
        }
        public Person FindById(long id)
        {
            return this._context.Persons.SingleOrDefault(x => x.Id.Equals(id));
        }

        public Person Update(Person person)
        {

            if (!Exists(person.Id)) return new Person();

            //var result = this._context.Persons.SingleOrDefault(p => person.Id.Equals(person.Id));

            try
            {
                this._context.Entry<Person>(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this._context.SaveChanges();
                return person;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return this._context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}

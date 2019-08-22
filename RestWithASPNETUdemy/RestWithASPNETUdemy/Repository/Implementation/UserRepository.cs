using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            this._context = context;
        }

        public User Create(User user)
        {
            try
            {
                this._context.Users.Add(user);
                this._context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long id)
        {
            var result = this._context.Users.SingleOrDefault(p => p.Id.Equals(id));

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

        public List<User> FindAll()
        {
            return this._context.Users.ToList();
        }
        public User FindById(long id)
        {
            return this._context.Users.SingleOrDefault(x => x.Id.Equals(id));
        }

        public User Update(User user)
        {

            if (!Exists(user.Id)) return new User();

            //var result = this._context.Persons.SingleOrDefault(p => person.Id.Equals(person.Id));

            try
            {
                this._context.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this._context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return this._context.Users.Any(p => p.Id.Equals(id));
        }

        public User FindByLogin(string login)
        {
            return this._context.Users.SingleOrDefault(x => x.Login.Equals(login));
        }
    }
}

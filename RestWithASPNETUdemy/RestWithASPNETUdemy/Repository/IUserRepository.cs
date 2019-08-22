using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Repository
{
    public interface IUserRepository
    {
        User Create(User person);
        User FindById(long id);
        User FindByLogin(string login);
        List<User> FindAll();
        User Update(User person);
        void Delete(long id);
        bool Exists(long? id);

    }
}

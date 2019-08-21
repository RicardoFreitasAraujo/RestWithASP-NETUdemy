using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class BookBusiness : IBookBusiness
    {

        private readonly IRepository<Book> _repository;

        public BookBusiness(IRepository<Book> repository)
        {
            this._repository = repository;
        }

        public Book Create(Book book)
        {
            return this._repository.Create(book);
        }

        public void Delete(long id)
        {
            this._repository.Delete(id);
        }

        public List<Book> FindAll()
        {
            return this._repository.FindAll();
        }

        public Book FindById(long id)
        {
            return this._repository.FindById(id);
        }

        public Book Update(Book book)
        {
            return this._repository.Update(book);
        }

    }
}

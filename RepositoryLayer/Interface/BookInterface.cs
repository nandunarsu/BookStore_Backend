using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface BookInterface
    {
      public Task<bool> addBook(Book book);
      public  Task<IEnumerable<Book>> getAllBook();
      public  Task<Book> getBookById(int bId);
    }
}

using BussinessLayer.Interface;
using ModelLayer.Cart;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class BookServicebl : BookInterfacebl
    {
        private readonly BookInterface _bookbl;
        
        public BookServicebl(BookInterface bookbl)
        {
            _bookbl = bookbl;
        }

        public Task<bool> addBook(Book book)
        {
            return _bookbl.addBook(book);
        }
        public Task<IEnumerable<Book>> getAllBook()
        {
            return _bookbl.getAllBook();
        }
        public Task<Book> getBookById(int bId)
        {
            return _bookbl.getBookById(bId);
        }
    }
}

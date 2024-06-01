using Dapper;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class BookService : BookInterface
    {
        private readonly DapperContext Context;
        public BookService(DapperContext context)
        {
            Context = context;

        }
        public async Task<bool> addBook(Book book)
        {
            string insertQuery = @"INSERT INTO Books (BookName, BookImage, Description, AuthorName, Quantity, Price) 
                                       VALUES (@BookName, @BookImage, @Description, @AuthorName, @Quantity, @Price)";
            using (var connection = Context.CreateConnection())
            {
                var data = await connection.ExecuteAsync(insertQuery, book);
                return data > 0;
            }
        }
        public  async Task<IEnumerable<Book>> getAllBook()
        {
            string query = "SELECT * FROM Books";
            using (var connection = Context.CreateConnection())
            {
                var notes = await connection.QueryAsync<Book>(query);
                return notes.Reverse().ToList();
                
            }
        }
        public async Task<Book> getBookById(int bId)
        {
            string query = "SELECT * FROM Books WHERE BookId=@bId";
            using (var connection = Context.CreateConnection())
            {
                
                 var book = connection.Query<Book>(query, new { bId = bId }).FirstOrDefault();
                return book;

            }
        }
    }
}

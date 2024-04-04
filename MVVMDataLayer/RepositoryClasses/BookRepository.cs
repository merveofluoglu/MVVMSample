using MVVMDataLayer.Context;
using MVVMDataLayer.RepositoryInterfaces;
using MVVMEntityLayer;

namespace MVVMDataLayer.RepositoryClasses
{
    public class BookRepository : IBookRepository
    {
        private BookDbContext _dbContext;

        public BookRepository(BookDbContext _db) 
        {
            _dbContext = _db;
        }

        public Book Add(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return book;
        }

        public Book Update(Book book)
        {
            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
            return book;
        }

        public bool Delete(long id)
        {
            _dbContext.Books.Remove(_dbContext.Books.Find(id));
            _dbContext.SaveChanges();
            return true;
        }

        public Book CreateEmpty()
        {
            return new Book
            {
                Id = 0,
                Name = string.Empty,
                Author = string.Empty,
                Cost = 0,
                DateOfIssue = null,
                SellStartDate = DateTime.Now,
                SellEndDate = null,
                PaperType = string.Empty,
                IsHardcover = false,
            };
        }

        public List<Book> Get()
        {
            return _dbContext.Books.ToList();
        }

        public Book GetBook(long _id)
        {
            return _dbContext.Books.FirstOrDefault(p => p.Id == _id);
        }

        public List<Book> Search(BookSearch entity)
        {
            List<Book> _result = _dbContext.Books.Where(p => (
                entity.Name == null || p.Name.StartsWith(entity.Name)
                && p.Cost >= entity.ListPrice
            )).ToList();

            return _result;
        }
    }
}

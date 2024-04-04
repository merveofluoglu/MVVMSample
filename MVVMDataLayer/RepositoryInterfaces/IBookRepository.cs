using MVVMEntityLayer;

namespace MVVMDataLayer.RepositoryInterfaces
{
    public interface IBookRepository
    {
        List<Book> Get();
        Book GetBook(long id);
        Book CreateEmpty();
        Book Add(Book book);
        Book Update(Book book);
        bool Delete(long id);
        List<Book> Search(BookSearch entity);
    }
}

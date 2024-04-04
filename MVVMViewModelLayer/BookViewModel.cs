using MVVMDataLayer.RepositoryInterfaces;
using MVVMEntityLayer;
using MVVMViewModelLayer.BaseClasses;
using CommonLibrary.PagerClasses;

namespace MVVMViewModelLayer
{
    public class BookViewModel : ViewModelBase
    {
        public IBookRepository Repository { get; set; }
        public List<Book> Books { get; set; }
        public List<Book> AllBooks { get; set; }
        public BookSearch SearchEntity { get; set; }
        public int TotalBooks { get; set; }
        public Book SelectedBook { get; set; }

        public BookViewModel()
        {
            SearchEntity = new BookSearch();
        }

        public BookViewModel(IBookRepository repository)
        {
            Repository = repository;
            SearchEntity = new BookSearch();
        }

        public override void HandleRequest()
        {
            switch (EventCommand.ToLower())
            {
                case "add":
                    CreateEmptyBook(); 
                    IsDetailVisible = true;
                    break;
                case "delete":
                    if(DeleteBook(Convert.ToInt64(EventArgument)))
                    {
                        AllBooks = null;
                        SortBooks();
                        PageBooks();
                    }
                    break;
                case "search":
                    Pager.PageIndex = 0;
                    SortBooks();
                    PageBooks();
                    break;
                case "sort":
                case "page":
                    SortBooks();
                    PageBooks();
                    break;
                case "edit":
                    LoadBook(Convert.ToInt64(EventArgument));
                    IsDetailVisible = (SelectedBook != null);
                    break;
                case "cancel":
                    SortBooks();
                    PageBooks();
                    IsDetailVisible = false;
                    break;
                case "save":
                    if(Save())
                    {
                        AllBooks = null;
                        SortBooks();
                        PageBooks();
                        IsDetailVisible = false;
                    }
                    break;
                default:
                    LoadBooks();
                    break;
            }
        }

        public virtual bool Save()
        {
            if(!SelectedBook.Id.Equals(0))
            {
                Repository.Update(SelectedBook);
            }
            else
            {
                Repository.Add(SelectedBook);
            }
            return true;
        }

        public virtual bool DeleteBook(long id)
        {
            EventArgument = string.Empty;

            return Repository.Delete(id);
        }
        public virtual void CreateEmptyBook()
        {
            SelectedBook = Repository.CreateEmpty();
        }

        public virtual void PageBooks()
        {
            TotalBooks = Books.Count;
            base.SetPagerObject(TotalBooks);
            Books = Books.Skip(base.Pager.PageIndex * base.Pager.PageSize).Take(base.Pager.PageSize).ToList();
        }

        protected virtual void LoadBook(long id)
        {
            if (Repository == null)
            {
                throw new ApplicationException("Repository is null.");
            }
            else
            {
                SelectedBook = Repository.GetBook(id);
            }
        }

        protected virtual void LoadBooks()
        {
            if (Repository == null)
            {
                throw new ApplicationException("Repository is null.");
            }
            else
            {
                Books = Repository.Get().OrderBy(p => p.Name).ToList();
            }
        }

        public virtual void SearchBooks()
        {
            if (AllBooks == null)
            {
                if (Repository == null)
                {
                    throw new ApplicationException("Repository is null");
                }
                else
                {
                    AllBooks = Repository.Search(SearchEntity).OrderBy(p => p.Name).ToList();
                }
            }
            Books = AllBooks.Where(p => (SearchEntity.Name == null || p.Name.StartsWith(SearchEntity.Name))
                                    && p.Cost >= SearchEntity.ListPrice
                                    && (SearchEntity.Author == null || p.Author.StartsWith(SearchEntity.Author)))
                            .ToList();
        }

        protected virtual void SortBooks()
        {
            SearchBooks();

            if (EventCommand == "sort")
            {
                SetSortDirection();
            }

            bool isAscending = SortDirection == "asc";

            switch (SortExpression.ToLower())
            {
                case "name":
                    if (isAscending)
                    {
                        Books = Books.OrderBy(p => p.Name).ToList();
                    }
                    else
                    {
                        Books = Books.OrderByDescending(p => p.Name).ToList();
                    }
                    break;
                case "author":
                    if (isAscending)
                    {
                        Books = Books.OrderBy(p => p.Author).ToList();
                    }
                    else
                    {
                        Books = Books.OrderByDescending(p => p.Author).ToList();
                    }
                    break;
                case "cost":
                    if (isAscending)
                    {
                        Books = Books.OrderBy(p => p.Cost).ToList();
                    }
                    else
                    {
                        Books = Books.OrderByDescending(p => p.Cost).ToList();
                    }
                    break;
                case "dateofissue":
                    if (isAscending)
                    {
                        Books = Books.OrderBy(p => p.DateOfIssue).ToList();
                    }
                    else
                    {
                        Books = Books.OrderByDescending(p => p.DateOfIssue).ToList();
                    }
                    break;
                case "sellstartdate":
                    if (isAscending)
                    {
                        Books = Books.OrderBy(p => p.SellStartDate).ToList();
                    }
                    else
                    {
                        Books = Books.OrderByDescending(p => p.SellStartDate).ToList();
                    }
                    break;
                case "sellenddate":
                    if (isAscending)
                    {
                        Books = Books.OrderBy(p => p.SellEndDate).ToList();
                    }
                    else
                    {
                        Books = Books.OrderByDescending(p => p.SellEndDate).ToList();
                    }
                    break;
                case "papertype":
                    if (isAscending)
                    {
                        Books = Books.OrderBy(p => p.PaperType).ToList();
                    }
                    else
                    {
                        Books = Books.OrderByDescending(p => p.PaperType).ToList();
                    }
                    break;
                case "ishardcover":
                    if (isAscending)
                    {
                        Books = Books.OrderBy(p => p.IsHardcover).ToList();
                    }
                    else
                    {
                        Books = Books.OrderByDescending(p => p.IsHardcover).ToList();
                    }
                    break;
            }
        }
    }
}

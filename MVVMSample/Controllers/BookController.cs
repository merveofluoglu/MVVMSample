using Microsoft.AspNetCore.Mvc;
using MVVMDataLayer.RepositoryInterfaces;
using MVVMEntityLayer;
using MVVMViewModelLayer;
using Newtonsoft.Json;

namespace MVVMSample.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly BookViewModel _bookViewModel;

        public BookController(IBookRepository bookRepository, BookViewModel bookViewModel)
        {
            _bookRepository = bookRepository;
            _bookViewModel = bookViewModel;
        }

        public IActionResult Books()
        {
            _bookViewModel.SortExpression = "Name";
            _bookViewModel.EventCommand = "sort";
            _bookViewModel.Pager.PageSize = 5;
            _bookViewModel.HandleRequest();
            HttpContext.Session.SetString("Books", JsonConvert.SerializeObject(_bookViewModel.AllBooks));

            return View(_bookViewModel);
        }

        [HttpPost]
        public IActionResult Books(BookViewModel _vm)
        {
            _vm.Repository = _bookRepository;
            _vm.Pager.PageSize = 5;
            _vm.AllBooks = JsonConvert.DeserializeObject<List<Book>>(HttpContext.Session.GetString("Books"));
            _vm.HandleRequest();
            ModelState.Clear();

            return View(_vm);
        }
    }
}

using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var books = BookService.Instance.GetBooks();
            return View(books);
        }

        public IActionResult AddModal()
        {
            return PartialView("_AddBookPartial");
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("AddModal", vm); // Re-render the modal with validation errors
            }

            BookService.Instance.AddBook(vm);

            return RedirectToAction("Index");
        }


        public IActionResult EditModal(Guid id)
        {
            var editBookViewModel = BookService.Instance.GetBookById(id);
            if (editBookViewModel == null)
                return NotFound();

            return PartialView("_EditBookPartial", editBookViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookService.Instance.UpdateBook(vm);
            return Ok();
        }

        // ✅ UPDATED DeleteModal and Delete
        public IActionResult DeleteModal(Guid id)
        {
            var book = BookService.Instance.GetBookById(id);
            if (book == null)
                return NotFound();

            return PartialView("_DeleteBookPartial", book); // ✅ updated partial name
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var book = BookService.Instance.GetBookById(id);
            if (book == null)
                return NotFound();

            BookService.Instance.DeleteBook(id);
            return Ok(); // You can return a redirect if not using AJAX
        }

        public IActionResult Details(Guid id)
        {
            var book = BookService.Instance.GetBooks(includeArchived: true).FirstOrDefault(b => b.BookId == id);
            if (book == null)
                return NotFound();

            ViewBag.BookCopies = BookService.Instance.GetBookCopies(id);
            return View(book);
        }
        private readonly BookService _bookService = BookService.Instance;
        // GET: Show the Add Copy form
        [HttpGet]
        public IActionResult AddCopy(Guid bookId)
        {
            var vm = new AddBookCopyViewModel
            {
                BookId = bookId
            };
            return View(vm);
        }

        // POST: Handle form submission
        [HttpPost]
        public IActionResult AddCopy(AddBookCopyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _bookService.AddBookCopy(vm);
            return RedirectToAction("Details", new { id = vm.BookId }); // Redirect to book details
        }

        // Archive section
        public IActionResult Archive()
        {
            var archivedBooks = _bookService.GetArchivedBooks();
            return View(archivedBooks);
        }

        [HttpPost]
        public IActionResult ArchiveBook(Guid id)
        {
            try
            {
                _bookService.ArchiveBook(id, true);
                return Json(new { success = true });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult RestoreBook(Guid id)
        {
            try
            {
                _bookService.ArchiveBook(id, false);
                return Json(new { success = true });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Pull-out functionality
        public IActionResult PulloutModal(Guid bookCopyId)
        {
            var bookCopy = _bookService.GetBookCopyForPullout(bookCopyId);
            if (bookCopy == null)
                return NotFound();

            return PartialView("_PulloutBookCopyPartial", bookCopy);
        }

        [HttpPost]
        public IActionResult Pullout(PulloutBookCopyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_PulloutBookCopyPartial", vm);
            }

            _bookService.PulloutBookCopy(vm);
            return Json(new { success = true });
        }

    }
}

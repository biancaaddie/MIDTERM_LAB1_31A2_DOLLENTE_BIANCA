using Library_Management.Models;
using Library_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService = AuthorService.Instance;

        public IActionResult Index()
        {
            var authors = _authorService.GetAuthors();
            return View(authors);
        }

        public IActionResult Archive()
        {
            var archivedAuthors = _authorService.GetArchivedAuthors();
            return View(archivedAuthors);
        }

        public IActionResult Details(Guid id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddAuthorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _authorService.AddAuthor(vm);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var author = _authorService.GetAuthorForEdit(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        [HttpPost]
        public IActionResult Edit(EditAuthorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _authorService.UpdateAuthor(vm);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteModal(Guid id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
                return NotFound();

            return PartialView("_DeleteAuthorPartial", author);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _authorService.DeleteAuthor(id);
                return Json(new { success = true });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Archive(Guid id)
        {
            try
            {
                _authorService.ArchiveAuthor(id);
                return Json(new { success = true });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Restore(Guid id)
        {
            try
            {
                _authorService.RestoreAuthor(id);
                return Json(new { success = true });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Modal actions for better UX
        public IActionResult AddModal()
        {
            return PartialView("_AddAuthorPartial");
        }

        [HttpPost]
        public IActionResult AddModal(AddAuthorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_AddAuthorPartial", vm);
            }

            _authorService.AddAuthor(vm);
            return Json(new { success = true });
        }

        public IActionResult EditModal(Guid id)
        {
            var author = _authorService.GetAuthorForEdit(id);
            if (author == null)
                return NotFound();

            return PartialView("_EditAuthorPartial", author);
        }

        [HttpPost]
        public IActionResult EditModal(EditAuthorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditAuthorPartial", vm);
            }

            _authorService.UpdateAuthor(vm);
            return Json(new { success = true });
        }
    }
}

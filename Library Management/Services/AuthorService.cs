using Library_Management.Models;
using Library_Management_Domain.Entities;

namespace Library_Management.Services
{
    public class AuthorService
    {
        private readonly BookService _bookService = BookService.Instance;

        private AuthorService()
        {
        }

        // Singleton pattern
        private static AuthorService? _instance;
        public static AuthorService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AuthorService();
                }
                return _instance;
            }
        }

        public IEnumerable<AuthorListViewModel> GetAuthors(bool includeArchived = false)
        {
            var authors = _bookService.GetAllAuthorsInternal()
                .Where(a => includeArchived || !a.IsArchived);

            return authors.Select(a => new AuthorListViewModel
            {
                AuthorId = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                BirthDate = a.BirthDate,
                ProfileImageUrl = a.ProfileImageUrl,
                BookCount = a.Books.Count(b => !b.IsArchived),
                IsArchived = a.IsArchived
            });
        }

        public IEnumerable<AuthorListViewModel> GetArchivedAuthors()
        {
            return GetAuthors(includeArchived: true).Where(a => a.IsArchived);
        }

        public AuthorDetailsViewModel? GetAuthorById(Guid id)
        {
            var author = _bookService.GetAllAuthorsInternal()
                .FirstOrDefault(a => a.Id == id);

            if (author == null) return null;

            var books = _bookService.GetBooks()
                .Where(b => _bookService.GetAllAuthorsInternal()
                    .Any(a => a.Id == author.Id && a.Books.Any(bk => bk.Id == b.BookId)));

            return new AuthorDetailsViewModel
            {
                AuthorId = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                ProfileImageUrl = author.ProfileImageUrl,
                Books = books.ToList(),
                IsArchived = author.IsArchived
            };
        }

        public EditAuthorViewModel? GetAuthorForEdit(Guid id)
        {
            var author = _bookService.GetAllAuthorsInternal()
                .FirstOrDefault(a => a.Id == id);

            if (author == null) return null;

            return new EditAuthorViewModel
            {
                AuthorId = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                ProfileImageUrl = author.ProfileImageUrl,
                IsArchived = author.IsArchived
            };
        }

        public void AddAuthor(AddAuthorViewModel vm)
        {
            ArgumentNullException.ThrowIfNull(vm, nameof(vm));

            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                Name = vm.Name,
                Biography = vm.Biography,
                BirthDate = vm.BirthDate,
                ProfileImageUrl = vm.ProfileImageUrl,
                IsArchived = false,
                Books = new List<Book>()
            };

            _bookService.AddAuthor(newAuthor);
        }

        public void UpdateAuthor(EditAuthorViewModel vm)
        {
            ArgumentNullException.ThrowIfNull(vm, nameof(vm));
            _bookService.UpdateAuthor(vm);
        }

        public void DeleteAuthor(Guid id)
        {
            _bookService.DeleteAuthor(id);
        }

        public void ArchiveAuthor(Guid id)
        {
            _bookService.ArchiveAuthor(id, true);
        }

        public void RestoreAuthor(Guid id)
        {
            _bookService.ArchiveAuthor(id, false);
        }
    }
}

using Library.Contracts;
using Library.Data;
using Library.Data.Entities;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            var book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                //Category = model.Category,
                CategoryId = model.CategoryId,
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            var user = await context
                .Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var book = await context.Books.FirstOrDefaultAsync(u => u.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid book ID");
            }

            if (!user.ApplicationUsersBooks.Any(b => b.BookId == bookId))
            {
                user.ApplicationUsersBooks.Add(new ApplicationUserBook()
                {
                    BookId = book.Id,
                    ApplicationUserId = user.Id,
                    Book = book,
                    ApplicationUser = user
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            var books = await context.Books.Select(b => new BookViewModel
            {
                Author = b.Author,
                Category = b.Category,
                CategoryId = b.CategoryId,
                Id = b.Id,
                Rating = b.Rating,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
            })
                .ToListAsync();
                
            return books;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<CollectionBookViewModel>> GetMineAsync(string userId)
        {
            var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.ApplicationUsersBooks)
               .ThenInclude(u => u.Book)
               .ThenInclude(u => u.Category)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.ApplicationUsersBooks
               .Select(b => new CollectionBookViewModel()
               {
                   Id = b.BookId,
                   Author = b.Book.Author,
                   Category = b.Book.Category,
                   CategoryId = b.Book.CategoryId,
                   ImageUrl = b.Book.ImageUrl,
                   Title = b.Book.Title,
                   Description = b.Book.Description
               });
        }

        public async Task RemoveBookFromCollectionAsync(int bookId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var book = user.ApplicationUsersBooks.FirstOrDefault(m => m.BookId == bookId);

            if (book != null)
            {
                user.ApplicationUsersBooks.Remove(book);

                await context.SaveChangesAsync();
            }
        }
    }
}

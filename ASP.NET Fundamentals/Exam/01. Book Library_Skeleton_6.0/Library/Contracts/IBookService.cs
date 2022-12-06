using Library.Data.Entities;
using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task AddBookAsync(AddBookViewModel model);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task AddBookToCollectionAsync(int bookId, string userId);

        Task<IEnumerable<CollectionBookViewModel>> GetMineAsync(string userId);

        Task RemoveBookFromCollectionAsync(int bookId, string userId);
    }
}

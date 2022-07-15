using DapperLibrary.Models;

namespace DapperLibrary.Services.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(Book book);
        Task<IEnumerable<dynamic>> GetAllBooksAsync();
        Task<dynamic> GetSingleBookAsync(long id);
        Task<bool> ModifyBookAsync(int id, Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}

using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooksAsync(int pageNumber, int pageSize);
    Task<Book> GetBookByIdAsync(int id);
    
    Task AddBookAsync(Book book);
    
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
}
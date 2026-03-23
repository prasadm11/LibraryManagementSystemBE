using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BookRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Book>> GetAllBooksAsync()
    {
        var response = await _dbContext.Books.ToListAsync();
        return response;
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        var response = await _dbContext.Books.FindAsync(id);
        return response;
    }

    public async Task AddBookAsync(Book book)
    {
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateBookAsync(Book book)
    {
        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);
        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();
    }

}
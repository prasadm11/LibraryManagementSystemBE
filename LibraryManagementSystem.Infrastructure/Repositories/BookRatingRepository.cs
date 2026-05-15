using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class BookRatingRepository : IBookRatingRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BookRatingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(BookRating rating)
    {
        await _dbContext.BookRatings.AddAsync(rating);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> HasUserRatedBook(int userId, int bookId)
    {
        var response =await _dbContext.BookRatings.AnyAsync(r => r.BookId == bookId && r.UserId == userId);
        return response;
    }
    
    public async Task<List<BookRating>> GetBookRatings(int bookId)
    {
        var response = await _dbContext.BookRatings
            .Where(x => x.BookId == bookId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        return response;
    }
}
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
    
    public async Task<List<BookRating>> GetBookRatings(int bookId,int pageNumber, int pageSize)
    {
        var response = await _dbContext.BookRatings
            .AsNoTracking()
            .Where(x => x.BookId == bookId)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return response;
    }
    public async Task<(int TotalRatings, double AverageRating)> GetBookRatingStats(int bookId)
    {
        var query = _dbContext.BookRatings.Where(x => x.BookId == bookId);
        var totalRatings = await query.CountAsync();
        var averageRating = totalRatings > 0 ? Math.Round(await query.AverageAsync(x => x.Rating), 2) : 0;

        return (totalRatings, averageRating);
    }
}
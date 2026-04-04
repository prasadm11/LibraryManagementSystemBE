using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class BorrowRepository : IBorrowRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BorrowRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BorrowRecord>> GetByStatusAsync(BorrowStatus status)
    {
        var response = await  _dbContext.BorrowRecords
            .Where(x => x.Status == status)
            .Include(x => x.Book)
            .Include(x => x.User)
            .ToListAsync();
        return response;
    }

    public async Task AddAsync(BorrowRecord borrow)
    {
        var response = await _dbContext.AddAsync(borrow);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<BorrowRecord?> GetByIdAsync(int id)
    {
        var response = await _dbContext.BorrowRecords
            .Include(x => x.Book)
            .FirstOrDefaultAsync(x => x.Id == id);
        return response;
    }

    public async Task UpdateAsync(BorrowRecord borrow)
    {
        _dbContext.BorrowRecords.Update(borrow);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<BorrowRecord>> GetByUserIdAsync(int userId)
    {
        var response = await _dbContext.BorrowRecords
            .Where(x => x.UserId == userId)
            .Include(x => x.Book)
            .ToListAsync();
        return response;
    }

    public async Task<List<BorrowRecord>> GetOverdueBooksAsync()
    {
        var response = await _dbContext.BorrowRecords
            .Where(x => x.ReturnedAt == null && x.DueDate < DateTime.UtcNow)
            .Include(x => x.Book)
            .Include(x => x.User)
            .ToListAsync();
        return response;
    }

    public async Task<List<Book>> SearchBooksAsync(string keyword)
    {
        var response = await _dbContext.Books
            .Where(x => x.Title.ToLower().Contains(keyword.ToLower()) || x.Author.ToLower().Contains(keyword.ToLower()))
            .ToListAsync();
        return response;
    }

    public async Task<List<BorrowRecord>> GetAllAsync()
    {
        var response = await _dbContext.BorrowRecords.ToListAsync();
        return response;
    }
    
    public async Task<List<BorrowRecord>> GetUserBorrowRecordsAsync(int userId)
    {
        var response = await _dbContext.BorrowRecords
            .Where(x => x.UserId == userId)
            .ToListAsync();
        return response;
    }

    public async Task<List<BorrowRecord>> GetDueSoonBooksAsync(int days)
    {
        var today = DateTime.UtcNow.Date;
        var targetDate = today.AddDays(days);
        var response = await _dbContext.BorrowRecords
            .Where(x => x.ReturnedAt == null && x.DueDate.Date >= today && x.DueDate.Date <= targetDate)
            .Include(x => x.Book)
            .Include(x => x.User)
            .ToListAsync();
        return response;
    }

}
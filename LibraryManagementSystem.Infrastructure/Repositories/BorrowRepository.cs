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

    public async Task<List<BorrowRecord>> GetByStatusAsync(BorrowStatus status, int pageNumber, int pageSize)
    {
        var response = await  _dbContext.BorrowRecords
            .AsNoTracking()
            .Where(x => x.Status == status)
            .Include(x => x.Book)
            .Include(x => x.User)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
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
            .AsNoTracking()
            .Include(x => x.Book)
            .FirstOrDefaultAsync(x => x.Id == id);
        return response;
    }

    public async Task UpdateAsync(BorrowRecord borrow)
    {
        _dbContext.BorrowRecords.Update(borrow);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<BorrowRecord>> GetByUserIdAsync(int userId,int pageNumber, int pageSize)
    {
        var response = await _dbContext.BorrowRecords
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Include(x => x.Book)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return response;
    }

    public async Task<List<BorrowRecord>> GetOverdueBooksAsync(int pageNumber, int pageSize)
    {
        var response = await _dbContext.BorrowRecords
            .AsNoTracking()
            .Where(x => x.ReturnedAt == null && x.DueDate < DateTime.UtcNow)
            .Include(x => x.Book)
            .Include(x => x.User)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return response;
    }
    
    public async Task<List<BorrowRecord>> GetOverdueBooksAsync()
    {
        var response = await _dbContext.BorrowRecords
            .AsNoTracking()
            .Where(x => x.ReturnedAt == null && x.DueDate < DateTime.UtcNow)
            .Include(x => x.Book)
            .Include(x => x.User)
            .ToListAsync();
        return response;
    }

    public async Task<List<Book>> SearchBooksAsync(string keyword,int pageNumber, int pageSize)
    {
        var response = await _dbContext.Books
            .AsNoTracking()
            .Where(x => x.Title.ToLower().Contains(keyword.ToLower()) || 
                        x.Author.ToLower().Contains(keyword.ToLower()) 
                        // x.Genre.ToLower().Contains(keyword.ToLower()) ||
                        // x.PublishedYear.ToString().ToLower().Contains(keyword.ToLower())
                        ) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return response;
    }

    public async Task<List<BorrowRecord>> GetAllAsync(int pageNumber, int pageSize)
    {
        var response = await _dbContext.BorrowRecords
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return response;
    }
    
    public async Task<List<BorrowRecord>> GetAllAsync()
    {
        var response = await _dbContext.BorrowRecords
            .AsNoTracking()
            .ToListAsync();
        return response;
    }
    public async Task<List<BorrowRecord>> GetUserBorrowRecordsAsync(int userId,int pageNumber, int pageSize)
    {
        var response = await _dbContext.BorrowRecords
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return response;
    }
    
    public async Task<List<BorrowRecord>> GetUserBorrowRecordsAsync(int userId)
    {
        return await _dbContext.BorrowRecords
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<BorrowRecord>> GetDueSoonBooksAsync(int days,int pageNumber, int pageSize)
    {
        var today = DateTime.UtcNow.Date;
        var targetDate = today.AddDays(days);
        var response = await _dbContext.BorrowRecords
            .AsNoTracking()
            .Where(x => x.ReturnedAt == null && x.DueDate.Date >= today && x.DueDate.Date <= targetDate)
            .Include(x => x.Book)
            .Include(x => x.User)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return response;
    }

    public async Task<bool> HasUserReturnedBook(int userId, int bookId)
    {
        var response = await _dbContext.BorrowRecords.AnyAsync(x =>
            x.UserId == userId && x.BookId == bookId &&
            (x.Status == BorrowStatus.Returned || x.Status == BorrowStatus.ReturnedLate));
        return response;
    }

}
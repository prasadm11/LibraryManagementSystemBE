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
}
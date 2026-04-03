using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class BorrowRepository : IBorrowRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BorrowRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(BorrowRecord borrow)
    {
        var response = await _dbContext.AddAsync(borrow);
        await _dbContext.SaveChangesAsync();
    }
}
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class BorrowRequestRepository : IBorrowRequestRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public BorrowRequestRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task AddAsync(BorrowRecordsUserRequest request)
    {
        await _dbContext.BorrowRecordsUserRequests.AddAsync(request);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<BorrowRecordsUserRequest>> GetPendingRequestsAsync(int pageNumber, int pageSize)
    {
        //only fetch pending requests 
        var response = await _dbContext.BorrowRecordsUserRequests
            .AsNoTracking()
            .Where(x => x.Status == BorrowRequestStatus.Pending)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return response;
    }

    public async Task<BorrowRecordsUserRequest?> GetByIdAsync(int id)
    {
        var response = await _dbContext.BorrowRecordsUserRequests.FirstOrDefaultAsync(x => x.Id == id);
        return response;
    }

    public async Task UpdateAsync(BorrowRecordsUserRequest request)
    {
        _dbContext.BorrowRecordsUserRequests.Update(request);
        await _dbContext.SaveChangesAsync();
    }
    
}
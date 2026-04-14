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

    public async Task<List<BorrowRecordsUserRequest>> GetPendingRequestsAsync()
    {
        //only fetch pending requests 
        var response = await _dbContext.BorrowRecordsUserRequests
            .Where(x => x.Status == BorrowRequestStatus.Pending)
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
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IBorrowRequestRepository
{
    Task AddAsync(BorrowRecordsUserRequest request);
    
    Task<BorrowRecordsUserRequest?> GetByIdAsync(int id);
    //
    Task<List<BorrowRecordsUserRequest>> GetPendingRequestsAsync();
    //
    Task UpdateAsync(BorrowRecordsUserRequest request);
}
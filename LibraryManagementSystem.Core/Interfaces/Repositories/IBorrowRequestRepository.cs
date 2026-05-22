using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IBorrowRequestRepository
{
    Task AddAsync(BorrowRecordsUserRequest request);
    
    Task<BorrowRecordsUserRequest?> GetByIdAsync(int id);
    //
    Task<List<BorrowRecordsUserRequest>> GetPendingRequestsAsync(int pageNumber, int pageSize);
    //
    Task UpdateAsync(BorrowRecordsUserRequest request);
}
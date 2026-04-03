using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IBorrowRepository
{
    Task AddAsync(BorrowRecord borrow);
}
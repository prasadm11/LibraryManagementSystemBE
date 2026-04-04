using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IBorrowRepository
{
    Task<List<BorrowRecord>> GetByStatusAsync(BorrowStatus status);
    Task AddAsync(BorrowRecord borrow);
    
    //Return Book -> first get that borrow info then update that returned record in DB
    Task<BorrowRecord?> GetByIdAsync(int id);
    Task UpdateAsync(BorrowRecord borrow);
    
    Task<List<BorrowRecord>> GetByUserIdAsync(int userId);
    
    Task<List<BorrowRecord>> GetOverdueBooksAsync();
    
    Task<List<Book>> SearchBooksAsync(string keyword);
    
    Task<List<BorrowRecord>> GetAllAsync();
}
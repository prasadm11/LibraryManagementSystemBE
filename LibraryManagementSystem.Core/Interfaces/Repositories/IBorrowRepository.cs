using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IBorrowRepository
{
    Task<List<BorrowRecord>> GetByStatusAsync(BorrowStatus status,int pageNumber, int pageSize);
    Task AddAsync(BorrowRecord borrow);
    
    //Return Book -> first get that borrow info then update that returned record in DB
    Task<BorrowRecord?> GetByIdAsync(int id);
    Task UpdateAsync(BorrowRecord borrow);
    
    Task<List<BorrowRecord>> GetByUserIdAsync(int userId,int pageNumber, int pageSize);
    
    Task<List<BorrowRecord>> GetOverdueBooksAsync(int pageNumber, int pageSize);
    
    Task<List<BorrowRecord>> GetOverdueBooksAsync();
    
    Task<List<Book>> SearchBooksAsync(string keyword,int pageNumber, int pageSize);
    
    Task<List<BorrowRecord>> GetAllAsync(int pageNumber, int pageSize);

    Task<List<BorrowRecord>> GetUserBorrowRecordsAsync(int userId,int pageNumber, int pageSize);
    
    Task<List<BorrowRecord>> GetDueSoonBooksAsync(int days,int pageNumber, int pageSize);
    
    Task<bool> HasUserReturnedBook(int userId, int bookId);
    
    Task<List<BorrowRecord>> GetUserBorrowRecordsAsync(int userId);
    
    Task<List<BorrowRecord>> GetAllAsync();
}
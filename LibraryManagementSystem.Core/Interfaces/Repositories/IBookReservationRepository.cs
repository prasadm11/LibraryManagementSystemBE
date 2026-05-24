using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IBookReservationRepository
{
    Task AddAsync(BookReservation reservation);

    Task<List<BookReservation>> GetBookReservationsAsync(int bookId,int pageNumber, int pageSize);

    Task<List<BookReservation>> GetUserReservationsAsync(int userId,int pageNumber, int pageSize);

    Task<BookReservation?> GetNextReservationAsync(int bookId);

    Task UpdateAsync(BookReservation reservation);

    Task DeleteAsync(BookReservation reservation);
    
    Task<BookReservation?> GetByIdAsync(int reservationId);
}
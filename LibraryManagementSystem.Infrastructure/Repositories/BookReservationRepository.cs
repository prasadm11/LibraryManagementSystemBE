using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class BookReservationRepository : IBookReservationRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public BookReservationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(BookReservation reservation)
    {
        await _dbContext.BookReservations.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<BookReservation>> GetBookReservationsAsync(int bookId,int pageNumber, int pageSize)
    {
        var response = await _dbContext.BookReservations
            .AsNoTracking()
            .Where(x => x.BookId == bookId && !x.IsDeleted)
            .OrderBy(x => x.ReservedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return response;
    }
    
    public async Task<List<BookReservation>> GetUserReservationsAsync(int userId,int pageNumber, int pageSize)
    {
        var response = await _dbContext.BookReservations
            .AsNoTracking()
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .OrderByDescending(x => x.ReservedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return response;
    }
    
    public async Task<BookReservation?> GetNextReservationAsync(int bookId)
    {
        var response = await _dbContext.BookReservations
            .Where(x => x.BookId == bookId && !x.IsDeleted && !x.IsFulfilled && !x.IsCancelled)
            .OrderBy(x => x.ReservedAt)
            .FirstOrDefaultAsync();
        
        return response;
    }
    public async Task UpdateAsync(BookReservation reservation)
    {
        _dbContext.BookReservations.Update(reservation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(BookReservation reservation)
    {
        _dbContext.BookReservations.Update(reservation);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<BookReservation?> GetByIdAsync(int reservationId)
    {
        var response= await _dbContext.BookReservations
            .FirstOrDefaultAsync(x =>
                x.Id == reservationId &&
                !x.IsDeleted);
        return response;
    }

    
}
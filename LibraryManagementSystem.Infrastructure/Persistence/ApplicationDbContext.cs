using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    public DbSet<User> Users{get;set;}
    public DbSet<Book> Books{get;set;}
    
    public DbSet<BorrowRecord>  BorrowRecords{get;set;}
    
    public DbSet<BorrowRecordsUserRequest> BorrowRecordsUserRequests { get; set; }
    
    public DbSet<Notification> Notifications { get; set; }
    
    public DbSet<BookRating> BookRatings { get; set; }
    
    public DbSet<BookReservation>  BookReservations { get; set; }
}
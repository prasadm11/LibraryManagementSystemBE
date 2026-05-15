using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IBookRatingRepository
{
    Task AddAsync(BookRating rating);

    Task<bool> HasUserRatedBook(int userId, int bookId);

    Task<List<BookRating>> GetBookRatings(int bookId);
}
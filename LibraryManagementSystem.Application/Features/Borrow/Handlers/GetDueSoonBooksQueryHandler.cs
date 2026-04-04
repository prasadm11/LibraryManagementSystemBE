using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class GetDueSoonBooksQueryHandler : IRequestHandler<GetDueSoonBooksQuery , List<GetDueSoonBooksResponseDto>>
{
    private readonly IBorrowRepository _borrowRepository;

    public GetDueSoonBooksQueryHandler(IBorrowRepository borrowRepository)
    {
        _borrowRepository = borrowRepository;
    }
    public async Task<List<GetDueSoonBooksResponseDto>> Handle(
        GetDueSoonBooksQuery request,
        CancellationToken cancellationToken)
    {
        // 1. Get records from repo
        var records = await _borrowRepository.GetDueSoonBooksAsync(request.days);

        var today = DateTime.UtcNow.Date;

        // 2. Map manually
        var response = records.Select(x => new GetDueSoonBooksResponseDto
        {
            BorrowId = x.Id,
            BookTitle = x.Book.Title,
            UserId = x.UserId,
            UserName = x.User.FirstName + " " + x.User.LastName,
            DueDate = x.DueDate,
            DaysRemaining = (x.DueDate.Date - today).Days
        }).ToList();

        return response;
    }
}
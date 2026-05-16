using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class GetDueSoonBooksQueryHandler : IRequestHandler<GetDueSoonBooksQuery , ApiResponseModel<List<GetDueSoonBooksResponseDto>>>
{
    private readonly IBorrowRepository _borrowRepository;

    public GetDueSoonBooksQueryHandler(IBorrowRepository borrowRepository)
    {
        _borrowRepository = borrowRepository;
    }
    public async Task<ApiResponseModel<List<GetDueSoonBooksResponseDto>>> Handle(
        GetDueSoonBooksQuery request,
        CancellationToken cancellationToken)
    {
        // 1. Get records from repo
        var records = await _borrowRepository.GetDueSoonBooksAsync(request.days);

        var today = DateTime.UtcNow.Date;

        // 2. Map manually
        var result = records.Select(x => new GetDueSoonBooksResponseDto
        {
            BorrowId = x.Id,
            BookTitle = x.Book.Title,
            UserId = x.UserId,
            UserName = x.User.FirstName + " " + x.User.LastName,
            Email =  x.User.Email,
            DueDate = x.DueDate,
            DaysRemaining = (x.DueDate.Date - today).Days
        }).ToList();
        
        var response = ApiResponseModel<List<GetDueSoonBooksResponseDto>>.SuccessResponse(
            result, 
            "Due soon books fetched successfully",
            200);

        return response;
    }
}
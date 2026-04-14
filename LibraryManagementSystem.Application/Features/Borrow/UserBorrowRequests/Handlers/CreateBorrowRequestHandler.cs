using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class CreateBorrowRequestHandler : IRequestHandler<CreateBorrowRequestCommand , CreateBorrowResponseDto> 
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    
    public CreateBorrowRequestHandler(IBorrowRequestRepository borrowRequestRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
    }

    public async Task<CreateBorrowResponseDto> Handle(CreateBorrowRequestCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.CreateBorrowRequestDto;

        var createBorrowRequest = new BorrowRecordsUserRequest
        {
            UserId = request.UserId,
            BookId = request.BookId,
            Notes = request.Notes,
            Type = BorrowRequestType.Borrow,
            Status = BorrowRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _borrowRequestRepository.AddAsync(createBorrowRequest);

        var response = new CreateBorrowResponseDto
        {
            Message = "Borrow request submitted successfully"
        };
        
        return response;
    }
}



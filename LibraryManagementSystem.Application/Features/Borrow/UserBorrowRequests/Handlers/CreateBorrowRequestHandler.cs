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
    private readonly IBorrowRepository _borrowRepository;
    
    public CreateBorrowRequestHandler(IBorrowRequestRepository borrowRequestRepository, IBorrowRepository borrowRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _borrowRepository = borrowRepository;
    }

    public async Task<CreateBorrowResponseDto> Handle(CreateBorrowRequestCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.CreateBorrowRequestDto;
        var borrowRecords = await _borrowRepository.GetUserBorrowRecordsAsync(command.CreateBorrowRequestDto.UserId);
        var today = DateTime.UtcNow;
        var activeCount = borrowRecords.Count(x => x.Status == BorrowStatus.Active);
        var hasOverdue = borrowRecords.Any(x => x.ReturnedAt == null && x.DueDate < today);
        var hasUnpaidFine = borrowRecords.Any(x => x.FineAmount > 0 && !x.FinePaid);
        var maxLimitReached = activeCount >= 3;
        
        if (hasOverdue)

        {

            throw new Exception(

                "User has overdue books");

        }

        if (hasUnpaidFine)

        {

            throw new Exception(

                "User has unpaid fines");

        }

        if (maxLimitReached)

        {

            throw new Exception(

                "User reached borrow limit");

        }

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



using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class CreateRenewBookRequestCommandHandler : IRequestHandler<CreateRenewBookRequestCommand, string>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IBorrowRepository _borrowRepository;

    public CreateRenewBookRequestCommandHandler(IBorrowRequestRepository borrowRequestRepository, IBorrowRepository borrowRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _borrowRepository = borrowRepository;
    }

    public async Task<string> Handle(CreateRenewBookRequestCommand command, CancellationToken cancellationToken)
    {
        var borrowId = command.Dto.BorrowRecordId;

        var borrow = await _borrowRepository.GetByIdAsync(borrowId);
        
        if (borrow == null)
            throw new KeyNotFoundException("Borrow record not found");
        
        if (borrow.Status != BorrowStatus.Active)
            throw new Exception("Only active borrowed books can be renewed");

        var request = new BorrowRecordsUserRequest
        {
            UserId = borrow.UserId,
            BookId = borrow.BookId,
            BorrowRecordId = borrow.Id,
            Type = BorrowRequestType.Renew,
            Status = BorrowRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        await _borrowRequestRepository.AddAsync(request);
        return "Renew request submitted successfully";
        
    }
}
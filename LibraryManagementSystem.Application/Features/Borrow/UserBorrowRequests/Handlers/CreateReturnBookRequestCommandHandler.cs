using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class CreateReturnBookRequestCommandHandler : IRequestHandler<CreateReturnBookRequestCommand, string>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IBorrowRepository _borrowRepository;

    public CreateReturnBookRequestCommandHandler(IBorrowRequestRepository borrowRequestRepository,IBorrowRepository borrowRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _borrowRepository = borrowRepository;
    }
    public async Task<string> Handle(
        CreateReturnBookRequestCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.createReturnBookRequestDto;
        
        var borrow = await _borrowRepository.GetByIdAsync(request.BorrowRecordId);

        if (borrow == null)
            throw new Exception("Borrow record not found");
        
        if (borrow.Status == BorrowStatus.Returned || borrow.Status == BorrowStatus.ReturnedLate)
            throw new Exception("Book already returned");
        
        //check and calculate the fine
        if (borrow.DueDate < DateTime.UtcNow)
        {
            var lateDays = (DateTime.UtcNow - borrow.DueDate).Days;
            if (lateDays < 1)
            {
                lateDays = 1;
            }

            borrow.FineAmount = lateDays * 20;
            borrow.FinePaid = false;
            await _borrowRepository.UpdateAsync(borrow);
        }

        
        var result = new BorrowRecordsUserRequest
        {
            UserId =  borrow.UserId,
            BorrowRecordId = request.BorrowRecordId, 
            Type = BorrowRequestType.Return, 
            Status = BorrowRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow,
        };

        await _borrowRequestRepository.AddAsync(result);

        return "Return request submitted successfully";
    }
}
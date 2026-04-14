using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class RejectBorrowRequestCommandHandler  : IRequestHandler<RejectBorrowRequestCommand, string>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;

    public RejectBorrowRequestCommandHandler(IBorrowRequestRepository borrowRequestRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
    }

    public async Task<string> Handle(RejectBorrowRequestCommand command, CancellationToken cancellationToken)
    {
        var request = await _borrowRequestRepository.GetByIdAsync(command.BorrowRequestId);
        
        if (request == null)
            throw new KeyNotFoundException("Request not found");
        
        if (request.Status != BorrowRequestStatus.Pending)
            throw new Exception("Request already processed");
        
        request.Status = BorrowRequestStatus.Rejected;
        request.ApprovedAt = DateTime.UtcNow;
        
        await _borrowRequestRepository.UpdateAsync(request);
        
        return "Request rejected successfully";
    }
}
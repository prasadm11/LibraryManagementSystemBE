using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class RejectRequestCommandHandler  : IRequestHandler<RejectRequestCommand, string>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;

    public RejectRequestCommandHandler(IBorrowRequestRepository borrowRequestRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
    }

    public async Task<string> Handle(RejectRequestCommand command, CancellationToken cancellationToken)
    {
        var request = await _borrowRequestRepository.GetByIdAsync(command.id);
        
        if (request == null)
            throw new KeyNotFoundException("Request not found");
        
        if (request.Status != BorrowRequestStatus.Pending)
            throw new Exception("Request already processed");
        
        //Handler
        
        request.Status = BorrowRequestStatus.Rejected;
        request.ApprovedAt = DateTime.UtcNow;
        
        await _borrowRequestRepository.UpdateAsync(request);
        
        return "Request rejected successfully";
    }
}
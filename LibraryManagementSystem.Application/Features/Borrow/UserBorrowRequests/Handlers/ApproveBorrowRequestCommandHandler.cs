using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class ApproveBorrowRequestCommandHandler : IRequestHandler<ApproveBorrowRequestCommand , string>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IBorrowRequestRepository _borrowRequestRepository;

    public ApproveBorrowRequestCommandHandler(IMapper mapper, IBorrowRequestRepository borrowRequestRepository, IMediator mediator)
    {
        _mapper = mapper;
        _borrowRequestRepository = borrowRequestRepository;
        _mediator = mediator;
        
    }

    public async Task<string> Handle(ApproveBorrowRequestCommand command, CancellationToken cancellationToken)
    {
        var request = await _borrowRequestRepository.GetByIdAsync(command.BorrowRequestId);

        if (request == null)
        {
            throw new KeyNotFoundException("Request not found");
        }

        if (request.Status != BorrowRequestStatus.Pending)
        {
            throw new Exception("Request already processed");
        }
        //execute main boorow logic add borrow
        await _mediator.Send(new BorrowBookCommand(new BorrowBookRequestDto
        {
            UserId =  request.UserId,
            BookId = request.BookId,
            Notes =  request.Notes,
        }));
        
        //after execution in borrowrecord tbl update status in userrequestbl
        request.Status = BorrowRequestStatus.Approved;
        request.ApprovedAt = DateTime.UtcNow;
        
        await _borrowRequestRepository.UpdateAsync(request);
        
        return "Request approved successfully";
    }
    
}
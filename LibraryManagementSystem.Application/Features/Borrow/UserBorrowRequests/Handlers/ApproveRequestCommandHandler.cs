using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand , ApproveRequestResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IBorrowRequestRepository _borrowRequestRepository;

    public ApproveRequestCommandHandler(IMapper mapper, IBorrowRequestRepository borrowRequestRepository, IMediator mediator)
    {
        _mapper = mapper;
        _borrowRequestRepository = borrowRequestRepository;
        _mediator = mediator;
        
    }
    

    public async Task<ApproveRequestResponseDto> Handle(ApproveRequestCommand command, CancellationToken cancellationToken)
    {
        var request = await _borrowRequestRepository.GetByIdAsync(command.id);

        if (request == null)
        {
            throw new KeyNotFoundException("Request not found");
        }

        if (request.Status != BorrowRequestStatus.Pending)
        {
            throw new Exception("Request already processed");
        }
        //Handle the approve request according to the type like borrow,return,renew

        switch (request.Type)
        {
            case BorrowRequestType.Borrow:
                await _mediator.Send(new BorrowBookCommand(new BorrowBookRequestDto
                {
                    UserId =  request.UserId,
                    BookId = request.BookId,
                    Notes =  request.Notes,
                }));
                break;
            
            case BorrowRequestType.Return:
                if (request.BorrowRecordId == null)
                    throw new KeyNotFoundException("Invalid return request");
                await _mediator.Send(new ReturnBookCommand(new ReturnBookRequestDto
                {
                    BorrowId = request.BorrowRecordId.Value,
                }));
                break;
            
            case BorrowRequestType.Renew:
                if (request.BorrowRecordId == null)
                {
                    throw new KeyNotFoundException("Invalid renew request");
                }
                await _mediator.Send(new RenewBookCommand(new RenewBookRequestDto
                {
                    BorrowId = request.BorrowRecordId.Value
                }));
                break;
            default:
                throw new Exception("Invalid request type");
                
        }
        
        //after execution in borrowrecord tbl update status in userrequestbl
        request.Status = BorrowRequestStatus.Approved;
        request.ApprovedAt = DateTime.UtcNow;
        
        await _borrowRequestRepository.UpdateAsync(request);

        var response = new ApproveRequestResponseDto
        {
            Message = "Request approved successfully"
        };
        return response;

    }
    
}
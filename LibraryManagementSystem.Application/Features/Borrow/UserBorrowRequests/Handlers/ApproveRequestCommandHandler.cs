using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand , ApiResponseModel<ApproveRequestResponseDto>>
{
    private readonly IMediator _mediator;
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IBorrowRepository _borrowRepository;

    public ApproveRequestCommandHandler( IBorrowRequestRepository borrowRequestRepository, IMediator mediator,IBorrowRepository borrowRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _mediator = mediator;
        _borrowRepository = borrowRepository;
        
    }
    

    public async Task<ApiResponseModel<ApproveRequestResponseDto>> Handle(ApproveRequestCommand command, CancellationToken cancellationToken)
    {
        var request = await _borrowRequestRepository.GetByIdAsync(command.id);

        if (request == null)
        {
            throw new KeyNotFoundException("Request not found");
        }

        if (request.Status != BorrowRequestStatus.Pending)
        {
            throw new InvalidOperationException("Request already processed");
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
                
                var borrow = await _borrowRepository.GetByIdAsync(request.BorrowRecordId.Value);
                if (borrow == null)
                {
                    throw new KeyNotFoundException("Borrow record not found");
                }
                if (borrow.FineAmount > 0 && !borrow.FinePaid)
                {
                    throw new InvalidOperationException("Fine must be paid before approving return");
                }
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
                
                var renewBorrow = await _borrowRepository.GetByIdAsync(request.BorrowRecordId.Value);
                if (renewBorrow == null)
                {
                    throw new KeyNotFoundException("Borrow record not found");
                }
                if (renewBorrow.FineAmount > 0 && !renewBorrow.FinePaid)
                {
                    throw new InvalidOperationException("Fine must be paid before approving renew");
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

        var result = new ApproveRequestResponseDto
        {
            Message = "Request approved successfully"
        };

        var response = ApiResponseModel<ApproveRequestResponseDto>
            .SuccessResponse(
                result,
                "Request approved successfully",
                200
            );

        return response;

    }
    
}
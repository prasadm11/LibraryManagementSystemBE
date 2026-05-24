using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
// using LibraryManagementSystem.Infrastructure.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookReservation.Commands;
using LibraryManagementSystem.Application.Features.BookReservation.DTOs;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand , ApiResponseModel<ReturnBookResponseDto>>
{
    private readonly IBorrowRepository _borrowRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ReturnBookCommandHandler(IBorrowRepository borrowRepository, IMapper mapper, IMediator mediator)
    {
        _borrowRepository = borrowRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ApiResponseModel<ReturnBookResponseDto>> Handle(ReturnBookCommand command, CancellationToken cancellationToken)
    {
        var request = command.ReturnBookRequestDto;
        
        //first fetch the borrowred record
        var borrow = await _borrowRepository.GetByIdAsync(request.BorrowId);

        if (borrow == null)
        {
            throw new KeyNotFoundException($"No Borrow Record exist with this Borrow id {request.BorrowId}");
        }
        
        //check if book is already returned
        if (borrow.Status == BorrowStatus.Returned || borrow.Status == BorrowStatus.ReturnedLate)
        {
            throw new InvalidOperationException("Book is already returned");
        }
        
        //set return date
        var currentTime = DateTime.UtcNow;
        borrow.ReturnedAt = currentTime;
        
        
        //update fine and set status to returned
        if (currentTime > borrow.DueDate)
        {
            var LateDays = (currentTime - borrow.DueDate).Days;
            
            borrow.Status = BorrowStatus.ReturnedLate;
            borrow.FineAmount = LateDays * 10; //10 RS per day fine
            
        }
        else
        {
            borrow.Status =  BorrowStatus.Returned;
        }
        
        //Update the Book Copies
        borrow.Book.AvailableCopies++;
        
        await _borrowRepository.UpdateAsync(borrow);
        
        //call the reservation logic
        await _mediator.Send(new NotifyNextReservationUserCommand(
                new NotifyNextReservationUserRequestDto
                {
                    BookId = borrow.BookId,
                    BookTitle = borrow.Book.Title
                }));
        
        var result = _mapper.Map<ReturnBookResponseDto>(borrow);

        var response = ApiResponseModel<ReturnBookResponseDto>
            .SuccessResponse(
                result,
                "Book returned successfully",
                200
            );

        return response;

    }
}
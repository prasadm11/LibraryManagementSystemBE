using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand , ReturnBookResponseDto>
{
    private readonly IBorrowRepository _borrowRepository;
    private readonly IMapper _mapper;

    public ReturnBookCommandHandler(IBorrowRepository borrowRepository, IMapper mapper)
    {
        _borrowRepository = borrowRepository;
        _mapper = mapper;
    }

    public async Task<ReturnBookResponseDto> Handle(ReturnBookCommand command, CancellationToken cancellationToken)
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
            
            borrow.Status =  BorrowStatus.Returned;
            borrow.FineAmount = LateDays * 10; //10 RS per day fine
            
        }
        else
        {
            borrow.Status =  BorrowStatus.Returned;
        }
        
        //Update the Book Copies
        borrow.Book.AvailableCopies++;
        
        await _borrowRepository.UpdateAsync(borrow);
        
        var response = _mapper.Map<ReturnBookResponseDto>(borrow);
        return response;

    }
}
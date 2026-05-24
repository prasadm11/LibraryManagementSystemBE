using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, ApiResponseModel<BorrowBookResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;
    private readonly IBorrowRepository _borrowRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookReservationRepository _bookReservationRepository;

    public BorrowBookCommandHandler(IMapper mapper, IBookRepository bookRepository, IBorrowRepository borrowRepository,
        IUserRepository userRepository, IBookReservationRepository bookReservationRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
        _borrowRepository = borrowRepository;
        _userRepository = userRepository;
        _bookReservationRepository = bookReservationRepository;
    }

    public async Task<ApiResponseModel<BorrowBookResponseDto>> Handle(BorrowBookCommand command, CancellationToken cancellationToken)
    {
        var request = command.BorrowBookRequestDto;
        
        //check user exist
        var user = await _userRepository.GetUserByIdAsync(request.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        
        //check book exist
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);
        if (book == null)
        {
            throw new KeyNotFoundException("Book not found");
        }
        
        //check copies available
        if (book.AvailableCopies <= 0)
        {
            throw new Exception("Not enough copies");
        }
        
        //map
        var borrow = _mapper.Map<Core.Entities.BorrowRecord>(request);

        borrow.BorrowedAt = DateTime.UtcNow;
        borrow.DueDate = DateTime.UtcNow.AddDays(7);
        borrow.Status = Core.Enums.BorrowStatus.Active;
        borrow.FineAmount = 0;
        borrow.FinePaid = false;
        
        //update book count
        book.AvailableCopies--; 
        
        //save data
        await _borrowRepository.AddAsync(borrow);
        await _bookRepository.UpdateBookAsync(book);
        
        var reservations = await _bookReservationRepository.GetUserReservationsAsync(request.UserId,1,int.MaxValue);

        var reservation = reservations.FirstOrDefault(x=>x.BookId == request.BookId && !x.IsCancelled && !x.IsFulfilled);

        if (reservations != null)
        {
            reservation.IsFulfilled = true;
            await _bookReservationRepository.UpdateAsync(reservation);
        }
        
        
        
        var result = _mapper.Map<BorrowBookResponseDto>(borrow);
        result.Message = "Book borrowed successfully";

        var response = ApiResponseModel<BorrowBookResponseDto>.SuccessResponse(
            result,
            "Book borrowed successfully",
            200);
        
        return response;
    }
}
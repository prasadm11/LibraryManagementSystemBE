using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookReservation.Commands;
using LibraryManagementSystem.Application.Features.BookReservation.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookReservation.Handlers;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ApiResponseModel<CreateReservationResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IBookReservationRepository _bookReservationRepository;
    private readonly INotificationRepository _notificationRepository;
    
    public CreateReservationCommandHandler(IUserRepository userRepository, IBookRepository bookRepository, IBookReservationRepository bookReservationRepository,
        INotificationRepository notificationRepository)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _bookReservationRepository = bookReservationRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task<ApiResponseModel<CreateReservationResponseDto>> Handle(CreateReservationCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.createReservationRequestDto;
        var user = await _userRepository.GetUserByIdAsync(request.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);
        if (book == null)
        {
            throw new KeyNotFoundException("Book not found");
        }

        if (book.AvailableCopies > 0)
        {
            return ApiResponseModel<CreateReservationResponseDto>.FailureResponse(
                "Book is already available",
                400
                );
        }
        
        var existingReservations = await _bookReservationRepository.GetUserReservationsAsync(request.UserId,1,int.MaxValue);

        var alreadyReserved = existingReservations.Any(x => 
            x.BookId==request.BookId &&
            !x.IsCancelled && 
            !x.IsFulfilled
            );
        
        if (alreadyReserved)
        {
            throw new Exception("User already has active reservation for this book");
        }

        var reservation = new Core.Entities.BookReservation
        {
            UserId = request.UserId,
            BookId = request.BookId,
            ReservedAt = DateTime.UtcNow,
            IsFulfilled = false,
            IsCancelled = false,
            IsUserNotified = false,
            ExpiryDate = DateTime.UtcNow.AddDays(1)
        };
        await _bookReservationRepository.AddAsync(reservation);
        
        //set notification for user
        await _notificationRepository.AddAsync(new Core.Entities.Notification
        {
            UserId = request.UserId,
            Title = "Reservation Created",
            Message = $"You joined waitlist for '{book.Title}'",
            Type = "Reservation",
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        });

        var result = new CreateReservationResponseDto
        {
            Message = "Reservation created successfully"
        };
        
        var response = ApiResponseModel<CreateReservationResponseDto>
            .SuccessResponse(
                result,
                "Reservation created successfully",
                201);
        
        return response;
    }
    
}
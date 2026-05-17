using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Core.Interfaces.Services;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class CreateBorrowRequestHandler : IRequestHandler<CreateBorrowRequestCommand , ApiResponseModel<CreateBorrowResponseDto>> 
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IBorrowRepository _borrowRepository;
    private readonly IUserRepository _userRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IBookRepository _bookRepository;
    
    public CreateBorrowRequestHandler(IBorrowRequestRepository borrowRequestRepository, IBorrowRepository borrowRepository,IUserRepository userRepository, INotificationRepository notificationRepository,IBookRepository bookRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _borrowRepository = borrowRepository;
        _userRepository = userRepository;
        _notificationRepository = notificationRepository;
        _bookRepository = bookRepository;
    }

    public async Task<ApiResponseModel<CreateBorrowResponseDto>> Handle(CreateBorrowRequestCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.CreateBorrowRequestDto;
        var borrowRecords = await _borrowRepository.GetUserBorrowRecordsAsync(command.CreateBorrowRequestDto.UserId);
        var today = DateTime.UtcNow;
        var activeCount = borrowRecords.Count(x => x.Status == BorrowStatus.Active);
        var hasOverdue = borrowRecords.Any(x => x.ReturnedAt == null && x.DueDate < today);
        var hasUnpaidFine = borrowRecords.Any(x => x.FineAmount > 0 && !x.FinePaid);
        var maxLimitReached = activeCount >= 3;
        
        if (hasOverdue)
        {
            throw new Exception("User has overdue books");
        }

        if (hasUnpaidFine)
        {
            throw new Exception("User has unpaid fines");
        }

        if (maxLimitReached)
        {
            throw new Exception("User reached borrow limit");
        }

        var createBorrowRequest = new BorrowRecordsUserRequest
        {
            UserId = request.UserId,
            BookId = request.BookId,
            Notes = request.Notes,
            Type = BorrowRequestType.Borrow,
            Status = BorrowRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _borrowRequestRepository.AddAsync(createBorrowRequest);

        var admins = await _userRepository.GetAllAdminsAsync();
        var user = await _userRepository.GetUserByIdAsync(request.UserId);
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);

        foreach (var admin in admins)
        {
            await _notificationRepository.AddAsync(new Core.Entities.Notification
            {
                UserId = admin.Id,
                Title = "New Borrow Request",
                Message = $"{user.FirstName} {user.LastName} requested {book.Title}",
                Type = "BorrowRequest",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            }
            );
        }

        var result = new CreateBorrowResponseDto
        {
            Message = "Borrow request submitted successfully"
        };

        var response = ApiResponseModel<CreateBorrowResponseDto>
            .SuccessResponse(
                result,
                "Borrow request submitted successfully",
                200
            );

        return response;
    }
}

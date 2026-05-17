using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class CreateRenewBookRequestCommandHandler : IRequestHandler<CreateRenewBookRequestCommand, ApiResponseModel<CreateRenewBookRequestResponseDto>>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IBorrowRepository _borrowRepository;
    private readonly IUserRepository _userRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IBookRepository _bookRepository;

    public CreateRenewBookRequestCommandHandler(IBorrowRequestRepository borrowRequestRepository, IBorrowRepository borrowRepository, IUserRepository userRepository, INotificationRepository notificationRepository, IBookRepository bookRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _borrowRepository = borrowRepository;
        _userRepository = userRepository;
        _notificationRepository = notificationRepository;
        _bookRepository = bookRepository;
    }

    public async Task<ApiResponseModel<CreateRenewBookRequestResponseDto>> Handle(CreateRenewBookRequestCommand command, CancellationToken cancellationToken)
    {
        var borrowId = command.Dto.BorrowRecordId;

        var borrow = await _borrowRepository.GetByIdAsync(borrowId);
        
        if (borrow == null)
            throw new KeyNotFoundException("Borrow record not found");
        
        if (borrow.Status != BorrowStatus.Active)
            throw new Exception("Only active borrowed books can be renewed");
        
        if (borrow.DueDate < DateTime.UtcNow)
        {
            throw new Exception("Cannot renew overdue book");
        }

        var request = new BorrowRecordsUserRequest
        {
            UserId = borrow.UserId,
            BookId = borrow.BookId,
            BorrowRecordId = borrow.Id,
            Type = BorrowRequestType.Renew,
            Status = BorrowRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        await _borrowRequestRepository.AddAsync(request);
        
        var admins = await _userRepository.GetAllAdminsAsync();
        var user = await _userRepository.GetUserByIdAsync(borrow.UserId);
        var book = await _bookRepository.GetBookByIdAsync(borrow.BookId);
        
        foreach (var admin in admins)
        {
            await _notificationRepository.AddAsync(new Core.Entities.Notification
            {
                UserId = admin.Id,
                Title = "New Renew Request",
                Message = $"{user.FirstName} requested renew for '{book.Title}'",
                Type = "RenewRequest",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }

        var result = new CreateRenewBookRequestResponseDto
        {
            Message = "Renew request submitted successfully"
        };

        var response = ApiResponseModel<CreateRenewBookRequestResponseDto>
            .SuccessResponse(
                result,
                "Renew request submitted successfully",
                200
            );

        return response;
        
    }
}
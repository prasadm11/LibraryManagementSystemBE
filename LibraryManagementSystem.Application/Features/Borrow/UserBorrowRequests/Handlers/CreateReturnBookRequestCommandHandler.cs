using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class CreateReturnBookRequestCommandHandler : IRequestHandler<CreateReturnBookRequestCommand, ApiResponseModel<CreateReturnBookRequestResponseDto>>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IBorrowRepository _borrowRepository;
    private readonly IUserRepository _userRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IBookRepository _bookRepository;

    public CreateReturnBookRequestCommandHandler(IBorrowRequestRepository borrowRequestRepository,IBorrowRepository borrowRepository, IUserRepository userRepository, INotificationRepository notificationRepository, IBookRepository bookRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _borrowRepository = borrowRepository;
        _userRepository = userRepository;
        _notificationRepository = notificationRepository;
        _bookRepository = bookRepository;
    }
    public async Task<ApiResponseModel<CreateReturnBookRequestResponseDto>> Handle(
        CreateReturnBookRequestCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.createReturnBookRequestDto;
        
        var borrow = await _borrowRepository.GetByIdAsync(request.BorrowRecordId);

        if (borrow == null)
            throw new Exception("Borrow record not found");
        
        if (borrow.Status == BorrowStatus.Returned || borrow.Status == BorrowStatus.ReturnedLate)
            throw new Exception("Book already returned");
        
        //check and calculate the fine
        if (borrow.DueDate < DateTime.UtcNow)
        {
            var lateDays = (DateTime.UtcNow - borrow.DueDate).Days;
            if (lateDays < 1)
            {
                lateDays = 1;
            }

            borrow.FineAmount = lateDays * 20;
            borrow.FinePaid = false;
            await _borrowRepository.UpdateAsync(borrow);
        }

        
        var result = new BorrowRecordsUserRequest
        {
            UserId =  borrow.UserId,
            BorrowRecordId = request.BorrowRecordId, 
            Type = BorrowRequestType.Return, 
            Status = BorrowRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow,
        };

        await _borrowRequestRepository.AddAsync(result);
        
        var admins = await _userRepository.GetAllAdminsAsync();
        var user = await _userRepository.GetUserByIdAsync(borrow.UserId);
        var book = await _bookRepository.GetBookByIdAsync(borrow.BookId);
        
        foreach (var admin in admins)
        {
            await _notificationRepository.AddAsync(new Core.Entities.Notification
            {
                UserId = admin.Id,
                Title = "New Return Request",
                Message = $"{user.FirstName} requested return for '{book.Title}'",
                Type = "ReturnRequest",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }
        
        var responseDto = new CreateReturnBookRequestResponseDto
        {
            Message = "Return request submitted successfully"
        };

        var response = ApiResponseModel<CreateReturnBookRequestResponseDto>
            .SuccessResponse(
                responseDto,
                "Return request submitted successfully",
                200
            );

        return response;
    }
}
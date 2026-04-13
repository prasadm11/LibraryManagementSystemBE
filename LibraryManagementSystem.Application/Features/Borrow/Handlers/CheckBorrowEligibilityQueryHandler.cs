using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
// using LibraryManagementSystem.Infrastructure.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class CheckBorrowEligibilityQueryHandler : IRequestHandler<CheckBorrowEligibilityQuery, BorrowEligibilityResponseDto>
{
    private readonly IBorrowRepository _borrowRepository;
    private readonly IUserRepository _userRepository;
    
    public CheckBorrowEligibilityQueryHandler(IBorrowRepository borrowRepository, IUserRepository userRepository)
    {
        _borrowRepository = borrowRepository;
        _userRepository = userRepository;
    }

    public async Task<BorrowEligibilityResponseDto> Handle(CheckBorrowEligibilityQuery command, CancellationToken cancellationToken)
    {
        var record = await _borrowRepository.GetUserBorrowRecordsAsync(command.BorrowEligibilityRequestDto.UserId);
        var user = await _userRepository.GetUserByIdAsync(command.BorrowEligibilityRequestDto.UserId);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {command.BorrowEligibilityRequestDto.UserId} not found");
        }
        var today = DateTime.UtcNow;
        
        //active borrowed book count
        var activeCount = record.Count(x => x.Status == BorrowStatus.Active);
        
        // Check overdue books (not returned + due date passed)
        var hasOverdue = record.Any(x => x.ReturnedAt == null && x.DueDate < today);
        
        //check unpaid fine
        var hasUnpaidFine = record.Any(x => x.FineAmount > 0 && !x.FinePaid);
        
        //borrow limit book max-3
        var maxLimitReached = activeCount >= 3;
        
        //final eligiblity
        var isEligible = !hasOverdue && !hasUnpaidFine && !maxLimitReached;

        string message;

        if (hasOverdue)
        {
            message = "User has overdue books";
        }
        else if (hasUnpaidFine)
        {
            message = "User has unpaid fines";
        }
        else if (maxLimitReached)
        {
            message = "User has reached borrow limit";
        }
        else
        {
            message = "User is eligible to borrow";
        }
        
        var response = new BorrowEligibilityResponseDto
        {
            UserId = command.BorrowEligibilityRequestDto.UserId,
            IsEligible = isEligible,
            Message = message,
            ActiveBorrowCount = activeCount,
            HasOverdueBooks = hasOverdue,
            HasUnpaidFines = hasUnpaidFine
        };
        
        return response;
        
    }
}
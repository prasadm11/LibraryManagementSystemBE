using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class RenewBookCommandHandler : IRequestHandler<RenewBookCommand, ApiResponseModel<RenewBookResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IBorrowRepository _borrowRepository;

    public RenewBookCommandHandler(IMapper mapper, IBorrowRepository borrowRepository)
    {
        _mapper = mapper;
        _borrowRepository = borrowRepository;
    }

    public async Task<ApiResponseModel<RenewBookResponseDto>> Handle(RenewBookCommand command, CancellationToken cancellationToken)
    {
        var request = command.RenewBookRequestDto;
        
        //fetch borrow record first
        var borrow =await _borrowRepository.GetByIdAsync(request.BorrowId);

        if (borrow == null)
        {
            throw new KeyNotFoundException($"Borrow not found with given Id {request.BorrowId}");
        }
        
        //already returned case
        if (borrow.Status == BorrowStatus.Returned || borrow.Status == BorrowStatus.ReturnedLate)
        {
            throw new InvalidOperationException("Cannot renew a returned book.");
        }

        //renewed for 7 days
        var oldDueVale = borrow.DueDate;
        borrow.DueDate = oldDueVale.AddDays(7);
        
        await _borrowRepository.UpdateAsync(borrow);
        
        var result = _mapper.Map<RenewBookResponseDto>(borrow);

        result.Message = $"Book renewed successfully. Next due date is {borrow.DueDate:yyyy-MM-dd}";

        var response = ApiResponseModel<RenewBookResponseDto>
            .SuccessResponse(
                result,
                "Book renewed successfully",
                200
            );

        return response;
    }
}
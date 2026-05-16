using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class PayFineCommandHandler : IRequestHandler<PayFineCommand, ApiResponseModel<PayFineResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IBorrowRepository _borrowRepository;

    public PayFineCommandHandler(IMapper mapper, IBorrowRepository borrowRepository)
    {
        _mapper = mapper;
        _borrowRepository = borrowRepository;
    }

    public async Task<ApiResponseModel<PayFineResponseDto>> Handle(PayFineCommand command, CancellationToken cancellationToken)
    {
        var request = command.PayFineRequestDto;
        
        var borrow = await _borrowRepository.GetByIdAsync(request.BorrowId);
        
        //check not null
        if (borrow == null)
        {
            throw new KeyNotFoundException("Borrow record not found");
        }
        // check fine
        if (borrow.FineAmount <= 0)
        {
            throw new InvalidOperationException("No fine to pay");
        }
        if (borrow.FinePaid)
        {
            throw new InvalidOperationException("Fine is already paid");
        }
        
        borrow.FinePaid = true;
        
        //save changes to paid
        await _borrowRepository.UpdateAsync(borrow);

        var result = new PayFineResponseDto()
        {
            BorrowId = borrow.Id,
            FineAmount = borrow.FineAmount,
            FinePaid = borrow.FinePaid,
            Message = "Fine paid successfully"
        };

        var response = ApiResponseModel<PayFineResponseDto>
            .SuccessResponse(
                result,
                "Fine paid successfully",
                200
            );

        return response;

    }
}

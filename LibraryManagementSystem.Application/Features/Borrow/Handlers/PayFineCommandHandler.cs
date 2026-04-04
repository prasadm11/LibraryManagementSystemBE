using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class PayFineCommandHandler : IRequestHandler<PayFineCommand, PayFineResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IBorrowRepository _borrowRepository;

    public PayFineCommandHandler(IMapper mapper, IBorrowRepository borrowRepository)
    {
        _mapper = mapper;
        _borrowRepository = borrowRepository;
    }

    public async Task<PayFineResponseDto> Handle(PayFineCommand command, CancellationToken cancellationToken)
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

        var response = new PayFineResponseDto()
        {
            BorrowId = borrow.Id,
            FineAmount = borrow.FineAmount,
            FinePaid = borrow.FinePaid,
            Message = "Fine paid successfully"
        };
        return response;

    }
}

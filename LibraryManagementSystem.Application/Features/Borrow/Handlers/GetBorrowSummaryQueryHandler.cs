using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class GetBorrowSummaryQueryHandler : IRequestHandler<GetBorrowSummaryQuery , ApiResponseModel<BorrowSummaryResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IBorrowRepository _borrowRepository;

    public GetBorrowSummaryQueryHandler(IMapper mapper, IBorrowRepository borrowRepository)
    {
        _mapper = mapper;
        _borrowRepository = borrowRepository;
    }

    public async Task<ApiResponseModel<BorrowSummaryResponseDto>> Handle(GetBorrowSummaryQuery request,
        CancellationToken cancellationToken)
    {
        var records =await _borrowRepository.GetAllAsync();
        
        var today = DateTime.UtcNow;
        var summary = new BorrowSummaryResponseDto()
        {
            TotalBorrowed = records.Count,

            Active = records.Count(x => x.Status == BorrowStatus.Active),

            Returned = records.Count(x => x.Status == BorrowStatus.Returned),

            ReturnedLate = records.Count(x => x.Status == BorrowStatus.ReturnedLate),

            Overdue = records.Count(x => x.ReturnedAt == null && x.DueDate < today),

            TotalFineCollected = records.Sum(x => x.FineAmount)
        };

        var result = _mapper.Map<BorrowSummaryResponseDto>(summary);
        
        var response = ApiResponseModel<BorrowSummaryResponseDto>.SuccessResponse(
            result,
            "BorrowSummary fetched successfully",
            200);
        
        return response;
    }
    
    
}
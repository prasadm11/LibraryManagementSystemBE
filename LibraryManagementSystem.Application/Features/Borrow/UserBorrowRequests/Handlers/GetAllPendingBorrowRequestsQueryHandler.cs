using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class GetAllPendingBorrowRequestsQueryHandler : IRequestHandler<GetAllPendingBorrowRequestsQuery, List<GetAllPendingBorrowRequestsResponseDto>>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IMapper _mapper;

    public GetAllPendingBorrowRequestsQueryHandler(IBorrowRequestRepository borrowRequestRepository, IMapper mapper)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _mapper = mapper;
    }

    public async Task<List<GetAllPendingBorrowRequestsResponseDto>> Handle(GetAllPendingBorrowRequestsQuery request,
        CancellationToken cancellationToken)
    {

        var result = await _borrowRequestRepository.GetPendingRequestsAsync();
        
        var response = _mapper.Map<List<GetAllPendingBorrowRequestsResponseDto>>(result);
        
        return response;

    }
}
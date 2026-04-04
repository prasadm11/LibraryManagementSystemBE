using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class GetOverdueBooksQueryHandler : IRequestHandler<GetOverdueBooksQuery, List<GetOverdueBooksResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IBorrowRepository _borrowRepository;

    public GetOverdueBooksQueryHandler(IMapper mapper, IBorrowRepository borrowRepository)
    {
        _mapper = mapper;
        _borrowRepository = borrowRepository;
    }

    public async Task<List<GetOverdueBooksResponseDto>> Handle(GetOverdueBooksQuery request,
        CancellationToken cancellationToken)
    {
        var records = await _borrowRepository.GetOverdueBooksAsync();
        var response = _mapper.Map<List<GetOverdueBooksResponseDto>>(records);
        for (int i = 0; i < response.Count; i++)
        {
            var borrow = records[i];

            var daysLate = (DateTime.UtcNow - borrow.DueDate).Days;

            response[i].DaysLate = daysLate;
            response[i].FineAmount = daysLate * 10;
        }
        
        return response;
    }
}
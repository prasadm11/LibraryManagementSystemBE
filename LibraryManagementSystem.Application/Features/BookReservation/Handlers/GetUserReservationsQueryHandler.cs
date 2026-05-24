using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookReservation.Commands;
using LibraryManagementSystem.Application.Features.BookReservation.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookReservation.Handlers;

public class GetUserReservationsQueryHandler : IRequestHandler<GetUserReservationsQuery, ApiResponseModel<List<GetUserReservationsResponseDto>>>
{
    private readonly IBookReservationRepository _bookReservationRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetUserReservationsQueryHandler(IBookReservationRepository bookReservationRepository, IBookRepository bookRepository, IMapper mapper)
    {
        _bookReservationRepository = bookReservationRepository;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseModel<List<GetUserReservationsResponseDto>>> Handle(GetUserReservationsQuery request,
        CancellationToken cancellationToken)
    {
        var reservations =
            await _bookReservationRepository.GetUserReservationsAsync(request.getUserReservationsRequestDto.UserId,request.PageNumber,request.PageSize);

        var result = _mapper.Map<List<GetUserReservationsResponseDto>>(reservations);
        
        foreach (var dto in result)
        {
            var book = await _bookRepository.GetBookByIdAsync(dto.BookId);
            dto.BookTitle = book.Title;
            dto.BookImageUrl = book.ImageUrl;
        }

        var response = ApiResponseModel<List<GetUserReservationsResponseDto>>
            .SuccessResponse(
                result,
                "User reservations fetched successfully",
                200
                );
        
        return response;
    }
}
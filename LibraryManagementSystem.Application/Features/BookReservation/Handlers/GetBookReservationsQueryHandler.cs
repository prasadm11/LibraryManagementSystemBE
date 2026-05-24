using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookReservation.Commands;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookReservation.Handlers;

public class GetBookReservationsQueryHandler : IRequestHandler<GetBookReservationsQuery, ApiResponseModel<List<GetBookReservationsResponseDto>>>
{
    private readonly IBookReservationRepository _bookReservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetBookReservationsQueryHandler(IBookReservationRepository bookReservationRepository, IUserRepository userRepository, IMapper mapper)
    {
        _bookReservationRepository = bookReservationRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseModel<List<GetBookReservationsResponseDto>>> Handle(GetBookReservationsQuery request,
        CancellationToken cancellationToken)
    {
        var reservations =
            await _bookReservationRepository.GetBookReservationsAsync(request.getBookReservationsRequestDto.BookId,
                request.PageNumber, request.PageSize);
        var result = _mapper.Map<List<GetBookReservationsResponseDto>>(reservations);

        foreach (var dto in result)
        {
            var user = await _userRepository.GetUserByIdAsync(dto.UserId);
            dto.UserName = user.Username;
            dto.ProfileImageUrl = user.ProfileImageUrl;
        }

        var response = ApiResponseModel<List<GetBookReservationsResponseDto>>
            .SuccessResponse(
                result,
                "Book reservations fetched successfully",
                200
            );
        
        return response;
    }
}
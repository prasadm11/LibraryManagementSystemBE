using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookReservation.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookReservation.Commands;

public record GetUserReservationsQuery(GetUserReservationsRequestDto getUserReservationsRequestDto,int PageNumber, int PageSize): IRequest<ApiResponseModel<List<GetUserReservationsResponseDto>>>;
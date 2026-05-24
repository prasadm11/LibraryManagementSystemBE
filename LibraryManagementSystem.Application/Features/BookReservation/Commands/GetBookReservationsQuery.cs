using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookReservation.DTOs;
using LibraryManagementSystem.Application.Features.BookReservation.Handlers;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookReservation.Commands;

public record GetBookReservationsQuery(GetBookReservationsRequestDto getBookReservationsRequestDto, int PageNumber, int PageSize)
: IRequest<ApiResponseModel<List<GetBookReservationsResponseDto>>>;

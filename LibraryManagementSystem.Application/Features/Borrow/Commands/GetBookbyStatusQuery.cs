using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Entities;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Commands;

public record GetBookbyStatusQuery(GetBookBorrowStatusRequestDto GetBookBorrowStatusRequestDto,int pageNumber, int pageSize) : IRequest<ApiResponseModel<List<GetBookBorrowStatusResponseDto>>>;
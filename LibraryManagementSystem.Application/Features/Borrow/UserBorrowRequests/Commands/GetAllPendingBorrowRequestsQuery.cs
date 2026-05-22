using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;

public record GetAllPendingBorrowRequestsQuery(int pageNumber, int pageSize) : IRequest<ApiResponseModel<List<GetAllPendingBorrowRequestsResponseDto>>>; 
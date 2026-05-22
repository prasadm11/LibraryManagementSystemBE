using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands;

public record GetAllBooksQuery(int pageNumber, int pageSize) : IRequest<ApiResponseModel<List<BookResponseDto>>>;

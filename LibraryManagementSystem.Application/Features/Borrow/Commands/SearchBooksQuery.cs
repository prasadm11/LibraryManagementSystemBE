using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Commands;

public record SearchBooksQuery(SearchBooksRequestDto SearchBooksRequestDto,int pageNumber, int pageSize) : IRequest<ApiResponseModel<List<BookResponseDto>>>; 
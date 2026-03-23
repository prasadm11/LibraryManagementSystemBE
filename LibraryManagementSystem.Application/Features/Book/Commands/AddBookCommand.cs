using LibraryManagementSystem.Application.Features.Book.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands;

public record AddBookCommand(AddBookDto addBookDto) : IRequest<string>;
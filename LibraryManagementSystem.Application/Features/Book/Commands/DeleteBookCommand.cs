using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Commands;

public record DeleteBookCommand(int Id) : IRequest<string>;
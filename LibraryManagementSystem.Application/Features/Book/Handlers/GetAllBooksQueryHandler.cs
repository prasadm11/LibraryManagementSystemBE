using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Handlers;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookResponseDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookRatingRepository _bookRatingRepository;
    private readonly IMapper _mapper;
    
    public GetAllBooksQueryHandler(IBookRepository bookRepository,IBookRatingRepository bookRatingRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _bookRatingRepository = bookRatingRepository;
        _mapper = mapper;
    }

    public async Task<List<BookResponseDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var response = await _bookRepository.GetAllBooksAsync();
        var result = _mapper.Map<List<BookResponseDto>>(response);
        foreach (var bookDto in result)
        {
            var ratings = await _bookRatingRepository.GetBookRatings(bookDto.Id);
            bookDto.AverageRating = ratings.Count;
            bookDto.AverageRating = ratings.Any() ? Math.Round(ratings.Average(x=>x.Rating), 2) : 0;
        }
        return result;
    }
}
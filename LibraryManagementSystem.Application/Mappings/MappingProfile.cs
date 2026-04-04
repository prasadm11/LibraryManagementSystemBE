using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, GetAllUsersResponseDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<User, GetUserByIdDto>();

        //Books
        CreateMap<Book, BookResponseDto>();
        CreateMap<AddBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        
        //BorrowBook
        CreateMap<BorrowBookRequestDto, BorrowRecord>();
        CreateMap<BorrowRecord, BorrowBookResponseDto>()
            .ForMember(dest => dest.BorrowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        
        //Return Book
        CreateMap<BorrowRecord, ReturnBookResponseDto>()
            .ForMember(dest => dest.BorrowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        
        //Get Books By Status
        // Get Borrow By Status
        CreateMap<BorrowRecord, GetBookBorrowStatusResponseDto>()
            .ForMember(dest => dest.BorrowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }

}
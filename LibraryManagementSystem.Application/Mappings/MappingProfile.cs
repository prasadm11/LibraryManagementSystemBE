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

    }

}
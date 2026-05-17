using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
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
        CreateMap<User, CreateUserResponseDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        CreateMap<User, DeleteUserResponseDto>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id));
        CreateMap<User, UpdateUserResponseDto>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id));

        //Books
        CreateMap<Book, BookResponseDto>();
        CreateMap<AddBookDto, Book>().ReverseMap();
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
        
        //User Borrow History
        CreateMap<BorrowRecord, GetUserBorrowHistoryResponseDto>()
            .ForMember(dest => dest.BorrowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        
        //Overdue Books
        CreateMap<BorrowRecord, GetOverdueBooksResponseDto>()
            .ForMember(dest => dest.BorrowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.DaysLate, opt => opt.Ignore())
            .ForMember(dest => dest.FineAmount, opt => opt.Ignore());
        
        // Renew Book
        CreateMap<BorrowRecord, RenewBookResponseDto>()
            .ForMember(dest => dest.BorrowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.NewDueDate, opt => opt.MapFrom(src => src.DueDate));
        
        // Borrow Request
        CreateMap<BorrowRecordsUserRequest, GetAllPendingBorrowRequestsResponseDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        
    }

}
using AutoMapper;
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
    }
    
}
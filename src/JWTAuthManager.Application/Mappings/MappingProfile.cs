using AutoMapper;
using JWTAuthManager.Application.DTOs;
using JWTAuthManager.Domain.Entities;

namespace JWTAuthManager.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}
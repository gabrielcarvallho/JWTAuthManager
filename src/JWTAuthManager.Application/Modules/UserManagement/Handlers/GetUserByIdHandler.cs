using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Queries;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Application.Modules.UserManagement.Queries;
using JWTAuthManager.Domain.Repositories;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            return Result<UserDto>.Failure("User not found");

        var response = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(response);
    }
}
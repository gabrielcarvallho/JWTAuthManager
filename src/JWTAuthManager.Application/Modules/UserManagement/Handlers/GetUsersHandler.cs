using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Queries;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Application.Modules.UserManagement.Queries;
using JWTAuthManager.Domain.Repositories;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class GetUsersHandler : IQueryHandler<GetUsersQuery, Result<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        var response = _mapper.Map<IEnumerable<UserDto>>(users);

        return Result<IEnumerable<UserDto>>.Success(response);
    }
}
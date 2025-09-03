using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Queries;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Application.Modules.UserManagement.Queries;
using JWTAuthManager.Domain.Interfaces.Repositories;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unitOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Result<UserDto>.Failure("User not found");
        }

        var userDto = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(userDto);
    }
}
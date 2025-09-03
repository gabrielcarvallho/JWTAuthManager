using AutoMapper;
using JWTAuthManager.Application.Common.Interfaces.Messaging.Queries;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.UserManagement.DTOs;
using JWTAuthManager.Application.Modules.UserManagement.Queries;
using JWTAuthManager.Domain.Interfaces.Repositories;

namespace JWTAuthManager.Application.Modules.UserManagement.Handlers;

public class GetUsersHandler : IQueryHandler<GetUsersQuery, Result<PaginatedList<UserDto>>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUsersHandler(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unitOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Users.GetQueryable();

        query = query.Where(u => u.isActive == request.IsActive);
        query = query.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);

        var users = await PaginatedList<UserDto>.CreateAsync(
            query.Select(u => _mapper.Map<UserDto>(u)),
            request.PageNumber,
            request.PageSize);

        return Result<PaginatedList<UserDto>>.Success(users);
    }
}
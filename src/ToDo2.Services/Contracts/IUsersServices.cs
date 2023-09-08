using ToDo2.Services.Dtos.PaginatedSearch;
using ToDo2.Services.Dtos.Users;

namespace ToDo2.Services.Contracts;

public interface IUsersServices
{
    Task<UserDto?> Create(AddUsersDto dto);
    Task<UserDto?> Update(int id, UpdateUserDto dto);
    Task Remove(int id);
    Task<UserDto?> GetById(int id);
    Task<PagedDto<UserDto>> Search(BuscarUsersDto dto);
}
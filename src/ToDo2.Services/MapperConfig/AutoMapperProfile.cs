using AutoMapper;
using ToDo2.Domain.Entities;
using ToDo2.Domain.Paginacao;
using ToDo2.Services.Dtos.PaginatedSearch;
using ToDo2.Services.Dtos.Tasks;
using ToDo2.Services.Dtos.Users;

namespace ToDo2.Services.MapperConfig;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region Users

        CreateMap<Users, UserDto>().ReverseMap();
        CreateMap<Users, AddUsersDto>().ReverseMap();
        CreateMap<PagedDto<UserDto>, PaginatedResult<Users>>().ReverseMap();
        CreateMap<Users, UpdateUserDto>().ReverseMap();

        #endregion
        
        #region Tasks

        CreateMap<Tasks, TasksDto>().ReverseMap();
        CreateMap<Tasks, AddTasksDto>().ReverseMap();
        CreateMap<PagedDto<TasksDto>, PaginatedResult<Tasks>>().ReverseMap();
        CreateMap<Tasks, UpdateTasksDto>().ReverseMap();

        #endregion
    }
}
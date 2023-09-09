using AutoMapper;
using ToDo2.Domain.Contracts.Repositories;
using ToDo2.Domain.Entities;
using ToDo2.Services.Contracts;
using ToDo2.Services.Dtos.PaginatedSearch;
using ToDo2.Services.Dtos.Users;
using ToDo2.Services.NotificatorConfig;

namespace ToDo2.Services.Services;

public class UsersServices : BaseService, IUsersServices
{
    public UsersServices(IMapper mapper, INotificator notificator, IUsersRepository usersRepository, IHashServices hashServices) : base(mapper, notificator)
    {
        _usersRepository = usersRepository;
        _hashServices = hashServices;
    }
    
    private readonly IUsersRepository _usersRepository;
    private readonly IHashServices _hashServices;
    
    public async Task<UserDto?> Create(AddUsersDto dto)
    {
        var user = Mapper.Map<Users>(dto);
        if (!await Validate(user)) return null;
        
        user.Senha = _hashServices.GenerateHash(user.Senha);
        user.CriadoEm = DateTime.Now;
        _usersRepository.Create(user);
        
        if (await CommitChanges()) return Mapper.Map<UserDto>(user);
        
        Notificator.Handle("Não foi possível salvar esse usuário.");
        return null;
    }

    public async Task<UserDto?> Update(int id, UpdateUserDto dto)
    {
        if (id != dto.id)
        {
            Notificator.Handle("Ids não conferem.");
            return null;
        }
        
        var user = Mapper.Map<Users>(dto);
        if (!await Validate(user)) return null;
        
        _usersRepository.Update(user);
        if (await CommitChanges()) return Mapper.Map<UserDto>(user);
        
        Notificator.Handle("Não foi possível salvar esse usuário.");
        return null;
    }

    public async Task Remove(int id)
    {
        var user = await _usersRepository.GetById(id);
        if (user == null)
        {
            Notificator.HandleNotFound();
            return;
        }
        
        _usersRepository.Remove(user);
        
        if(!await CommitChanges()) Notificator.Handle("Não foi possível excluir o usuário.");
    }

    public async Task<UserDto?> GetById(int id)
    {
        var user = await _usersRepository.GetById(id);
        if (user != null) return Mapper.Map<UserDto>(user);
        
        Notificator.HandleNotFound();
        return null;
    }

    public async Task<PagedDto<UserDto>> Search(BuscarUsersDto dto)
    {
        var users = await _usersRepository.Search(dto);
        return Mapper.Map<PagedDto<UserDto>>(users);
    }

    private async Task<bool> Validate(Users users)
    {
        if (!users.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return false;
        }

        if (!await _usersRepository.Any(c => c.Id != users.Id && c.Email == users.Email)) return true;
        
        Notificator.Handle("Esse email já está cadastrado.");
        return false;
    }

    private async Task<bool> CommitChanges() => await _usersRepository.UnitOfWork.Commit();
}
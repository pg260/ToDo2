using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ToDo2.Core.JwtSettings;
using ToDo2.Domain.Contracts.Repositories;
using ToDo2.Domain.Entities;
using ToDo2.Services.Contracts;
using ToDo2.Services.Dtos.Auth;
using ToDo2.Services.Dtos.Users;
using ToDo2.Services.NotificatorConfig;

namespace ToDo2.Services.Services;

public class AuthServices : BaseService, IAuthServices
{
    public AuthServices(IMapper mapper, INotificator notificator, IUsersRepository usersRepository, IHashServices hashServices) : base(mapper, notificator)
    {
        _usersRepository = usersRepository;
        _hashServices = hashServices;
    }

    private readonly IUsersRepository _usersRepository;
    private readonly IHashServices _hashServices;

    public async Task<AuthUserDto?> Login(LoginDto dto)
    {
        var user = await _usersRepository.FirstOrDefault(c => c.Email == dto.Email);
        if (user == null)
        {
            Notificator.Handle("Email incorreto.");
            return null;
        }

        if (!_hashServices.VerifyHash(dto.Senha, user.Senha))
        {
            Notificator.Handle("Senha incorreta.");
            return null;
        }

        var token = GenerateToken(user, Settings.Secret);
        var userDto = Mapper.Map<UserDto>(user);

        return new AuthUserDto
        {
            UserDto = userDto,
            Token = token
        };
    }
    
    private string GenerateToken(Users users, string key)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var encodeKey = Encoding.ASCII.GetBytes(key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                new Claim(ClaimTypes.Name, users.Nome),
                new Claim(ClaimTypes.Email, users.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(encodeKey),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
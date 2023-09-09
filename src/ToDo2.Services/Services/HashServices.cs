﻿using AutoMapper;
using ToDo2.Services.Contracts;
using ToDo2.Services.NotificatorConfig;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace ToDo2.Services.Services;

public class HashServices : BaseService, IHashServices
{
    public HashServices(IMapper mapper, INotificator notificator) : base(mapper, notificator)
    {
    }

    public string GenerateHash(string password)
    {
        var salt = new byte[32];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var argon2 = new Argon2i(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 8,
            Iterations = 4,
            MemorySize = 1024 * 1024
        };

        var hash = argon2.GetBytes(32);

        var saltedHash = new byte[salt.Length + hash.Length];
        Array.Copy(salt, 0, saltedHash, 0, salt.Length);
        Array.Copy(hash, 0, saltedHash, salt.Length, hash.Length);

        return Convert.ToBase64String(saltedHash);
    }

    public bool VerifyHash(string password, string hash)
    {
        var saltedHash = Convert.FromBase64String(hash);

        var salt = new byte[32];
        Array.Copy(saltedHash,0,salt,0, salt.Length);

        var argon2 = new Argon2i(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 8,
            Iterations = 4,
            MemorySize = 1024 * 1024
        };
        
        var newHash = argon2.GetBytes(32);
        
        var saltedNewHash = new byte[salt.Length + newHash.Length];
        Array.Copy(salt, 0, saltedNewHash, 0, salt.Length);
        Array.Copy(newHash, 0, saltedNewHash, salt.Length, newHash.Length);

        return saltedNewHash.SequenceEqual(saltedHash);
    }
}
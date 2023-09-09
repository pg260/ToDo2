namespace ToDo2.Services.Contracts;

public interface IHashServices
{
    string GenerateHash(string password);
    bool VerifyHash(string password, string hash);
}
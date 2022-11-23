using API.DTOs;

namespace API.Services;

public interface IAuthenticationService
{
    public bool ValidateUser(string userName, string password, out string token);
    public string Register(RegisterDTO dto);
    public string Login(LoginDTO dto);
}
using API.DTOs;
using Core;

namespace Application.Interfaces;

public interface IUserRepository
{
    public User CreateNewUser(User user);
    public User GetUserByEmail(string email);
    public User GetUserByUserName(string username);
    User UpdateUser(User user);
    bool CheckUserName(RegisterDTO userName);
}
using API.DTOs;
using Core;

namespace Application.Interfaces;

public interface IUserService
{
    public User CreateNewUser(User user);
    public UserDTO GetUserByEmail(string email);
    public User GetUserByUserName(string username);

    public RegisterDTO UpdateUser(RegisterDTO user);
    void changeBanStatus(string email);
}
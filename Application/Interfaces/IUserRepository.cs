using Core;

namespace Application.Interfaces;

public interface IUserRepository
{
    public User CreateNewUser(User user);
    public User GetUserByEmail(string email);
}
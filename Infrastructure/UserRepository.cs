using API.DTOs;
using Application.Interfaces;
using Core;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly BikeShopDbContext _bikeShopDbContext;

    public UserRepository(BikeShopDbContext context)
    {
        _bikeShopDbContext = context;
    }
    public User CreateNewUser(User user)
    {
        _bikeShopDbContext.UsersTable.Add(user);
        _bikeShopDbContext.SaveChanges();
        return user;
    }

    public User GetUserByEmail(string email)
    {
        Console.WriteLine("Email in user repository: "+email);
        return _bikeShopDbContext.UsersTable
                .FirstOrDefault(u=> u.Email.Equals(email)) ?? throw new KeyNotFoundException("There was no users with email "+email);
    }

    public User GetUserByUserName(string username)
    {
        return _bikeShopDbContext.UsersTable
            .FirstOrDefault(u=> u.userName.Equals(username)) ?? throw new KeyNotFoundException("There was no users with username "+username);
    }
}
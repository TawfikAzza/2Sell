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

    public User UpdateUser(User user)
    {
        Console.WriteLine("Update User in repository"+user.FirstName);
        _bikeShopDbContext.UsersTable.Update(user);
        _bikeShopDbContext.SaveChanges();
        return user;
    }

    public bool CheckUserName(RegisterDTO userName)
    {
        Console.WriteLine("In UserRepository : "+userName.Email);
        
        
        int user = _bikeShopDbContext.UsersTable.Where(u => (u.userName == userName.userName && u.Email != userName.Email)).Count();
        Console.WriteLine("Count: "+user);
        if (user > 0)
        {
            Console.WriteLine("User is null");
            return false;
        }
            
        return true;
    }

    public List<User> GetAllUsers()
    {
        return _bikeShopDbContext.UsersTable.ToList();
    }

    public void changeBanStatus(string email)
    {
        Console.WriteLine("EMAIL: "+email);
        if (email != "")
        {   
            User user = _bikeShopDbContext.UsersTable.Single(u => u.Email.Equals(email));
           if (user.RoleId == 1)
                user.RoleId = 2;
            else
                user.RoleId = 1;
            _bikeShopDbContext.UsersTable.Update(user);
            _bikeShopDbContext.SaveChanges();
          
        }

       
    }
}
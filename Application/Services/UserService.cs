using System.Security.Cryptography;
using API.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core;

namespace Application.Services;

public class UserService :IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository repository,IMapper mapper)
    {
        _userRepository = repository;
        _mapper = mapper;
    }
    public User CreateNewUser(User user)
    {
        return _userRepository.CreateNewUser(user);
    }
    private bool checkEmail(string email)
    {
        try
        {
            _userRepository.GetUserByEmail(email);
        }
        catch (KeyNotFoundException)
        {
            return false;
        }
        return true;
    }
    private bool checkUserName(RegisterDTO user)
    {
        return _userRepository.CheckUserName(user);
    }
    public UserDTO GetUserByEmail(string email)
    {
        Console.WriteLine("Email in user service : "+email);
        User user =  _userRepository.GetUserByEmail(email);
        UserDTO userDto = _mapper.Map<User, UserDTO>(user);
        return userDto;
    }

    public User GetUserByUserName(string username)
    {
        return _userRepository.GetUserByUserName(username);
    }

    public RegisterDTO UpdateUser(RegisterDTO userToModify)
    {
        string message = "";

        if (!checkUserName(userToModify))
        {
            message = "User Name already exists";
        }
        if (message != "")
        {
            throw new Exception(message);
        }
        
        User user = _userRepository.GetUserByEmail(userToModify.Email);
        user.Email = userToModify.Email;
        user.userName = userToModify.userName;
        user.FirstName = userToModify.FirstName;
        user.LastName = userToModify.LastName;
        user.Address = userToModify.Address;
        user.PostalCode = userToModify.PostalCode;
        user.PhoneNumber = userToModify.PhoneNumber;
        user.RoleId = userToModify.RoleId;
        var saltBytes = RandomNumberGenerator.GetBytes(32);
        string salt = "";
        foreach (byte bit in saltBytes)
        {
            salt += bit;
        }
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userToModify.Password + salt);
        
        User userUpdated =  _userRepository.UpdateUser(user);
        RegisterDTO userDto = _mapper.Map<User, RegisterDTO>(userUpdated);
        return userDto;
    }
}
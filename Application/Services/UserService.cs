using System.Security.Cryptography;
using API.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core;
using FluentValidation.TestHelper;

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
        if (user.Img == "")
            user.Img = "https://cdn.filestackcontent.com/5W5vdLq1TmnfSXV0PKlS";
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

       return _mapper.Map<User, UserDTO>(_userRepository.GetAllUsers()
           .Where(u => u.Email == email).FirstOrDefault());
    }

    private User GetUserByEmailDB(string email)
    {
        return _userRepository.GetUserByEmail(email);
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
        if (userToModify.Img != "")
            user.Img = userToModify.Img;
        else
            user.Img = "https://cdn.filestackcontent.com/5W5vdLq1TmnfSXV0PKlS";
        var saltBytes = RandomNumberGenerator.GetBytes(32);
        string salt = "";
        foreach (byte bit in saltBytes)
        {
            salt += bit;
        }
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userToModify.Password + salt);
        user.Salt = salt;
        User userUpdated =  _userRepository.UpdateUser(user);
        RegisterDTO userDto = _mapper.Map<User, RegisterDTO>(userUpdated);
        return userDto;
    }

    public void changeBanStatus(string email)
    {
        _userRepository.changeBanStatus(email);
    }
}
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
}
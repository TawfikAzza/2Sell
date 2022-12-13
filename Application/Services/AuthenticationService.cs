using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Services;
using Application.Helpers;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Core;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppSettings _appSettings;
    private readonly IUserRepository _userRepository;
    private readonly IBikeShopRepository _bikeShopRepository;
    private byte[] _secretByte;
    private readonly IMapper _mapper;
    public AuthenticationService(IUserRepository repository
        ,IOptions<AppSettings> appSettings
        ,IBikeShopRepository bikeShopRepository,
        IMapper mapper)
    {
        _userRepository = repository;
        _appSettings = appSettings.Value;
        _bikeShopRepository = bikeShopRepository;
        _mapper = mapper;
    }

    public AuthenticationService(IUserRepository repository)
    {
        _userRepository = repository;
    }
    public bool ValidateUser(string userName, string password, out string token)
    {
        throw new NotImplementedException();
    }

    public string Register(RegisterDTO dto)
    {
        string message = "";

        if (checkEmail(dto.Email))
        {
            message = "Email already exists";
        }
        
        if (checkUserName(dto.userName))
        {
            if (message != "")
                message = "Email and User Name already exists";
            else
                message = "User Name already exists";

        }
        if (message != "")
        {
            throw new Exception(message);
        }
        var saltBytes = RandomNumberGenerator.GetBytes(32);
        string salt = "";
        foreach (byte bit in saltBytes)
        {
            salt += bit;
        }

        UserValidator validator = new UserValidator();
        ValidationResult validation = validator.Validate(dto);
        if (validation.IsValid)
        {
            User user = new User() {
                Email = dto.Email,
                Salt = salt,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password + salt),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                PostalCode = dto.PostalCode,
                userName = dto.userName,
                RoleId = Convert.ToInt32(dto.RoleId)
            };
            user.Img = "";
        
            _userRepository.CreateNewUser(user);
            return GenerateToken(user);
        }
    return null;
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
    private bool checkUserName(string userName)
    {
        try
        {
            _userRepository.GetUserByUserName(userName);
        }
        catch (KeyNotFoundException)
        {
            return false;
        }
        return true;
    }
    public string Login(LoginDTO dto)
    {
        var user = _userRepository.GetUserByEmail(dto.Email);
        Console.WriteLine("In Auth Service :" + dto.Email);
        if (BCrypt.Net.BCrypt.Verify(dto.Password + user.Salt, user.PasswordHash))
        {
            return GenerateToken(user);
        }
        throw new Exception("Invalid Login");
    }
    
    //Peter's way of implementing the token generation, I paste it here if you want to have a look at how he does it,
    //I decided in the end to use Alex's approach as it is more clear and straightforward and contains everything we need to know
    //about token creation for now.
    private string CreateToken(string username)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
        //claims.Add(new Claim(ClaimTypes.Role, _userRole[username]));
        //byte[] secret = System.Text.Encoding.UTF8.GetBytes("SuperDuperSecret");
        JwtSecurityToken token = new JwtSecurityToken(
            new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(_secretByte),
                SecurityAlgorithms.HmacSha512)),
            new JwtPayload(null, null,claims,DateTime.Now,DateTime.Now.AddMinutes(10))
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    //End of Peter's way
    private string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
        Console.WriteLine("User role Id"+user.RoleId);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            
            Expires = DateTime.UtcNow.AddDays(7),
            Subject = new ClaimsIdentity(new[]
            { new Claim("email", user.Email), 
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("userName",user.userName),
                new Claim("expDate",DateTime.UtcNow.AddDays(7).ToString())
            }),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}